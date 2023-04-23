using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Runtime.InteropServices;

namespace Game_Project.Classes
{
    internal class Character : MovingItem
    {

        protected StateType state;//מצב הדמות
        public static Grid grid;
        public Character(double PlaceX, double PlaceY, Canvas arena, double size) : base(PlaceX, PlaceY, arena, size)
        {
            this.state = StateType.RunRight;
            MatchGiftoState();

        }
        /// <summary>
        /// הפעולה מתבצעת באופן מתמיד ואחראית על הנעת הדמות
        /// </summary>
        protected override void MoveTimer_Tick(object sender, object e)
        {
            base.MoveTimer_Tick(sender, e);
            if (this.placeY + this.Image.Height >= Arena.ActualHeight -157)//נגיעה ברצפת הזירה
            {
                MatchGifToState();
                this.SpeedY = 0;
                this.accelerationY = 0;
                this.speedX = 0;
                this.placeY -= 1;
            }
            if (this.placeY + this.Image.Height <= Arena.ActualHeight - (Arena.ActualHeight - grid.ActualHeight))//נגיעה בתקרת הזירה
            {
                MatchGifToState();
                this.SpeedY = 0;
                this.speedX = 0;
                this.placeY += 7;
            }
            this.placeY += this.SpeedY;
            this.SpeedY += this.accelerationY;

            Canvas.SetTop(this.Image, this.placeY);
            if (this.state == StateType.Jump)
                this.state = StateType.RunRight;
        }
        public void Dead()
        {
            this.state = StateType.Die;
            base.speedX = 0;
            MatchGiftoState();
            this.moveTimer.Tick -= MoveTimer_Tick;

        }
        public new  void Stop()
        {
            if (this.state == StateType.RunRight || this.state == StateType.Jump)
            {
                this.state = StateType.RunRight;
                base.speedX = 0;
            }
            MatchGiftoState();
        }
        /// <summary>
        ///הפעולה מבצעת קפיצה   
        /// </summary>
        //  אם הספיד וואי שווה לאפס, כלומר יש נגיעה ברצפה (שזאת הפעם היחידה בתהליך הקפיצה בה הדמות יכולה להיכנס לפונקציה  )
        public void Jump()
        {
            if(SpeedY == 0)
                if (this.state == StateType.RunRight)
                {
                this.state = StateType.Jump;
                this.SpeedY = -22;
                this.accelerationY = 0.8;
                MatchGiftoState();
                }
        }


        public virtual void MatchGiftoState()
        {
            return;
        }
    }

    public enum StateType
    {
     Jump, RunRight,
        Die
    }
}