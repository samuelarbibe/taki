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
        private User _cu;
        private int _counter;

        public LoadingPage(int playerCount)
        {
            _counter = 0;
            _playerCount = playerCount;
            _cu = MainWindow.CurrentUser;

            InitializeComponent();

            SearchGame();
        }

        private void SearchGame()
        {
            Player p = new Player();

            p.User_id = _cu.Id;
            p.FirstName = _cu.FirstName;
            p.LastName = _cu.LastName;
            p.Username = _cu.Username;
            p.Password = _cu.Password;
            p.Level = _cu.Level;
            p.Score = _cu.Score;
            p.Admin = _cu.Admin;
            p.Wins = _cu.Wins;
            p.Losses = _cu.Losses;
            p.TempScore = 0;


            _game = MainWindow.Service.StartGame(p, _playerCount);

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
                        _game = MainWindow.Service.StartGame(p, _playerCount);
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
