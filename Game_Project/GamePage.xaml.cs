using DataBase.Models;
using Game_Project.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Game_Project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private User user;
       
        private Manager manager;

        public string ImageSource { get; private set; }

        public GamePage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter.ToString() != "")
            {
                this.user = (User)e.Parameter;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.manager = new Manager(MyGrid, Arena, this.user);
            this.manager.removeLive += Manager_removeLive;
            this.manager.addcoins += Manager_addcoins;
            this.manager.prize += Manager_prize;
            this.manager.Time += Manager_Time; 
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            Character.grid = Gridon;
        }

        private void Manager_Time(object sender, EventArgs e)
        {
            int seconds = (int)sender;
            TimeNumbersTextNlock.Text = seconds.ToString();
        }

        private void Manager_prize(object sender, EventArgs e)
        {
            Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp -= CoreWindow_KeyUp;
        }

        private void Manager_addcoins(object sender, EventArgs e)
        {
            int point = (int)sender;
            PointsTextBlock.Text = point.ToString();
        }

        private void Manager_removeLive(object sender, EventArgs e)
        {
            int lives = (int)sender;
            if (lives==2)
            {
                this.Gridon.Children.Remove(heart3);
            }
            else  
               if (lives==1)
                     this.Gridon.Children.Remove(heart2);
            else
                if (lives == 0)
            {
                this.Gridon.Children.Remove(heart1);
                this.manager.Prize();

            }
            else
            {
                //PopupGrid.Visibility = Visibility.Visible;
            }
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            this.manager.GoCharacter(args.VirtualKey);
        }
        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            this.manager.StopCharacter();
        }
       

        private void TimeNumbersTextNlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
