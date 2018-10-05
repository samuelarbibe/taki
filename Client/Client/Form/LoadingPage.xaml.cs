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
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page
    {
        private Game _game;
        private readonly int _playerCount;
        private int _counter;

        public LoadingPage(int playerCount)
        {
            _counter = 0;
            _playerCount = playerCount;

            InitializeComponent();

            SearchGame();
        }

        private void SearchGame()
        {
            _game = MainWindow.Service.StartGame(MainWindow.CurrentUser, _playerCount);

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += FindGame;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            void FindGame(object sender, EventArgs e)
            {

                if (_game == null)
                {
                    status.Text = "player is in queue...";

                    if (_counter < 10)
                    {
                        _game = MainWindow.Service.StartGame(MainWindow.CurrentUser, _playerCount);
                        _counter++;
                    }
                    else
                    {
                        dispatcherTimer.Stop();
                        status.Text = "no game could be found... please try again";
                        status.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
                else // if game is found
                {
                    MainWindow.BigFrame.Navigate(new GamePage(_game));
                    dispatcherTimer.Stop();// stop timer loop
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchGame();
        }

        private void CancelSearchButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.BigFrame.Navigate(new MainMenu(true));
        }
    }
}
