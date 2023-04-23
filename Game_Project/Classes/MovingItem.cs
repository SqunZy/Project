using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Rectangle = System.Drawing.Rectangle;

namespace Game_Project.Classes
{
    internal class MovingItem : Item
    {

        protected double accelerationY;//תאוצת הכובד
        protected double SpeedY;//מהירות אנכית
        protected StateType state;//מצב הדמות
        protected double speedX;
        public event EventHandler RemoveObjectEvent;

        public MovingItem(double placeX, double placeY, Canvas arena, double size) : base(placeX, placeY, arena, size)
        {
            this.SpeedY = 0;
            this.speedX = 0;
            this.accelerationY = 0;


        }
        protected override void MoveTimer_Tick(object sender, object e)
        {
            this.placeX += this.speedX;
            Canvas.SetLeft(this.Image, this.placeX);
            if(this.placeX<-150)
            {
                this.Arena.Children.Remove(this.Image);
                if (this.RemoveObjectEvent != null)
                    this.RemoveObjectEvent(this, null);
            }
        }
        protected virtual void MatchGifToState() { }

        internal void Stop()
        {
            this.speedX = 0;
            this.SpeedY = 0;
        }
    }
}
