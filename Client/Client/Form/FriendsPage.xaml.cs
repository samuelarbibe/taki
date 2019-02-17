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

namespace Form
{
    /// <summary>
    /// Interaction logic for FriendsPage.xaml
    /// </summary>
    public partial class FriendsPage : Page
    {
        public FriendsPage()
        {
            InitializeComponent();

            UserList ul = MainWindow.Service.GetAllUseFriends(MainWindow.CurrentUser.Id);

            DataGrid.ItemsSource = ul;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
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
    }
}
