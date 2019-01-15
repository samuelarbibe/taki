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
using Form.TakiService;

namespace Form
{
    /// <summary>
    /// Inter_action logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private User _cu = MainWindow.CurrentUser;

        public UserPage()
        {
            InitializeComponent();
            
            this.DataContext = _cu;

            LevelProgressBar.Value = _cu.Score % 1000;
            ShowScore.Text = (_cu.Score % 1000) + "/1000";

            if (_cu.Admin)
            {
                AdminButton.Visibility = Visibility;
            }
            else
            {
                AdminButton.Visibility = Visibility.Hidden;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.CurrentUser = null;
            MainWindow.Service.Logout(_cu.Id);
            MainWindow.CurrentUser = null;
            MainWindow.BigFrame.Navigate(new LoginMenu());
        }

        private void Admin_Button_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.MenuFrame.Navigate(new AdminUserPage());
        }

        private void Game_Button_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.MenuFrame.Navigate(new GameHistoryUserPage());
        }
    }
}
