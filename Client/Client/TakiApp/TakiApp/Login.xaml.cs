using System;
using System.Collections.Generic;
using TakiApp.TakiService;

using Xamarin.Forms;

namespace Mobile
{
    public partial class Login : ContentPage
    {
        ServiceClient service;
        public Login()
        {
            InitializeComponent();
            service = new ServiceClient();
            service.LoginCompleted += Serv_LoginCompleted;
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            string usernameValue = Username.Text;
            string passwordValue = Password.Text;

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

                if (e.Error != null) { msg = e.Error.Message; }
                else if(e.Result == null) { msg = "Wrong Username or Password"; }
                else if (e.Cancelled) { msg = "Didn't Work!"; }
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

    }
}
