using Form.TakiService;
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
using MaterialDesignThemes;

namespace Form
{
    /// <summary>
    /// Interaction logic for FriendsPage.xaml
    /// </summary>
    public partial class FriendsPage : Page
    {
        private UserList ul;

        public FriendsPage()
        {
            InitializeComponent();

             ul = MainWindow.Service.GetAllUseFriends(MainWindow.CurrentUser.Id);

            View.ItemsSource = ul;
        }

        protected void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                BackButton.Content = "cannot go back...";
            }
        }

        private void RemoveFriendButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            User u2 = b.DataContext as User;

            MainWindow.Service.RemoveFriend(MainWindow.CurrentUser, u2);

            View.ItemsSource = null;

            ul.Remove(ul.Find(u => u.Id == u2.Id));

            View.ItemsSource = ul;
        }
    }
}
