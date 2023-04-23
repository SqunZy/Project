
using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;

namespace Game_Project.Classes
{
    internal class Coin :MovingItem
    { 
        private Random rnd;
        public event EventHandler Touch; 
        public Coin(double placeX, double placeY, Canvas arena, double size) : base(placeX, placeY, arena, size)
        {
            base.speedX = -20;
            base.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Cristal.png"));
            this.rnd = new Random();
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
    }
}
