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
    /// Interaction logic for GameHistoryUserPage.xaml
    /// </summary>
    public partial class GameHistoryUserPage : Page
    {
        public GameHistoryUserPage()
        {
            GameList gl = MainWindow.Service.GetAllUserGames(MainWindow.CurrentUser.Id);

            InitializeComponent();
            this.DataContext = gl;
            this.DataGrid.ItemsSource = gl;
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
