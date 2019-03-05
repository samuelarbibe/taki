using System;
using System.Collections.Generic;
using TakiApp;
using TakiApp.TakiService;
using Xamarin.Forms;

namespace TakiApp
{
    public partial class Register : ContentPage
    {
        ServiceClient _service;
        public Register()
        {
            InitializeComponent();
            _service = new ServiceClient();
            _service.RegisterCompleted += Serv_RegisterCompleted;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
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
                    _service.RegisterAsync(firstNameValue, lastNameValue, usernameValue, passwordValue);
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
                    await Navigation.PushModalAsync(new Login());
                }
                InsertStatus.Text = msg;
            });
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Login());
        }           
    }
}
