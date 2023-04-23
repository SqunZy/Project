using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Game_Project.Classes
{
    class Trap : MovingItem 
    {
        private TrapType trapType;
        private Random rnd;
        private int rnd2;
        public event EventHandler Touch;
        private int number;
        public Trap(double placeX, double placeY, double speedX, Canvas arena, double size) : base(placeX, placeY, arena, size)
        {
            this.rnd = new Random();
            this.number = new Random().Next(4);
            if (this.number == 0)
            {
                this.trapType = TrapType.Bird;
                base.placeY = arena.ActualHeight - rnd.Next(500, 1500);
                Canvas.SetTop(base.Image, base.PlaceY);
            }
            else
                this.trapType = TrapType.Kaktus;
            base.speedX = -speedX;
            MatchGifToState();
        }
        protected override void MoveTimer_Tick(object sender, object e)
        {
            base.MoveTimer_Tick(sender, e);

            //if (this.placeX <= 0)
            //{
            //    this.placeX += 2;
            //    Canvas.SetLeft(this.Image, this.placeX);
            //    this.speedX = 0;
            //}
            if (this.Touch != null)
                this.Touch(this, null);
        }
        protected override void MatchGifToState()
        {
            switch (this.trapType)
            {
                case TrapType.Kaktus:
                    base.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets//kaktus.jpg"));
                    break;
                case TrapType.Bird:
                    base.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets//spaceship.png"));
                    break;     
            }
        }
        public override Rect GetRectangle() => new Rect((int)this.placeX, (int)this.placeY, (int)this.Image.Width, (int)this.Image.Height - 150);
        
       
    }
    enum TrapType
    {
        Kaktus,
        Bird
    }
}
