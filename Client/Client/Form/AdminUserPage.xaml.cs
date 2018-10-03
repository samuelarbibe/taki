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
    /// Interaction logic for AdminUserPage.xaml
    /// </summary>
    public partial class AdminUserPage : Page
    {
        public AdminUserPage()
        {
            UserList cu = MainWindow.Service.GetAllUsers();

            InitializeComponent();
            this.DataContext = cu;
            DataGrid.ItemsSource = cu;
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
