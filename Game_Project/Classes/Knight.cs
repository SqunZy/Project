using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml;

namespace Game_Project.Classes
{
    internal class Knight : Character
    {

        /// <summary>
        /// הפעולה בונה עצם מטיפוס אביר
        /// </summary>
        /// <param name="placeX"> מיקום אופקי </param>
        /// <param name="placeY"> מיקום אנכי </param>
        /// <param name="arena"> גבולות זירת המשחק </param>
        /// <param name="size"> גודל הדמות </param>
        public Knight(double placeX, double placeY, Canvas arena, double size) : base(placeX, placeY, arena, size)
        {

        }
        /// <summary>
        ///  פעולה המתאימה את הגיפ למצב הדמות
        /// </summary>
        public override void MatchGiftoState()
        {
            switch (this.state)
            {
                //case StateType.AttackLeft: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Knight/AttackLeft.gif")); break;
                //case StateType.AttackRight: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Knight/AttackRight.gif")); break;
                //case StateType.Dead: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Knight/Dead.gif")); break;
                //case StateType.IdleLeft: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Knight/IdleLeft.gif")); break;
                //case StateType.IdleRight: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Knight/IdleRight.gif")); break;
                //case StateType.JumpLeft: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Knight/JumpLeft.gif")); break;
                case StateType.Jump: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Knight/JumpRight.gif")); break;
                //case StateType.RunLeft: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Knight/RunLeft.gif")); break;
                case StateType.RunRight: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Knight/RunRight.gif")); break;
                //case StateType.WalkLeft: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Knight/WalkLeft.gif")); break;
                //case StateType.WalkRight: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Knight/WalkRight.gif")); break;
            }
        }
    }
}
