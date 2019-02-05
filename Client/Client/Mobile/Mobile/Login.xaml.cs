using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Mobile
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            string usernameValue = Username.Text;
            string passwordValue = Password.Text;

            if (usernameValue != null && passwordValue != null)
            {
                //User currentUserCheck = MainWindow.Service.Login(usernameValue, passwordValue);
                int currenUserCheck = 1;

                if (currenUserCheck != null)
                {
                    noUserError.Text = "";

                    this.Navigation.PushModalAsync(new MainMenu());
                }
                else
                {
                    noUserError.Text = "Your Password or Username are incorrect. Try again";
                }
            }
            else
            {
                noUserError.Text = "Please fill up all the fields!";
            }
        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            this.Navigation.PushModalAsync(new Register());
        }

    }
}
