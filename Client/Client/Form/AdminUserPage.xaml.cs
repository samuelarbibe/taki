using System.Windows;
using System.Windows.Controls;
using Form.TakiService;

namespace Form
{
    /// <summary>
    /// Inter_action logic for AdminUserPage.xaml
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
