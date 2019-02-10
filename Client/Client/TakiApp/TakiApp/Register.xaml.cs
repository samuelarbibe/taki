using System;
using System.Collections.Generic;
using TakiApp.TakiService;
using Xamarin.Forms;

namespace Mobile
{
    public partial class Register : ContentPage
    {
        ServiceClient service;
        public Register()
        {
            InitializeComponent();
            service = new ServiceClient();
            service.RegisterCompleted += Serv_RegisterCompleted;
        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            string firstNameValue = FirstName.Text;
            string lastNameValue = LastName.Text;
            string usernameValue = Username.Text;
            string passwordValue = Password.Text;
            string confirmPasswordValue = ConfirmPassword.Text;

            if (firstNameValue != null && lastNameValue != null && usernameValue != null && passwordValue != null && confirmPasswordValue != null)
            {
                if (passwordValue.Equals(confirmPasswordValue)) //checks if password and confirm password are equal
                {
                    service.RegisterAsync(firstNameValue, lastNameValue, usernameValue, passwordValue);
                }
            }
        }

        private void Serv_RegisterCompleted(object sender, RegisterCompletedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                string msg = null;

                if (e.Error != null) { msg = e.Error.Message; }
                else if(e.Result == false) { msg = "Username or Password Already in Use"; }
                else if (e.Cancelled) { msg = "Didn't work!"; }
                else
                {
                    await this.Navigation.PushModalAsync(new Login());
                }
                this.InsertStatus.Text = msg;
            });
        }

        private void ReturnButton_Click(object sender, System.EventArgs e)
        {
            this.Navigation.PushModalAsync(new Login());
        }           
    }
}
