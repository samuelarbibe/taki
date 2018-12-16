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
        static System.Windows.Threading.DispatcherTimer myTimer = new System.Windows.Threading.DispatcherTimer();

        private bool _myTurn;
        private Game _currentGame;
        private User _currentUser = MainWindow.CurrentUser;
        private Player _currentPlayer;
        private Player _table;
        private PlayerList _playersList = new PlayerList();
        private MessageList _localMessageList;
        private BackgroundWorker _backgroundProgress;
        private BackgroundWorker _saveChanges;
        private List<Card> _deck;

        private bool _active;
        private int _turn;
        private int _playersCount;
        private int _currentPlayerIndex;
        private bool _clockWiseRotation;

        public bool MyTurn { get => _myTurn; set => _myTurn = value; }
        public bool Active { get => _active; set => _active = value; }
        public int Turn { get => _turn; set => _turn = value; }
        public List<Card> Deck { get => _deck; set => _deck = value; }
        public bool ClockWiseRotation { get => _clockWiseRotation; set => _clockWiseRotation = value; }
        public MessageList LocalMessageList { get => _localMessageList; set => _localMessageList = value; }
        public BackgroundWorker BackgroundProgress { get => _backgroundProgress; set => _backgroundProgress = value; }
        public BackgroundWorker SaveChanges1 { get => _saveChanges; set => _saveChanges = value; }
        public int PlayersCount { get => _playersCount; set => _playersCount = value; }
        public int CurrentPlayerIndex { get => _currentPlayerIndex; set => _currentPlayerIndex = value; }

        private PlayerList PlayersList { get => _playersList; set => _playersList = value; }
        private Player Table { get => _table; set => _table = value; }
        private Player CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }
        private User CurrentUser { get => _currentUser; set => _currentUser = value; }
        private Game CurrentGame { get => _currentGame; set => _currentGame = value; }

        public GamePage(Game game)
        {
            InitializeComponent();

            MainWindow.CurrentGamePage = this;

            CurrentGame = game;

            ClockWiseRotation = true;
            MyTurn = false;
            Active = true;

            //the first player is the one to request changes saving in the database every x seconds
            if (_currentUser.Id == CurrentGame.Players[0].UserId){

                InitialTurn();// broadcast that self is the first player in the game's players list

                SetSaveChnages();
            }

            uctable.TakeCardFromDeckButtonClicked += new EventHandler(TakeCardFromDeck);

            uctable.PassCardToStackButtonClicked += new EventHandler(PassCardToDeck);

            ReorderPlayerList();

            Deck = PlayersList[PlayersList.Count - 1].Hand.ToList();

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

            SetBackgroundWorker();
            
            //PrintCards();
        }

        private void SetBackgroundWorker()
        {
            BackgroundProgress = new BackgroundWorker();
            BackgroundProgress.DoWork += FetchChanges;
            BackgroundProgress.RunWorkerCompleted += BackgroundProcess_RunWorkerCompleted;

            BackgroundProgress.RunWorkerAsync();
        }

        private void SetSaveChnages()
        {
            SaveChanges1 = new BackgroundWorker();
            SaveChanges1.DoWork += SaveChanges;
            SaveChanges1.WorkerSupportsCancellation = true;
            SaveChanges1.RunWorkerCompleted += SaveChanges_RunWorkerCompleted;

            SaveChanges1.RunWorkerAsync();
        }

        //save the changes in the database every 2 seconds
        private void SaveChanges(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(500);

            MainWindow.Service.SaveChanges();
        }


        private void SaveChanges_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Active) SaveChanges1.RunWorkerAsync();
        }


        private void FetchChanges(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(200);

            LocalMessageList = MainWindow.Service.DoAction(CurrentGame.Id, CurrentPlayer.Id);
        }


        private void BackgroundProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Active)
            {
                // Update local variables
                if (LocalMessageList != null && LocalMessageList.Count != 0)
                {
                    foreach (Message m in LocalMessageList)
                    {
                        switch (m.ACTION)
                        {
                            case Message.Action.Add:

                                PlayersList.Find(p => p.Id == m.Target).Hand.Add(m.Card);
                                break;

                            case Message.Action.Remove:

                                Player tempPlayer = PlayersList.Find(p => p.Id == m.Target); // the target player
                                Card tempCard = tempPlayer.Hand.Find(c => c.Id == m.Card.Id); // the target card
                                tempPlayer.Hand.Remove(tempCard); // remove the target card from the target player's hand

                                break;

                            case Message.Action.NextTurn:

                                Turn = PlayersList.FindIndex(p => p.Id == m.Target);
                                IsMyTurn(m.Target == CurrentPlayer.Id);

                                DeclareTurn();

                                break;

                            case Message.Action.PlayerQuit:

                                int prevChangeSaverId = PlayersList[0].UserId;

                                Player quitter = PlayersList.Find(p => p.Id == m.Target);
                                PlayersList.Remove(quitter); // remove the quitting player from the local players list

                                PlayersList.TrimExcess();
                                CurrentGame.Players.TrimExcess();

                                if (_currentUser.Id == PlayersList[0].UserId)
                                {
                                    InitialTurn(); // broadcast that self is the first player in the game's players list

                                    uc1.SetAsActive();

                                    // if the player isn't already the player in charge of saving changes
                                    if (_currentUser.Id != prevChangeSaverId)
                                    {
                                        // pass the save changes functionality to the target player.
                                        SetSaveChnages();
                                    }
                                }
                                else
                                {
                                    IsMyTurn(false);
                                }

                                DeclareTurn();

                                break;
                        }
                    }
                    //PrintCards();

                    UpdateUI();
                }

                // Run again
                BackgroundProgress.RunWorkerAsync();   // This will make the BgWorker run again, and never runs before it is completed.
            }
        }

        //public void PrintCards()
        //{
        //    string cards = "\n \n --------------------------------------";
        //    foreach (Player p in PlayersList)
        //    {
        //        cards += "\n \n Player "+ p.Username +":";
        //        foreach (Card c in p.Hand)
        //        {
        //            cards += "\n value:" + c.Value + ", color:" + c.Color;
        //        }
        //    }
        //    Console.Write(cards);
        //}

        // this function re-arranges the player in a particular way, to make sure that:
        // - list[First] is the current player
        // - all the players after him are arranged in order
        // - list[Last] is the table
        private void ReorderPlayerList()
        {
            PlayersCount = CurrentGame.Players.Count;

            CurrentPlayerIndex = CurrentGame.Players.FindIndex(p => p.UserId == CurrentUser.Id);

            for (int i = 0; i < PlayersCount - 1; i++)
            {
                if (CurrentPlayerIndex >= PlayersCount - 1)
                {
                    PlayersList.Add(CurrentGame.Players[CurrentPlayerIndex % (PlayersCount - 1)]);
                }
                else
                {
                    PlayersList.Add(CurrentGame.Players[CurrentPlayerIndex]);
                }

                CurrentPlayerIndex++;
            }

            PlayersList.Add(CurrentGame.Players.Find(q => q.Username == "table"));

            CurrentPlayer = PlayersList[0];

            Table = PlayersList[PlayersCount - 1];
        }

        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            ExitDialog dialog = new ExitDialog
            {
                Owner = Application.Current.MainWindow
            };

            if (dialog.ShowDialog() == true)
            {
                PlayerQuit();
                MainWindow.BigFrame.Navigate(new MainMenu());
            }

        }

        private void UpdateUI()
        {
            uc1.UpdateUI(PlayersList[0]);

            switch (PlayersList.Count)
            {
                case 2:
                    uc2.Visibility = Visibility.Hidden;
                    uc3.Visibility = Visibility.Hidden;
                    uc4.Visibility = Visibility.Hidden;
                    uctable.UpdateUI(PlayersList[1]);

                    ForceQuit dialog = new ForceQuit
                    {
                        Owner = Application.Current.MainWindow
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        PlayerQuit();
                        MainWindow.BigFrame.Navigate(new MainMenu());
                    }

                    break;

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

        private void PassCardToDeck(object sender, EventArgs e)
        {
            Player currentPlayer = PlayersList.First();
            Random rand = new Random();
            Player table = PlayersList.Last();
            MessageList temp = new MessageList();
            Card givenCard = uc1.SelectedCard(); // get a random card

            if (givenCard != null && Utilities.Algorithm.SimpleCheck(givenCard, table.Hand[table.Hand.Count-1]))
            {

                for (int i = 0; i < (PlayersList.Count - 1); i++) //add for each player, not including the table
                {
                    temp.Add(new Message()// add the top card of the table to the current player
                    {
                        ACTION= Message.Action.Add,
                        Target = table.Id, // the person who's hand is modified
                        Reciever = PlayersList[i].Id, // the peron who this message is for
                        Card = givenCard, // the card modified
                        GameId = CurrentGame.Id // the game modified
                    });

                    temp.Add(new Message()// add the top card of the table to the current player
                    {
                        ACTION= Message.Action.Remove,
                        Target = currentPlayer.Id, // the person who's hand is modified
                        Reciever = PlayersList[i].Id, // the peron who this message is for
                        Card = givenCard, // the card modified
                        GameId = CurrentGame.Id // the game modified
                    });
                }

                MainWindow.Service.AddActions(temp);

                TurnFinished(); // give turn to next player
            }
        }


        private void TakeCardFromDeck(object sender, EventArgs e) 
        {
            Player currentPlayer = PlayersList.First();
            Random rand = new Random();
            Player table = PlayersList.Last();
            MessageList temp = new MessageList();
            Card takenCard = Deck[rand.Next(Deck.Count)]; // get a random card

            for (int i = 0; i < (PlayersList.Count - 1); i++) //add for each player, not including the table
            {
                temp.Add(new Message()// add the top card of the table to the current player
                {
                    ACTION= Message.Action.Add,
                    Target = currentPlayer.Id, // the person who's hand is modified
                    Reciever = PlayersList[i].Id, // the peron who this message is for
                    Card = takenCard, // the card modified
                    GameId = CurrentGame.Id // the game modified
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
            if (ClockWiseRotation)
            {
                return PlayersList[PlayersList.Count - 2].Id;// return the next player's Id
            }
            return PlayersList[1].Id;// return the previous player's Id
        }

        public void IsMyTurn(bool value)
        {
            MyTurn = value;

            if (MyTurn) {
                uctable.CanTakeCardFromDeck();
            }
            else uctable.CannotTakeCardFromDeck();
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
                    ACTION = Message.Action.NextTurn, // give next turn to
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
            uc1.SetAsActive();

            MessageList temp = new MessageList();

            for (int i = 0; i < (PlayersList.Count - 1); i++) //add for each player, not including the table
            {
                temp.Add(new Message()
                {
                    ACTION= Message.Action.NextTurn, // give next turn to self
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
            Console.WriteLine("Player"+ CurrentPlayer.Username +" removed from the game in the gameList: " + MainWindow.Service.PlayerQuit(CurrentPlayer).ToString());

            MessageList temp = new MessageList();

            for (int i = 0; i < (PlayersList.Count - 1); i++) //add for each player, not including the table
            {
                temp.Add(new Message()
                {
                    ACTION= Message.Action.PlayerQuit,
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
