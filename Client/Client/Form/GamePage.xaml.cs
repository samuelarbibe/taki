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
using System.Runtime.Remoting.Channels;
using System.Threading;

namespace Form
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private bool _myTurn;
        private Game _currentGame;
        private User _currentUser = MainWindow.CurrentUser;
        private Player _currentPlayer;
        private Player _table;
        private PlayerList _playersList = new PlayerList();
        private MessageList _localMessageList;
        private BackgroundWorker _backgroundProgress;
        private bool _active;
        private int _turn;
        private int _playersCount;
        private int _currentPlayerIndex;

        private PlayerList PlayersList { get => _playersList; set => _playersList = value; }
        private Player Table { get => _table; set => _table = value; }
        private Player CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }
        private User CurrentUser { get => _currentUser; set => _currentUser = value; }
        private Game CurrentGame { get => _currentGame; set => _currentGame = value; }
        public bool MyTurn { get => _myTurn; set => _myTurn = value; }
        public bool Active { get => _active; set => _active = value; }
        public int Turn { get => _turn; set => _turn = value; }

        public GamePage(Game game)
        {
            InitializeComponent();
            CurrentGame = game;

            MyTurn = false;
            Active = true;

            if (_currentUser.Id == CurrentGame.Players[0].UserId){
                InitialTurn();// broadcast that self is the first player in the game's players list

                uc1.SetAsActive();
            }
            else
            {
                IsMyTurn(false);
            }

            

            ReorderPlayerList();

            DataContext = PlayersList;

            uc1.SetCurrentPlayer(CurrentPlayer);

            switch (CurrentGame.Players.Count)
            {
                case 3:
                    uc2.Visibility = Visibility.Hidden;
                    uc3.SetCurrentPlayer(PlayersList[1]);
                    uc4.Visibility = Visibility.Hidden;
                    uctable.SetCurrentPlayer(Table);
                    break;
                case 4:
                    uc2.SetCurrentPlayer(PlayersList[1]);
                    uc3.SetCurrentPlayer(PlayersList[2]);
                    uc4.Visibility = Visibility.Hidden;
                    uctable.SetCurrentPlayer(Table);
                    break;
                case 5:
                    uc2.SetCurrentPlayer(PlayersList[1]);
                    uc3.SetCurrentPlayer(PlayersList[2]);
                    uc4.SetCurrentPlayer(PlayersList[3]);
                    uctable.SetCurrentPlayer(Table);
                    break;
            }

            _backgroundProgress = new BackgroundWorker();
            _backgroundProgress.DoWork += FetchChanges;
            _backgroundProgress.RunWorkerCompleted += BackgroundProcess_RunWorkerCompleted;

            _backgroundProgress.RunWorkerAsync(); 
            
            //PrintCards();
        }

        private void FetchChanges(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(200);

            _localMessageList = MainWindow.Service.DoAction(CurrentGame.Id, CurrentPlayer.Id);
        }

        private void BackgroundProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Active)
            {
                // Update local variables
                if (_localMessageList != null && _localMessageList.Count != 0)
                {
                    foreach (Message m in _localMessageList)
                    {
                        switch (m.Action)
                        {
                            case Message._action.add:

                                PlayersList.Find(p => p.Id == m.Target).Hand.Add(m.Card);
                                break;

                            case Message._action.remove:

                                Player tempPlayer = PlayersList.Find(p => p.Id == m.Target); // the target player
                                Card tempCard = tempPlayer.Hand.Find(c => c.Id == m.Card.Id); // the target card
                                tempPlayer.Hand.Remove(tempCard); // remove the target card from the target player's hand
                                break;

                            case Message._action.next_turn:

                                Turn = PlayersList.FindIndex(p => p.Id == m.Target);
                                IsMyTurn(m.Target == CurrentPlayer.Id);

                                DeclareTurn();

                                break;

                            case Message._action.player_quit:

                                Player quitter = PlayersList.Find(p => p.Id == m.Target);
                                PlayersList.Remove(quitter); // remove the quitting player from the local players list
                                PlayerQuitDialog dialog = new PlayerQuitDialog(quitter.Username);
                                dialog.ShowDialog();
                                PlayersList.TrimExcess();
                                CurrentGame.Players.TrimExcess();

                                break;
                        }
                    }
                    //PrintCards();

                    UpdateUI();
                }

                // Run again
                _backgroundProgress.RunWorkerAsync();   // This will make the BgWorker run again, and never runs before it is completed.
            }
        }

        public void PrintCards()
        {
            string cards = "\n \n --------------------------------------";
            foreach (Player p in PlayersList)
            {
                cards += "\n \n Player "+ p.Username +":";
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
            _playersCount = CurrentGame.Players.Count;

            _currentPlayerIndex = CurrentGame.Players.FindIndex(p => p.UserId == CurrentUser.Id);

            for (int i = 0; i < _playersCount - 1; i++)
            {
                if (_currentPlayerIndex >= _playersCount - 1)
                {
                    PlayersList.Add(CurrentGame.Players[_currentPlayerIndex % (_playersCount - 1)]);
                }
                else
                {
                    PlayersList.Add(CurrentGame.Players[_currentPlayerIndex]);
                }

                _currentPlayerIndex++;
            }

            PlayersList.Add(CurrentGame.Players.Find(q => q.Username == "table"));

            CurrentPlayer = PlayersList[0];

            Table = PlayersList[_playersCount - 1];
        }

        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            ExitDialog dialog = new ExitDialog();

            if(dialog.ShowDialog() == true)
            {
                Console.WriteLine("Player removed from the game in the gameList: "+ MainWindow.Service.PlayerQuit(CurrentPlayer).ToString());
                PlayerQuit();
                MainWindow.BigFrame.Navigate(new MainMenu());
            }

        }

        private void UpdateUI()
        {
            uc1.UpdateUI(PlayersList[0]);

            switch (PlayersList.Count)
            {
                case 3:
                    uc2.Visibility = Visibility.Hidden;
                    uc3.UpdateUI(PlayersList[1]);
                    uc4.Visibility = Visibility.Hidden;
                    uctable.UpdateUI(PlayersList[2]);
                    break;
                case 4:
                    uc2.UpdateUI(PlayersList[1]);
                    uc3.UpdateUI(PlayersList[2]);
                    uc4.Visibility = Visibility.Hidden;
                    uctable.UpdateUI(PlayersList[3]);
                    break;
                case 5:
                    uc2.UpdateUI(PlayersList[1]);
                    uc3.UpdateUI(PlayersList[2]);
                    uc4.UpdateUI(PlayersList[3]);
                    uctable.UpdateUI(PlayersList[4]);
                    break;
            }
        }


        public void TakeCardFromDeck(object sender, RoutedEventArgs e) // a demo action build, in this case, a card will be moved from the table to the player that pressed;
        {
            Player currentPlayer = PlayersList.First();
            Player table = PlayersList.Last();
            MessageList temp = new MessageList();

            for (int i = 0; i < (PlayersList.Count - 1); i++) //add for each player, not including the table
            {
                temp.Add(new Message()// add the top card of the table to the current player
                {
                    Action = Message._action.add,
                    Target = currentPlayer.Id,
                    Reciever = PlayersList[i].Id,
                    Card = table.Hand.Last(),
                    GameId = CurrentGame.Id
                });

                temp.Add(new Message()// remove the top card from the table
                {
                    Action = Message._action.remove,
                    Target = table.Id,
                    Reciever = PlayersList[i].Id,
                    Card = table.Hand.Last(),
                    GameId = CurrentGame.Id
                });
            }

            MainWindow.Service.AddActions(temp);

            TurnFinished(); // give turn to next player
        }

        private int GetUserControlOfPlayer(int playerIndex)
        {
            if (uc1.CurrentPlayer != null && uc1.CurrentPlayer.Id == PlayersList[playerIndex].Id) return 1;
            if (uc2.CurrentPlayer != null && uc2.CurrentPlayer.Id == PlayersList[playerIndex].Id) return 2;
            if (uc3.CurrentPlayer != null && uc3.CurrentPlayer.Id == PlayersList[playerIndex].Id) return 3;
            return 4;
        }

        public int GetNextPlayerId()
        {
            return PlayersList[1].Id; // return the next player's Id
        }

        public void IsMyTurn(bool MyTurn)
        {
            if (MyTurn) {
                TakeCardFromDeck_Button.Visibility = Visibility.Visible;
            }
            else TakeCardFromDeck_Button.Visibility = Visibility.Hidden;
        }

        private void DeclareTurn() // declare the player with the turn as active
        {
            switch (GetUserControlOfPlayer(Turn))
            {
                case 1:

                    uc1.SetAsActive();
                    uc2.SetAsNonActive();
                    uc3.SetAsNonActive();
                    uc4.SetAsNonActive();
                    break;

                case 2:

                    uc1.SetAsNonActive();
                    uc2.SetAsActive();
                    uc3.SetAsNonActive();
                    uc4.SetAsNonActive();
                    break;

                case 3:

                    uc1.SetAsNonActive();
                    uc2.SetAsNonActive();
                    uc3.SetAsActive();
                    uc4.SetAsNonActive();
                    break;

                case 4:

                    uc1.SetAsNonActive();
                    uc2.SetAsNonActive();
                    uc3.SetAsNonActive();
                    uc4.SetAsActive();
                    break;

            }
        }


        public void TurnFinished()
        {
            MessageList temp = new MessageList();

            for (int i = 0; i < (PlayersList.Count - 1); i++) //add for each player, not including the table
            {
                temp.Add(new Message()
                {
                    Action = Message._action.next_turn, // give next turn to
                    Target = GetNextPlayerId(),
                    Reciever = PlayersList[i].Id,
                    GameId = CurrentGame.Id
                });
            }

            MainWindow.Service.AddActions(temp);

            IsMyTurn(false);
        }

        private void InitialTurn()
        {
            MessageList temp = new MessageList();

            for (int i = 0; i < (PlayersList.Count - 1); i++) //add for each player, not including the table
            {
                temp.Add(new Message()
                {
                    Action = Message._action.next_turn, // give next turn to self
                    Target = CurrentPlayer.Id,
                    Reciever = PlayersList[i].Id,
                    GameId = CurrentGame.Id
                });
            }

            MainWindow.Service.AddActions(temp);

            IsMyTurn(true);
        }

        public void PlayerQuit()
        {
            MessageList temp = new MessageList();

            for (int i = 0; i < (PlayersList.Count - 1); i++) //add for each player, not including the table
            {
                temp.Add(new Message()
                {
                    Action = Message._action.player_quit,
                    Target = CurrentPlayer.Id,
                    Reciever = PlayersList[i].Id,
                    GameId = CurrentGame.Id
                });
            }

            MainWindow.Service.AddActions(temp);

            if (MyTurn) TurnFinished();
            Active = false;
        }
    }
}
