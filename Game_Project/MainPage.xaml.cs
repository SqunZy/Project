using DataBase.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Game_Project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private User user;
        public MainPage()
        {
            this.InitializeComponent();
            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
        }

        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
            if (e.Parameter != null && e.Parameter.ToString() != "")
            {
                this.user = (User)e.Parameter;
                ChooseButton.IsEnabled = true;
                ShopButton.IsEnabled = true;
                ScoreButton.IsEnabled = true;
                Storage.IsEnabled = true;
                
            }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage),this.user);
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private void ChooseButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage),this.user);
        }

        private void ShopButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShopPage), this.user);
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage),this.user);

        }

        private void ScoreButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ScorePage), this.user);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (user!=null)
            {
                ChooseButton.IsEnabled = true;
                ShopButton.IsEnabled = true;
                Storage.IsEnabled = true;

            }
        }

        private void StorageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Storage), this.user);
        }

       
    }
}
