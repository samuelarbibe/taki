﻿using System;
using System.Collections.Generic;
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
using System.ServiceModel;
using Form.TakiService;
using Form.Utilities;

namespace Form
{
    /// <summary>
    /// Inter_action logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {

        public Login()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            LoginMenu.LoginFrame.Navigate(new Register());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string usernameValue = Username.Text;
            string passwordValue = Password.Password;

            List<Control> textBoxes = new List<Control>();
            textBoxes.Add(Username);
            textBoxes.Add(Password);

            if (Check.NullCheck(textBoxes))
            {
                User currentUserCheck = MainWindow.Service.Login(usernameValue, passwordValue);

                if (currentUserCheck != null)
                {
                    noUserError.Text = "";
                    MainWindow.CurrentUser = currentUserCheck;
                    MainWindow.BigFrame.Navigate(new MainMenu());
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

        private void AdminFillButton_Click(object sender, RoutedEventArgs e)
        {
            Username.Text = "Samuelov1";
            Password.Password = "123";

            LoginButton_Click(null, null);
        }

        private void User1FillButton_Click(object sender, RoutedEventArgs e)
        {
            Username.Text = "fredg2";
            Password.Password = "Israel123";

            LoginButton_Click(null, null);
        }

        private void User2FillButton_Click(object sender, RoutedEventArgs e)
        {
            Username.Text = "itai";
            Password.Password = "menes";

            LoginButton_Click(null, null);
        }

        private void User3FillButton_Click(object sender, RoutedEventArgs e)
        {
            Username.Text = "el_primo";
            Password.Password = "123456";

            LoginButton_Click(null, null);
        }
    }
}
