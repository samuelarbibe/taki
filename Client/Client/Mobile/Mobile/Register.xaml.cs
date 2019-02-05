using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Mobile
{
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            string firstNameValue = FirstName.Text;
            string lastNameValue = LastName.Text;
            string usernameValue = Username.Text;
            string passwordValue = Password.Text;
            string confirmPasswordValue = ConfirmPassword.Text;

            if (firstNameValue != null && lastNameValue != null && usernameValue != null && passwordValue != null && confirmPasswordValue != null )
            {
                if (passwordValue.Equals(confirmPasswordValue)) //checks if password and confirm password are equal
                {
                //    if (MainWindow.Service.PasswordAvailable(passwordValue)) //checks if this password is used or not
                //    {
                //        PasswordStatus.Text = "";

                //        if (MainWindow.Service.Register(firstNameValue, lastNameValue, usernameValue, passwordValue)) //checks if registration succeeded
                //        {
                //            InsertStatus.Text = "";
                //            MainWindow.BigFrame.Navigate(new LoginMenu());
                //        }
                //        else
                //        {
                //            InsertStatus.Text = "There was an error. we couldn't register you.";
                //        }
                //    }
                //    else
                //    {
                //        PasswordStatus.Text = "password already in use.";
                //    }
                }
                else
                {
                    PasswordStatus.Text = "passwords don't match";
                }
            }
            else
            {
                InsertStatus.Text = "Please fill up all the fields!";
            }
        }

        private void ReturnButton_Click(object sender, System.EventArgs e)
        {
            this.Navigation.PushModalAsync(new Login());
        }           
    }
}
