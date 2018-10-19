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
        private int _playerCount;
        private Player _p;
        private User _cu;
        private int _counter;
        private bool _gameNotFound;


        public LoadingPage(int playerCount)
        {
            _counter = 0;
            _playerCount = playerCount;
            _cu = MainWindow.CurrentUser;
            _gameNotFound = false;

            InitializeComponent();

            SearchGame();
        }

        private void SearchGame()
        {
            _p = new Player();

            _p.UserId = _cu.Id;
            _p.FirstName = _cu.FirstName;
            _p.LastName = _cu.LastName;
            _p.Username = _cu.Username;
            _p.Password = _cu.Password;
            _p.Level = _cu.Level;
            _p.Score = _cu.Score;
            _p.Admin = _cu.Admin;
            _p.Wins = _cu.Wins;
            _p.Losses = _cu.Losses;
            _p.TempScore = 0;


            _game = MainWindow.Service.StartGame(_p, _playerCount);

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += FindGame;
            dispatcherTimer.Interval = new TimeSpan(0,0,0,0,200); //send request five times every second
            dispatcherTimer.Start();

            void FindGame(object sender, EventArgs e)
            {

                if (_game == null)
                {
                    status.Text = "player is in queue...";

                    if (_counter < 50)
                    {
                        _game = MainWindow.Service.StartGame(_p, _playerCount);
                        _counter++;
                    }
                    else
                    {
                        dispatcherTimer.Stop();
                        status.Text = "no game could be found... please try again";
                        status.Foreground = new SolidColorBrush(Colors.Red);
                        _gameNotFound = true;
                        MainWindow.Service.StopSearchingForGame(_p);
                    }
                }
                else // if game is found
                {
                    MainWindow.BigFrame.Navigate(new GamePage(_game));
                    dispatcherTimer.Stop();// stop timer loop
                }
            }
        }

        private void SearchAgainButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.BigFrame.Navigate(new LoadingPage(_playerCount));
        }

        private void CancelSearchButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Service.StopSearchingForGame(_p);
            MainWindow.BigFrame.Navigate(_gameNotFound ? new MainMenu(true) : new MainMenu());
        }
    }
}
