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
        private User _cu;
        private int _playerCount;

        public MenuPage()
        {
            InitializeComponent();
            _cu = MainWindow.CurrentUser;
            _playerCount = 2;
            UsernameTextBlock.Text = _cu.Username;


            ProgressBar.Value = _cu.Score % 1000;
            //ShowScore.Text = (_cu.Score % 1000) + "/1000";
            LevelTextBlock.Text = _cu.Level.ToString();
        }

        public MenuPage(bool failed)
        {
            InitializeComponent();
            _cu = MainWindow.CurrentUser;
            _playerCount = 2;
            UsernameTextBlock.Text = MainWindow.CurrentUser.Username;

            ProgressBar.Value = _cu.Score % 1000;

            LevelTextBlock.Text = _cu.Level.ToString();
            SearchFailed.Text = "Couldn't find a Game.... please try again another time";
        }

        private void MultiplayerButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.BigFrame.Navigate(new LoadingPage(_playerCount));
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.MenuFrame.Navigate(new UserPage());
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.MenuFrame.Navigate(new SettingsPage());
        }

        private void PlayerCountComboBox_DropDownClosed(object sender, EventArgs e)
        {
            _playerCount = Int32.Parse(PlayerCountComboBox.Text);
        }
    }
}
