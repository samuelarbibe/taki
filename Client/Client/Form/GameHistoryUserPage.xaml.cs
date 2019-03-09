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
        public GameHistoryUserPage(int u1)
        {
            GameList gl = MainWindow.Service.GetAllUserGames(u1);

            InitializeComponent();
            DataContext = gl;
            DataGrid.ItemsSource = gl;
        }

        public GameHistoryUserPage(int u1, int u2)
        {
            GameList gl = MainWindow.Service.GetMutualGames(u1, u2);

            InitializeComponent();
            DataContext = gl;
            DataGrid.ItemsSource = gl;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                BackButton.Content = "cannot go back...";
            }
        }
    }
}
