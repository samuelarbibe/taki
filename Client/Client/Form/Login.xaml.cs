using System;
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

namespace Form
{
    /// <summary>
    /// Interaction logic for Login.xaml
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
            string passwordValue = Password.Text;

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
    }
}
