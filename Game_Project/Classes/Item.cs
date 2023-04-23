using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Game_Project.Classes
{

    abstract class Item
    {
        private bool distroyted; 
        protected DispatcherTimer moveTimer;//הטיימר יאפשר התקדמות הדמות
        public Image Image { get; } //מראה הפריט  
        protected Canvas Arena;//זירת המשחק
        protected double placeX;//מיקום הדמות במישור האופקי
        protected double placeY;//מיקום הדמות במישור האנכי
        public double PlaceX { get { return this.placeX; } set {this.placeX = value; } }
        public double PlaceY { get { return this.placeY; } set {this.placeY = value; } }
      

        /// <summary>
        /// פעולה הבונה עצם מטיפוס פריט אך לעולם לא נזמן אותה 
        /// </summary>
        /// <param name="placeX">מיקום אופקי</param>
        /// <param name="placeY">מיקום אנכי</param>
        /// <param name="arena">זירת המשחק</param>
        public Item(double placeX, double placeY, Canvas Arena, double size)
        {
            this.Image = new Image();
            this.placeX = placeX;
            this.placeY = placeY;
            this.Arena = Arena;
            this.Image.Height = size;
            this.Image.Width = size;
            Canvas.SetLeft(this.Image, this.placeX);//קביעת מיקום אופקי
            Canvas.SetTop(this.Image, this.placeY);//קביעת מיקום אנכי
            this.Arena.Children.Add(this.Image);//שיוך הדמות לזירה
            this.moveTimer = new DispatcherTimer();
            this.moveTimer.Interval = TimeSpan.FromMilliseconds(1);
            this.moveTimer.Tick += MoveTimer_Tick;
            this.moveTimer.Start();
        }
        public virtual Rect GetRectangle() => new Rect((int)this.placeX, (int)this.placeY, (int)this.Image.Width,(int)this.Image.Height);
        protected virtual void MoveTimer_Tick(object sender, object e) { }

    }
}

