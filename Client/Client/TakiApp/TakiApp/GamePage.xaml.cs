using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Form.Dialogs;
using TakiApp.TakiService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = TakiApp.TakiService.Color;

namespace TakiApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage
    {
        private ServiceClient Service;
        private bool _myTurn;
        private PlayerList PlayersList { get; set; } = new PlayerList();
        private Player Table { get; set; }
        private Player CurrentPlayer { get; set; }
        private User CurrentUser { get; set; } = MainMenu.CurrentUser;
        private Game CurrentGame { get; set; }

        public bool MyTurn
        {
            get => _myTurn;
            set
            {
                _myTurn = value;

                if (MyTurn)
                {
                    uctable.CanTakeCardFromDeck();
                }
                else uctable.CannotTakeCardFromDeck();
            }
        }
        public bool Active { get; set; }
        public bool ClockWiseRotation { get; set; }
        public int Turn { get; set; }
        public int CurrentPlayerIndex { get; set; }
        public int PlayersCount { get; set; }
        public int PlusValue { get; set; }
        public Card OpenTaki { get; set; }
        public List<Card> Deck { get; set; }
        public MessageList LocalMessageList { get; set; }
        public BackgroundWorker BackgroundProgress { get; set; }

        public GamePage(Game game)
        {
            InitializeComponent();

            Service = new ServiceClient();

            MainMenu.CurrentGamePage = this;

            CurrentGame = game;

            Service.DoActionCompleted += Service_ActionCompleted;

            ClockWiseRotation = true;
            uc1.SetAsNonActive();
            OpenTaki = null;
            MyTurn = false;
            Active = true;
            PlusValue = 0;

            int firstPlayerUserId = CurrentGame.Players[0].UserId;
            ReorderPlayerList();

            //the first player is the one to request changes saving in the database every x seconds
            if (CurrentUser.Id == firstPlayerUserId)
            {
                InitialTurn();// broadcast that self is the first player in the game's players list
            }

            uctable.TakeCardFromDeckButtonClicked += TakeCardFromDeck;

            uctable.PassCardToStackButtonClicked += PassCardToDeck;

            Deck = PlayersList[PlayersList.Count - 1].Hand.ToList();

            BindingContext = PlayersList;

            uc1.SetCurrentPlayer(CurrentPlayer);

            switch (CurrentGame.Players.Count)
            {
                case 3:
                    uc2.IsVisible = false;
                    uc3.SetCurrentPlayer(PlayersList[1]);
                    uc4.IsVisible = false;
                    uctable.SetCurrentPlayer(Table);
                    break;
                case 4:
                    uc2.SetCurrentPlayer(PlayersList[1]);
                    uc3.SetCurrentPlayer(PlayersList[2]);
                    uc4.IsVisible = false;
                    uctable.SetCurrentPlayer(Table);
                    break;
                case 5:
                    uc2.SetCurrentPlayer(PlayersList[1]);
                    uc3.SetCurrentPlayer(PlayersList[2]);
                    uc4.SetCurrentPlayer(PlayersList[3]);
                    uctable.SetCurrentPlayer(Table);
                    break;
            }

            Service.DoActionAsync(CurrentGame.Id, CurrentPlayer.Id);
        }

        private void Service_ActionCompleted(object sender, DoActionCompletedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                string msg = null;

                if (e.Error != null){msg = e.Error.Message;}
                else if (e.Result == null) { msg = "No Messages awaiting.";  }
                else if (e.Cancelled) { msg = "Didn't Work!";  }
                else
                {
                    LocalMessageList = e.Result;                  
                }
                if (Active)
                {
                    Thread.Sleep(250);
                    DoActions();
                    
                }              
                Console.WriteLine(msg);
            });
        }

        private void DoActions()
        {
            if (Active)
            {
                // Update local variables
                if (LocalMessageList != null && LocalMessageList.Count != 0)
                {
                    foreach (Message m in LocalMessageList)
                    {
                        switch (m.Action)
                        {
                            case TakiService.Action.Add:


                                PlayersList.First(p => p.Id == m.Target.Id).Hand.Add(m.Card);
                                break;

                            case TakiService.Action.Remove:

                                Player tempPlayer = PlayersList.First(p => p.Id == m.Target.Id); // the target player
                                Card tempCard = tempPlayer.Hand.First(c => c.Id == m.Card.Id); // the target card
                                tempPlayer.Hand.Remove(tempCard); // remove the target card from the target player's hand

                                break;

                            case TakiService.Action.Win:

                                Player WinningPlayer = PlayersList.First(p => p.Id == m.Target.Id); // the winning player

                                if (CurrentPlayer.Id == m.Target.Id)
                                {
                                    PlayerWin();
                                    PlayerQuit();
                                    this.Navigation.PushModalAsync(new MainMenu());
                                }
                                else
                                {
                                    DisplayAlert("Winner", "Player " + WinningPlayer.Username + " Has won!", "OK");

                                    if (PlayersList.Count == 3)
                                    {
                                        PlayerLoss();

                                        Service.AddActionAsync(new Message()
                                        {
                                            Action = TakiService.Action.Loss,
                                            Target = CurrentPlayer,
                                            Reciever = CurrentPlayer.Id,
                                            GameId = CurrentGame.Id
                                        });
                                    }

                                }

                                break;

                            case TakiService.Action.SwitchHand:

                                CardList l1 = ((Player)PlayersList.First(p => p.Id == m.Target.Id)).Hand;

                                PlayersList.First(p => p.Id == m.Target.Id).Hand = PlayersList.First(p => p.Id == m.Card.Id).Hand;
                                PlayersList.First(p => p.Id == m.Card.Id).Hand = l1;

                                break;

                            case TakiService.Action.PlusTwo:

                                PlusValue = m.Card.Id;

                                if (CurrentPlayer.Id == m.Target.Id && PlusValue != 0)
                                {
                                    if (CurrentPlayer.Hand.FirstOrDefault(c => c.Value == Value.PlusTwo) == null)
                                    {
                                        TakeMultipleCardsFromDeck(PlusValue);
                                    }
                                }

                                break;

                            case TakiService.Action.NextTurn:

                                Turn = PlayersList.IndexOf(PlayersList.First(p => p.Id == m.Target.Id));
                                MyTurn = (m.Target.Id == CurrentPlayer.Id);

                                Win();

                                DeclareTurn();

                                break;

                            case TakiService.Action.SwitchRotation:

                                if (ClockWiseRotation) ClockWiseRotation = false;
                                else ClockWiseRotation = true;

                                break;

                            case TakiService.Action.PlayerQuit:

                                Player quitter = PlayersList.First(p => p.Id == m.Target.Id);
                                PlayersList.Remove(quitter); // remove the quitting player from the local players list

                                //PlayersList.TrimExcess(); 
                                //CurrentGame.Players.TrimExcess();

                                if (CurrentUser.Id == PlayersList[0].UserId)
                                {
                                    InitialTurn(); // broadcast that self is the first player in the game's players list

                                    uc1.SetAsActive();
                                }
                                else
                                {
                                    MyTurn = false;
                                }

                                DeclareTurn();

                                break;
                        }
                    }

                    UpdateUI();
                }
                Service.DoActionAsync(CurrentGame.Id, CurrentPlayer.Id);
            }
        }

        public void PrintCards()
        {
            string cards = "\n \n --------------------------------------";
            foreach (Player p in PlayersList)
            {
                cards += "\n \n Player " + p.Username + ":";
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
            PlayersCount = CurrentGame.Players.Count;
            
            CurrentPlayerIndex = CurrentGame.Players.Select((item, i) => new { Item = item, Index = i }).First(p => p.Item.UserId == CurrentUser.Id).Index;

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

            PlayersList.Add(CurrentGame.Players.First(q => q.Username == "table"));

            CurrentPlayer = PlayersList[0];

            Table = PlayersList[PlayersCount - 1];
        }

        private async void ExitGameButton_Click(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Quit", "Are you sure you want to quit? all progress will be lost", "Yes", "No");

            if (answer)
            {
                PlayerQuit();
                await this.Navigation.PushModalAsync(new MainMenu());
            }

        }

        private void UpdateUI()
        {
            uc1.UpdateUI(PlayersList[0]);

            switch (PlayersList.Count)
            {
                case 2:
                    uc2.IsVisible = false;
                    uc3.IsVisible = false;
                    uc4.IsVisible = false;
                    uctable.UpdateUI(PlayersList[1]);

                    var answer = DisplayAlert("Force Quit", "You are the last player left. you have to quit!", "OK");

                    if (answer.IsCanceled)
                    {
                        PlayerQuit();
                        this.Navigation.PushModalAsync(new MainMenu());
                    }

                    break;

                case 3:
                    uc2.IsVisible = false;
                    uc3.UpdateUI(PlayersList[1]);
                    uc4.IsVisible = false;
                    uctable.UpdateUI(PlayersList[2]);
                    break;
                case 4:
                    uc2.UpdateUI(PlayersList[1]);
                    uc3.UpdateUI(PlayersList[2]);
                    uc4.IsVisible = false;
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
            Player table = PlayersList.Last();
            MessageList temp = new MessageList();
            Card givenCard = uc1.SelectedCard;

            if (givenCard != null)
            {
                if (givenCard.Value == Value.TakiAll || givenCard.Value == Value.Taki)
                {
                    OpenTaki = givenCard;
                }

                if (CheckPlay(givenCard, table.Hand[table.Hand.Count - 1]))
                {
                    for (int i = 0; i < PlayersList.Count; i++) //add for each player, not including the table
                    {
                        if (givenCard.Value != Value.SwitchHandAll) // don't add "SwitchHandsAll" card to table
                        {
                            temp.Add(new Message() // add the top card of the table to the current player
                            {
                                Action = TakiService.Action.Add,
                                Target = table, // the person who's hand is modified
                                Reciever = PlayersList[i].Id, // the peron who this message is for
                                Card = givenCard, // the card modified
                                GameId = CurrentGame.Id // the game modified
                            });

                            temp.Add(new Message() // add the top card of the table to the current player
                            {
                                Action = TakiService.Action.Remove,
                                Target = CurrentPlayer, // the person who's hand is modified
                                Reciever = PlayersList[i].Id, // the peron who this message is for
                                Card = givenCard, // the card modified
                                GameId = CurrentGame.Id // the game modified
                            });
                        }
                    }

                    Service.AddActionsAsync(temp);

                    Switch(givenCard);
                }
            }
        }

        private void TakeCardFromDeck(object sender, EventArgs e)
        {
            if (PlusValue != 0)
            {
                TakeMultipleCardsFromDeck(PlusValue);
            }
            else
            {
                Player currentPlayer = PlayersList.First();
                MessageList temp = new MessageList();
                Card takenCard = uctable.GetCardFromStack(); // get a random card

                for (int i = 0; i < PlayersList.Count; i++) //add for each player, not including the table
                {
                    temp.Add(new Message()// add the top card of the table to the current player
                    {
                        Action = TakiService.Action.Add,
                        Target = CurrentPlayer, // the person who's hand is modified
                        Reciever = PlayersList[i].Id, // the peron who this message is for
                        Card = takenCard, // the card modified
                        GameId = CurrentGame.Id // the game modified
                    });

                    temp.Add(new Message()// add the top card of the table to the current player
                    {
                        Action = TakiService.Action.Remove,
                        Target = Table, // the person who's hand is modified
                        Reciever = PlayersList[i].Id, // the peron who this message is for
                        Card = takenCard, // the card modified
                        GameId = CurrentGame.Id // the game modified
                    });
                }

                Service.AddActionsAsync(temp);

                TurnFinished(Value.Nine); // give turn to next player
            }
        }

        private void TakeMultipleCardsFromDeck(int num)
        {
            Player currentPlayer = PlayersList.First();
            MessageList temp = new MessageList();

            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < PlayersList.Count; j++) //add for each player, not including the table
                {
                    temp.Add(new Message()// add the top card of the table to the current player
                    {
                        Action = TakiService.Action.Add,
                        Target = CurrentPlayer, // the person who's hand is modified
                        Reciever = PlayersList[j].Id, // the peron who this message is for
                        Card = uctable.GetCardFromStack(), // the card modified
                        GameId = CurrentGame.Id // the game modified
                    });
                }
            }

            Service.AddActionsAsync(temp);

            PlusTwoMessage(0); // finished taking cards

            TurnFinished(Value.Nine); // give turn to next player
        }

        private int GetUserControlOfPlayer(int playerIndex)
        {
            if (uc1.CurrentPlayer != null && uc1.CurrentPlayer.Id == PlayersList[playerIndex].Id) return 1;
            if (uc2.CurrentPlayer != null && uc2.CurrentPlayer.Id == PlayersList[playerIndex].Id) return 2;
            if (uc3.CurrentPlayer != null && uc3.CurrentPlayer.Id == PlayersList[playerIndex].Id) return 3;
            return 4;
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


        public void TurnFinished(Value value)
        {
            MessageList temp = new MessageList();

            for (int i = 0; i < PlayersList.Count; i++) //add for each player, not including the table
            {
                temp.Add(new Message()
                {
                    Action = TakiService.Action.NextTurn, // give next turn to
                    Target = PlayersList.First(p => p.Id == GetNextPlayerId(value)),
                    Reciever = PlayersList[i].Id,
                    GameId = CurrentGame.Id
                });
            }

            Service.AddActionsAsync(temp);

            MyTurn = false;
        }

        private void InitialTurn()
        {
            uc1.SetAsActive();

            MessageList temp = new MessageList();

            for (int i = 0; i < PlayersList.Count; i++) //add for each player, not including the table
            {
                temp.Add(new Message()
                {
                    Action = TakiService.Action.NextTurn, // give next turn to self
                    Target = CurrentPlayer,
                    Reciever = PlayersList[i].Id,
                    GameId = CurrentGame.Id
                });
            }

            Service.AddActionsAsync(temp);

            MyTurn = true;
        }

        public void PlayerQuit()
        {
            Service.PlayerQuitAsync(CurrentPlayer);

            Console.WriteLine("Player #" + CurrentPlayer.Username + " removed from the game in the gameList: ");

            MessageList temp = new MessageList();

            for (int i = 0; i < PlayersList.Count; i++) //add for each player, not including the table
            {
                temp.Add(new Message()
                {
                    Action = TakiService.Action.PlayerQuit,
                    Target = CurrentPlayer,
                    Reciever = PlayersList[i].Id,
                    GameId = CurrentGame.Id
                });
            }

            Service.AddActionsAsync(temp);

            if (MyTurn) TurnFinished(Value.Nine);
            Active = false;

            this.Navigation.PushModalAsync(new MainMenu());
        }


        private bool CheckPlay(Card given, Card table)
        {
            if (PlusValue != 0)
            {
                if (given.Value == table.Value) return true;
            }

            // not Multi-color
            if (given.Color == Color.Multi || table.Value == Value.TakiAll || given.Color == table.Color || given.Value == table.Value) return true;

            return false;
        }

        private void Switch(Card givenCard)
        {
            if (OpenTaki != null)
            {
                int colorCount = CurrentPlayer.Hand.Count(c => c.Color == OpenTaki.Color || c.Value == Table.Hand[Table.Hand.Count - 1].Value) - 1;

                if (givenCard.Value == Value.TakiAll)
                {
                    colorCount = 2;
                }

                if (OpenTaki.Value == Value.TakiAll && givenCard.Value != Value.TakiAll)
                {
                    switch (givenCard.Color)
                    {
                        case Color.Blue:
                            OpenTaki = new Card()
                            {
                                Id = 61,
                                Color = Color.Blue,
                                Image = "../Resources/Cards/card0061.png",
                                Special = true,
                                Value = Value.Taki
                            };
                            break;
                        case Color.Green:
                            OpenTaki = new Card()
                            {
                                Id = 13,
                                Color = Color.Green,
                                Image = "../Resources/Cards/card0013.png",
                                Special = true,
                                Value = Value.Taki
                            };
                            break;
                        case Color.Red:
                            OpenTaki = new Card()
                            {
                                Id = 29,
                                Color = Color.Red,
                                Image = "../Resources/Cards/card0029.png",
                                Special = true,
                                Value = Value.Taki
                            };
                            break;
                        case Color.Yellow:
                            OpenTaki = new Card()
                            {
                                Id = 45,
                                Color = Color.Yellow,
                                Image = "../Resources/Cards/card0045.png",
                                Special = true,
                                Value = Value.Taki
                            };
                            break;
                    }
                }

                if (colorCount == 0)
                {
                    OpenTaki = null;
                    TurnFinished(Value.Nine);
                }
                else
                {
                    if (colorCount == 1) // if one playing options is left in open taki
                    {
                        OpenTaki = null;
                    }

                    TurnFinished(Value.Plus);
                }
            }
            else
            {
                switch (givenCard.Value)
                {
                    case Value.SwitchColor:
                    case Value.SwitchColorAll:

                        PromptSwitchColor();

                        break;

                    case Value.PlusTwo:

                        PlusTwoMessage(PlusValue + 2);

                        break;

                    case Value.SwitchDirection:

                        ChangeRotationMessage();

                        break;

                    case Value.SwitchHandAll:

                        SwitchHandsMessage();

                        break;
                }

                TurnFinished(givenCard.Value); // GetNextPlayerId will handle this 
            }

        }

        private async void PromptSwitchColor()
        {
            var action = await DisplayActionSheet("Choose a Color:", null, null, "Green", "Blue", "Yellow", "Red");

            switch (action)
            {
                case "Green":
                    SwitchColorMessage(new Card()
                    {
                        Id = 14,
                        Value = Value.SwitchColor,
                        Color = Color.Green,
                        Image = "../Resources/Cards/card0014.png",
                        Special = true
                    });
                    break;

                case "Blue":
                    SwitchColorMessage(new Card()
                    {
                        Id = 62,
                        Value = Value.SwitchColor,
                        Color = TakiService.Color.Blue,
                        Image = "../Resources/Cards/card0062.png",
                        Special = true
                    });
                    break;

                case "Red":
                    SwitchColorMessage(new Card()
                    {
                        Id = 30,
                        Value = Value.SwitchColor,
                        Color = TakiService.Color.Red,
                        Image = "../Resources/Cards/card0030.png",
                        Special = true
                    });
                    break;

                case "Yellow":
                    SwitchColorMessage(new Card()
                    {
                        Id = 46,
                        Value = Value.SwitchColor,
                        Color = TakiService.Color.Red,
                        Image = "../Resources/Cards/card0046.png",
                        Special = true
                    });
                    break;
            }

        }

        private void SwitchHandsMessage()
        {
            MessageList temp = new MessageList();

            for (int i = 0; i < (PlayersList.Count - 1); i++) //add for each player, not including the table
            {
                temp.Add(new Message() // add the top card of the table to the current player
                {
                    Action = TakiService.Action.Remove,
                    Target = CurrentPlayer, // the person who's hand is modified
                    Reciever = PlayersList[i].Id, // the peron who this message is for
                    Card = new Card() { Id = 67 }, // the card modified
                    GameId = CurrentGame.Id // the game modified
                });

                temp.Add(new Message()
                {
                    Action = TakiService.Action.SwitchHand,
                    Target = CurrentPlayer,
                    Card = new Card() { Id = GetNextPlayerId(Value.Nine) }, // pass the other Player's ID through the card field.
                    Reciever = PlayersList[i].Id,
                    GameId = CurrentGame.Id
                });
            }
            Service.AddActionsAsync(temp);
        }

        private void SwitchColorMessage(Card selectedColorCard)
        {
            MessageList temp = new MessageList();

            for (int i = 0; i < PlayersList.Count; i++) //add for each player, not including the table
            {
                temp.Add(new Message()
                {
                    Action = TakiService.Action.Add,
                    Target = Table,
                    Reciever = PlayersList[i].Id,
                    GameId = CurrentGame.Id,
                    Card = selectedColorCard
                });
            }

            Service.AddActionsAsync(temp);
        }

        private void PlusTwoMessage(int num)
        {
            MessageList temp = new MessageList();

            for (int i = 0; i < PlayersList.Count; i++) //add for each player, not including the table
            {
                temp.Add(new Message()
                {
                    Action = TakiService.Action.PlusTwo,
                    Target = PlayersList.First(p => p.Id == GetNextPlayerId(Value.Nine)), // get the next player
                    Reciever = PlayersList[i].Id,
                    GameId = CurrentGame.Id,
                    Card = new Card() { Id = num }// the card's id represents the PlusValue Build-up
                });
            }

            Service.AddActionsAsync(temp);
        }

        private void ChangeRotationMessage()
        {
            MessageList temp = new MessageList();

            for (int i = 0; i < PlayersList.Count; i++) //add for each player, not including the table
            {
                temp.Add(new Message()
                {
                    Action = TakiService.Action.SwitchRotation,
                    Target = CurrentPlayer,
                    Reciever = PlayersList[i].Id,
                    GameId = CurrentGame.Id
                });
            }


            Service.AddActionsAsync(temp);
        }


        private int GetNextPlayerId(Value value)
        {
            // special card switch - case
            switch (value)
            {
                case Value.Stop:
                    if (PlayersList.Count > 3)
                    {
                        if (ClockWiseRotation) return PlayersList[PlayersList.Count - 3].Id;
                        return PlayersList[2].Id;
                    }
                    return CurrentPlayer.Id;

                case Value.Plus:
                case Value.Taki:
                case Value.TakiAll:
                    return CurrentPlayer.Id;

                case Value.SwitchDirection:
                    {
                        if (!ClockWiseRotation) return PlayersList[PlayersList.Count - 2].Id;
                        return PlayersList[1].Id;
                    }

            }

            if (ClockWiseRotation) return PlayersList[PlayersList.Count - 2].Id;
            return PlayersList[1].Id;

        }

        private void Win()
        {
            if (CurrentPlayer.Hand.Count == 0)
            {
                MessageList temp = new MessageList();

                for (int i = 0; i < PlayersList.Count; i++) //add for each player, not including the table
                {
                    temp.Add(new Message()
                    {
                        Action = TakiService.Action.Win,
                        Target = CurrentPlayer,
                        Reciever = PlayersList[i].Id,
                        GameId = CurrentGame.Id
                    });
                }
                Service.AddActionsAsync(temp);
            }
        }

        private void PlayerWin()
        {
            CurrentUser.Score += 500;
            CurrentUser.Wins += 1;
            CurrentUser.Level = (CurrentUser.Score - CurrentUser.Score % 1000) / 1000;
        }

        private void PlayerLoss()
        {
            CurrentUser.Score += 200;
            CurrentUser.Losses += 1;
            CurrentUser.Level = (CurrentUser.Score - CurrentUser.Score % 1000) / 1000;
        }
    }
}