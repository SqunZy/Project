using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Game_Project.Classes
{
    internal class Dino : Character
    {
 
        /// <summary>
        /// הפעולה בונה עצם מטיפוס דינוזאור 
        /// </summary>
        /// <param name="placeX"> מיקום אופקי </param>
        /// <param name="placeY"> מיקום אנכי </param>
        /// <param name="arena"> גבולות זירת המשחק </param>
        /// <param name="size"> גודל הדמות </param>
        public Dino(double placeX, double placeY, Canvas arena, double size):base(placeX, placeY, arena, size)
        {
          
        }
        /// <summary>
        ///  פעולה המתאימה את הגיפ למצב הדמות
        /// </summary>
        public override void  MatchGiftoState()
        {
            switch (this.state)
            {
                case StateType.Jump: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Dino/DinoJumpRight.gif")); break;
                case StateType.RunRight: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Dino/DinoRunRight.gif")); break;
                case StateType.Die: this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Dino/DinoDeadGif.gif")); break;
            }
        }
        public override Rect GetRectangle() => new Rect((int)this.placeX, (int)this.placeY, (int)this.Image.Width-100, (int)this.Image.Height-100);

    }
}
