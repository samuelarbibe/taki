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
    public partial class Menu : Page
    {
        private User _cu = MainWindow.CurrentUser;

        public Menu()
        {
            InitializeComponent();
            UsernameTextBlock.Text = MainWindow.CurrentUser.Username;

            ProgressBar.Value = _cu.Score % 1000;
            //ShowScore.Text = (_cu.Score % 1000) + "/1000";
            LevelTextBlock.Text = _cu.Level.ToString();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.MenuFrame.Navigate(new UserPage());
        }
    }
}
