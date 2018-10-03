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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private User _cu = MainWindow.CurrentUser;
        private int playerCount;

        public MenuPage()
        {
            InitializeComponent();
            UsernameTextBlock.Text = MainWindow.CurrentUser.Username;

            ProgressBar.Value = _cu.Score % 1000;
            //ShowScore.Text = (_cu.Score % 1000) + "/1000";
            LevelTextBlock.Text = _cu.Level.ToString();
            playerCount = 2;
        }

        public MenuPage(bool failed)
        {
            InitializeComponent();
            UsernameTextBlock.Text = MainWindow.CurrentUser.Username;

            ProgressBar.Value = _cu.Score % 1000;
            //ShowScore.Text = (_cu.Score % 1000) + "/1000";
            LevelTextBlock.Text = _cu.Level.ToString();
            playerCount = 2;
            SearchFailed.Text = "Couldn't find a Game.... please try again";
        }

        private void MultiplayerButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.BigFrame.Navigate(new LoadingPage(playerCount));
        }



        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();

        }
    }
}
