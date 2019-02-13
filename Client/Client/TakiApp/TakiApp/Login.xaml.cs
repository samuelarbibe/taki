using System;
using System.Collections.Generic;
using TakiApp.TakiService;

using Xamarin.Forms;

namespace TakiApp
{
    public partial class Login : ContentPage
    {
        ServiceClient service;
        private string usernameValue;
        private string passwordValue;

        public Login()
        {
            InitializeComponent();
            service = new ServiceClient();
            service.LoginCompleted += Serv_LoginCompleted;
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            usernameValue = Username.Text;
            passwordValue = Password.Text;

            if (usernameValue != null && passwordValue != null)
            {
                service.LoginAsync(usernameValue, passwordValue);
            }
            else
            {
                noUserError.Text = "Please fill up all the fields!";
            }
        }

        private void Serv_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                MainMenu.CurrentUser = null;
                string msg = null;

                if (e.Error != null) { msg = e.Error.Message;
                    Console.WriteLine(msg);
                    }
                else if(e.Result == null) { msg = "Wrong Username or Password"; Console.WriteLine(msg); }
                else if (e.Cancelled) { msg = "Didn't Work!"; Console.WriteLine(msg); }
                else
                {
                    MainMenu.CurrentUser = e.Result as User;
                    await this.Navigation.PushModalAsync(new MainMenu());
                }
                this.noUserError.Text = msg;
            });
        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            this.Navigation.PushModalAsync(new Register());
        }

        private void AdminFillButton_Clicked(object sender, EventArgs e)
        {
            Username.Text = "Samuelov1";
            Password.Text = "123";

            LoginButton_Click(null, null);
        }
    }
}
