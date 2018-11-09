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
using System.ServiceModel;
using Form.TakiService;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Form
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        Game _currentGame;
        User _currentUser = MainWindow.CurrentUser;
        Player _currentPlayer;
        Player _table;
        PlayerList _playersList = new PlayerList();
        private MessageList _localMessageList;
        private BackgroundWorker _backgroundProgress;
        private int _playersCount;
        private int _currentPlayerIndex;

        public GamePage(Game game)
        {
            InitializeComponent();
            _currentGame = game;

            ReorderPlayerList();

            DataContext = _playersList;

            uc1.SetCurrentPlayer(_currentPlayer);

            //BoxOne.Text = _currentPlayer.Username;

            switch (_currentGame.Players.Count)
            {
                case 3:
                    uc2.Visibility = Visibility.Hidden;
                    uc3.SetCurrentPlayer(_playersList[1]);
                    uc4.Visibility = Visibility.Hidden;
                    uctable.SetCurrentPlayer(_table);
                    break;
                case 4:
                    uc2.SetCurrentPlayer(_playersList[1]);
                    uc3.SetCurrentPlayer(_playersList[2]);
                    uc4.Visibility = Visibility.Hidden;
                    uctable.SetCurrentPlayer(_table);
                    break;
                case 5:
                    uc2.SetCurrentPlayer(_playersList[1]);
                    uc3.SetCurrentPlayer(_playersList[2]);
                    uc4.SetCurrentPlayer(_playersList[3]);
                    uctable.SetCurrentPlayer(_table);
                    break;
            }

            _backgroundProgress = new BackgroundWorker();
            _backgroundProgress.DoWork += FetchChanges;
            _backgroundProgress.RunWorkerCompleted += BackgroundProcess_RunWorkerCompleted;

            _backgroundProgress.RunWorkerAsync();                             
        }

        private void FetchChanges(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(600);

            _localMessageList = MainWindow.Service.DoAction(_currentGame.Id, _currentPlayer.Id);
        }

        private void BackgroundProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Update UI
            if (_localMessageList != null && _localMessageList.Count != 0)
            {
                foreach (Message m in _localMessageList)
                {
                    switch (m.Action)
                    {
                        case "add":
                            _playersList.Find(p => p.Id == m.Target).Hand.Add(m.Card);

                            break;
                        case "remove":
                            _playersList.Find(p => p.Id == m.Target).Hand.Remove(m.Card);
                            break;
                    }
                }
                //PrintCards();
            }
            // Run again
            _backgroundProgress.RunWorkerAsync();   // This will make the BgWorker run again, and never runs before it is completed.
        }

        public void PrintCards()
        {
            string cards = null;
            foreach (Player p in _playersList)
            {
                cards += "\n Player "+ p.Username +":";
                foreach (Card c in p.Hand)
                {
                    cards += "\n value:" + c.Value + ", color:" + c.Color;
                }
            }
            Console.Write(cards);
        }

        // this function re-arranges the player in a particular way, to make sure that:
        // - list[First] is the current player
        // - all the players after him are arranged in order
        // - list[Last] is the table
        private void ReorderPlayerList()
        {
            _playersCount = _currentGame.Players.Count;

            _currentPlayerIndex = _currentGame.Players.FindIndex(p => p.UserId == _currentUser.Id);

            for (int i = 0; i < _playersCount - 1; i++)
            {
                if (_currentPlayerIndex >= _playersCount - 1)
                {
                    _playersList.Add(_currentGame.Players[_currentPlayerIndex % (_playersCount - 1)]);
                }
                else
                {
                    _playersList.Add(_currentGame.Players[_currentPlayerIndex]);
                }

                _currentPlayerIndex++;
            }

            _playersList.Add(_currentGame.Players.Find(q => q.Username == "table"));

            _currentPlayer = _playersList[0];

            _table = _playersList[_playersCount - 1];
        }


        private void AddAction(object sender, RoutedEventArgs e) // a demo action build, in this case, a card will be moved from the table to the player that pressed;
        {
            MessageList temp = new MessageList();

            for (int i = 0; i < (_playersList.Count - 1); i++) //add for each player, not including the table
            {
                temp.Add(new Message()// add the top card of the table to the current player
                {
                    Action = "add",
                    Target = _currentPlayer.Id,
                    Reciever = _playersList[i].Id,
                    Card = _table.Hand.Last(),
                    GameId = _currentGame.Id
                });

                temp.Add(new Message()// remove the top card from the table
                {
                    Action = "remove",
                    Target = _table.Id,
                    Reciever = _playersList[i].Id,
                    Card = _table.Hand.Last(),
                    GameId = _currentGame.Id
                });
            }
            MainWindow.Service.AddActions(temp);

        }


        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            ExitDialog dialog = new ExitDialog();

            if(dialog.ShowDialog() == true)
            {
                Console.WriteLine("Player removed from the game in the gameList: "+MainWindow.Service.PlayerQuit(_currentPlayer).ToString());
                MainWindow.BigFrame.Navigate(new MainMenu());
            }
        }


    }
}
