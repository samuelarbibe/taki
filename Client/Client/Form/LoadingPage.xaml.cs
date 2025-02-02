﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Inter_action logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page
    {
        private Game _game;
        private int _playerCount;
        private Player _p;
        private User _cu;
        private int _counter;
        private bool _gameNotFound;
        private System.Windows.Threading.DispatcherTimer _dispatcherTimer;


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
            _counter = 0;
            _p = new Player()
            {
                UserId = _cu.Id,
                FirstName = _cu.FirstName,
                LastName = _cu.LastName,
                Username = _cu.Username,
                Password = _cu.Password,
                Level = _cu.Level,
                ProfileImage = _cu.ProfileImage,
                Score = _cu.Score,
                Admin = _cu.Admin,
                Wins = _cu.Wins,
                Losses = _cu.Losses,
                TempScore = 0
            };

            //_game = MainWindow.Service.StartGame(_p, _playerCount);

            _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            _dispatcherTimer.Tick += FindGame;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500); //send request five times every second
            _dispatcherTimer.Start();


            void FindGame(object sender, EventArgs e)
            {

                if (_game == null)
                {
                    status.Text = "player is in queue...";
                    PlayersFound.Text = "Players Found: 0/" + _playerCount;


                    if (_counter < 20)
                    {
                        _game = MainWindow.Service.StartGame(_p, _playerCount);
                        _counter++;
                        PlayersFound.Text = "Players Found: "+ MainWindow.Service.GetPlayersFound(_playerCount) + "/" + _playerCount;
                    }
                    else
                    {
                        _dispatcherTimer.Stop();
                        status.Text = "no game could be found... please try again";
                        _gameNotFound = true;
                    }
                }
                else // if game is found
                {
                    MainWindow.BigFrame.Navigate(new GamePage(_game));
                    _dispatcherTimer.Stop();// stop timer loop
                }
            }
        }

        private void SearchAgainButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.BigFrame.Navigate(new LoadingPage(_playerCount));
        }

        private void CancelSearchButton_Click(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Stop();
            MainWindow.Service.StopSearchingForGame(_p);
            MainWindow.BigFrame.Navigate(_gameNotFound ? new MainMenu(true) : new MainMenu());
        }
    }
}
