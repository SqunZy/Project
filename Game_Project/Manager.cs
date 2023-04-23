using Game_Project.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using User = DataBase.Models.User;

namespace Game_Project
{
    
    internal class Manager
    {
      
        private Character mycharacter;
        private List<MovingItem> objects;
        private Random rnd;

        private int currentspeed;
        private DispatcherTimer addTimer;
        private DispatcherTimer timeTimer;
        private Canvas arena;
        private Grid grid;
        private int lives;//חיים
        public event EventHandler removeLive; //אירוע הורדת לב
        public event EventHandler prize; //אירוע הורדת לב
        private int points;
        private int seconds;
        private User user;
        public event EventHandler Time;
        public event EventHandler addcoins;//אירוע עדכון נקודות

        
        public Manager(Grid grid,Canvas Arena, User user)
        {
            this.seconds = 0;
            rnd = new Random();
            this.lives = 3;
            this.points = 0;
            this.arena = Arena;
            this.grid = grid;
            this.user = user;
            this.currentspeed = 10;

            this.mycharacter = new Dino(50, arena.ActualHeight-410, Arena, 305);

          

            this.objects = new List<MovingItem>();

            this.addTimer = new DispatcherTimer();
            this.addTimer.Interval = TimeSpan.FromSeconds(2);
            this.addTimer.Start();
            this.addTimer.Tick += AddTimer_Tick;


            this.timeTimer = new DispatcherTimer();
            this.timeTimer.Interval = TimeSpan.FromSeconds(1);
            this.timeTimer.Start();
            this.timeTimer.Tick += TimeTimer_Tick;
        }

        private void TimeTimer_Tick(object sender, object e)
        {
            this.seconds++;
            if (Time!=null)
                Time(this.seconds, null);
            if (this.seconds % 10 ==0)
                this.currentspeed += 2;
            
            
        }

        private void AddTimer_Tick(object sender, object e)
        {
            int num = rnd.Next(3);
            if (num == 0)
            {
                Coin coin = new Coin((int)(arena.ActualWidth + 50), arena.ActualHeight - 200, arena, 30);
                this.objects.Add(coin);
                coin.Touch += Coin_Touch;
                coin.RemoveObjectEvent += Coin_RemoveObjectEvent;
            }
            else
            {
                Trap trap = new Trap((int)(arena.ActualWidth + 50), arena.ActualHeight - 310,this.currentspeed, arena,170);
                this.objects.Add(trap);
                trap.Touch += Trap_Touch;
                trap.RemoveObjectEvent += Coin_RemoveObjectEvent;
            }
        }
     

        public async void Prize()
        { 
            for (int i = 0; i < this.objects.Count; i++)
            {
                this.objects[i].Stop();
                this.addTimer.Stop();
            }
            grid.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/PrizeBackground.png")), Stretch = Stretch.Fill };
            //await Task.Delay(30000);
            this.mycharacter.Dead();
            this.addTimer.Tick -= AddTimer_Tick;
            if (prize != null)
                prize(null, null);

        }
        private void Coin_RemoveObjectEvent(object sender, EventArgs e)
        {
            MovingItem item = (MovingItem)sender;
            this.objects.Remove(item);
        }

        private void Coin_Touch(object sender, EventArgs e)
        {
            Coin coin = (Coin)sender;
            Rect characterRect = this.mycharacter.GetRectangle();
            Rect zombieRect = coin.GetRectangle();
            Rect touchRect = RectHelper.Intersect(characterRect, zombieRect);
            if (touchRect.Width > 0 || touchRect.Height > 0)//בודר אם יש אורך ורחוב למבן שנוצר כתוצאה מההצלבה של שני המלבנים            {
            {
                this.arena.Children.Remove(coin.Image);
                this.objects.Remove(coin);
                coin.Touch -= Coin_Touch;
                this.points++;
                if (addcoins != null)
                    addcoins(this.points, null);
            }
        }
        private void Trap_Touch(object sender, EventArgs e)
        {
            Trap trap = (Trap)sender;
            Rect characterRect = this.mycharacter.GetRectangle();
            Rect zombieRect = trap.GetRectangle();
            Rect touchRect = RectHelper.Intersect(characterRect, zombieRect);
            if (touchRect.Width > 0 || touchRect.Height > 0)//בודר אם יש אורך ורחוב למבן שנוצר כתוצאה מההצלבה של שני המלבנים            {
            {
                this.arena.Children.Remove(trap.Image);
                this.objects.Remove(trap);
                trap.Touch -= Trap_Touch;
                this.lives--;
                if (removeLive != null)
                    removeLive(this.lives, null);
            }
        }

        internal void GoCharacter(VirtualKey virtualKey)
        {
            switch (virtualKey)
            {
                case VirtualKey.Up:
                case VirtualKey.W:
                case VirtualKey.Space: this.mycharacter.Jump();  break;
            }
        }
        public void StopCharacter()
        {
            this.mycharacter.Stop();
        }
    }
}
