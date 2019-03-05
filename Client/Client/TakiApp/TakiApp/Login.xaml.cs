using System;
using System.Collections.Generic;
using TakiApp.TakiService;

using Xamarin.Forms;

namespace TakiApp
{
    public partial class Login : ContentPage
    {
        ServiceClient _service;
        private string _usernameValue;
        private string _passwordValue;

        public Login()
        {
            InitializeComponent();
            BackgroundColor = Xamarin.Forms.Color.FromRgb(80, 155, 208);
            _service = new ServiceClient();
            _service.LoginCompleted += Serv_LoginCompleted;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            _usernameValue = Username.Text;
            _passwordValue = Password.Text;

            if (_usernameValue != null && _passwordValue != null)
            {
                _service.LoginAsync(_usernameValue, _passwordValue);
            }
            else
            {
                NoUserError.Text = "Please fill up all the fields!";
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
                    await Navigation.PushModalAsync(new MainMenu());
                }
                NoUserError.Text = msg;
            });
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Register());
        }

        private void AdminFillButton_Clicked(object sender, EventArgs e)
        {
            Username.Text = "Samuelov1";
            Password.Text = "123";

            LoginButton_Click(null, null);
        }
    }
}
