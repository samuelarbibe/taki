using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Form.Utilities;

namespace Form
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {


        public Register()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string firstNameValue = FirstName.Text;
            string lastNameValue = LastName.Text;
            string usernameValue = Username.Text;
            string passwordValue = Password.Text; 
            string confirmPasswordValue = ConfirmPassword.Text;

            List<Control> inputList = new List<Control>();
            inputList.Add(FirstName);
            inputList.Add(LastName);
            inputList.Add(Username);
            inputList.Add(Password);
            inputList.Add(ConfirmPassword);

            if (Check.nullCheck(inputList))
            {
                if (passwordValue.Equals(confirmPasswordValue)) //checks if password and confirm password are equal
                {
                    if (MainWindow.Service.PasswordAvailable(passwordValue)) //checks if this password is used or not
                    {
                        PasswordStatus.Text = "";

                        if (MainWindow.Service.Register(firstNameValue, lastNameValue, usernameValue, passwordValue)
                        ) //checks if registration succeeded
                        {
                            InsertStatus.Text = "";
                            MainWindow.BigFrame.Navigate(new LoginMenu());
                        }
                        else
                        {
                            InsertStatus.Text = "There was an error. we couldn't register you.";
                        }
                    }
                    else
                    {
                        PasswordStatus.Text = "password already in use.";
                    }
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.BigFrame.Navigate(new LoginMenu());
        }
    }
}
