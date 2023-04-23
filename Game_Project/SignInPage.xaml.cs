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
using DataBase;
using DataBase.Models;
using Windows.UI.Popups;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Game_Project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInPage : Page
    {
        private User user;

        public SignInPage()
        {
            this.InitializeComponent();
            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TryEnterFullScreenMode();

        }

        private void LoginCheckInput()
        {
            if (UserNameTextBox.Text != "" && UserPasswordTextBox.Password != "")
                LogInbtn.IsEnabled = true;
            else
                LogInbtn.IsEnabled = false;

        }

        private void SignUpCheckInput()
        {
            if (UserNameTextBoxSignUp.Text != "" && LastNameTextBoxSignUp.Text != "" && EmailTextBoxSignUp.Text != "" && NewPasswordTextBoxSignUp.Password != "")
                SignUpbtn.IsEnabled = true;
            else
                SignUpbtn.IsEnabled = false;
        }

        private void UserNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UserNameTextBox.Text != "" && UserPasswordTextBox.Password != "" &&
                              UserPasswordTextBox.Password != "")
            {
                LogInbtn.IsEnabled = true;
            }
            else
            {
                LogInbtn.IsEnabled = false;
            }
        }

        private void UserPasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if (UserNameTextBox.Text != "" && UserPasswordTextBox.Password != "" &&
                              UserPasswordTextBox.Password != "")
            {
                LogInbtn.IsEnabled = true;
            }
            else
            {
                LogInbtn.IsEnabled = false;
            }
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SignUpCheckInput();
        }

        private void EmailTextBoxSignUp_TextChanged(object sender, TextChangedEventArgs e)
        {
            SignUpCheckInput();
        }

        private void LastNameTextBoxSignUp_TextChanged(object sender, TextChangedEventArgs e)
        {
            SignUpCheckInput();
        }

        private void UserNameTextBoxSignUp_TextChanged(object sender, TextChangedEventArgs e)
        {
            SignUpCheckInput();
        }

        private void NewPasswordTextBoxSignUp_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SignUpCheckInput();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }


        /// <summary>
        /// הפעולה מאפשרת לקבל נתון מהשליח
        /// </summary>
        /// <param name="e">הנתון שנשלח</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            if (e.Parameter != null && e.Parameter.ToString() != "")
            {
                this.user = (User)e.Parameter;
            }
        }

      
       
        //פעולה הבודקת שהסיסמא תקינה
        private bool IsPasswordRight(string password)
        {
            if (password.Length < 4)
            {
                return false;
            }

            bool hasUpper = false;
            bool hasLower = false;
            bool hasNum = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUpper = true;
                }
                else if (char.IsLower(c))
                {
                    hasLower = true;
                }
                else if (char.IsNumber(c))
                {
                    hasNum = true;
                }

                if (hasUpper && hasNum && hasLower)
                {
                    return true;
                }
            }
            return false;
        }
        //פעולה הבודקת שהמייל חוקי
        private bool IsLegalMail(string text)
        {
            int counter = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '@')
                {
                    counter++;
                }
            }

            if (counter > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        

        private void SignUpbtn_Click(object sender, RoutedEventArgs e)
        {
            if (!NewPasswordTextBoxSignUp.Password.Equals(reNewPasswordTextBoxSignUp.Password))
            {
                //הודעה שהסיסמאות לא שוות
                //PasswordsCheckGrid.Visibility = Visibility.Visible;
                var messageDialog = new MessageDialog("password not match");
                messageDialog.ShowAsync();
            }
            else
            {
                if (!IsLegalMail(EmailTextBoxSignUp.Text))
                {
                    //המייל לא בסדר
                    var messageDialog = new MessageDialog("Email is illegal");
                    messageDialog.ShowAsync();
                }
                else
                {
                    if (!IsPasswordRight(NewPasswordTextBoxSignUp.Password))
                    {
                        //הודעה שגוייה
                        var messageDialog = new MessageDialog("password is illegal");
                        messageDialog.ShowAsync();
                    }
                    else
                    {
                        //כאן מתבצעת בדיקת תקינות הקלט
                        this.user = DataBaseMethods.AddUser(UserNameTextBoxSignUp.Text, NewPasswordTextBoxSignUp.Password, EmailTextBoxSignUp.Text);
                        if (this.user == null)
                        {
                            
                            var messageDialog = new MessageDialog("The user is already exist go sign in");
                            messageDialog.ShowAsync();
                        }
                        else
                        {
                            //הצגת הודעה שמבשרת שהמשתמש התווסף למסד הנתונים
                            //העברת המשתמש לתפריט הראשי יחד עם המשתמש
                            var messageDialog = new MessageDialog("sign up success");
                            messageDialog.ShowAsync();
                            Frame.Navigate(typeof(MainPage), this.user);
                        }
                    }
                }
            }
        }

        private void LogInbtn_Click(object sender, RoutedEventArgs e)
        {
            this.user = DataBaseMethods.GetUser(UserNameTextBox.Text, UserPasswordTextBox.Password);

            if (this.user == null)
            {
                var messageDialog = new MessageDialog("The user is already exist go sign in");
                messageDialog.ShowAsync();
                //הודעה המבשרת שהמשתמש אינו קיים

                //העברת המשתמש לדף הירשמות

            }
            else
            {
                //הצגת הודעה המבשרת שהמשתמש התווסף
                //העברת המשתמש לתפריט המלא
                Frame.Navigate(typeof(MainPage), this.user);
            }
        }
    }
}
