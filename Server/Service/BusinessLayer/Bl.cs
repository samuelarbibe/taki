using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Model;
using ViewModel;


namespace BusinessLayer
{
    public class Bl
    {
        private static List<PlayerList> _waitingList = new List<PlayerList>
        {
            // creating a list of waiting lists
            new PlayerList(), //waitingList[0] = players looking for 2 player games
            new PlayerList(), //waitingList[1] = players looking for 3 player games
            new PlayerList() //waitingList[2] = players looking for 4 player games
        };


        private static CardDb _cardDb = new CardDb();
          
        private static CardList _deck = _cardDb.SelectAll();
          
        private static GameList _gameList = new GameList();

        private static Dictionary<Player, int> _standbyPlayers = new Dictionary<Player, int>();
          
        private static Game _game;

        public Card BlBuildDeck()
        {
            CardDb db = new CardDb();
            CardList deck = db.SelectAll();
            return deck[0];
        }

        //receives a Message from the service, calculates according to the algorithms
        //makes changes to the database 
        //returns a messageList to all players  
        public Message Action(MessageList list)
        {
            //return a message
            return null;
        }

        public User BlLogin(string username, string password)
        {
            UserDb db = new UserDb();
            UserList userList = db.SelectByUsernameAndPassword(username, password);
            if (userList.Count > 0)
            {
                return userList[0];
            }

            return null;
        }

        public bool BlRegister(string firstName, string lastName, string username, string password)
        {
            UserDb db = new UserDb();
            User newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };

            db.Insert(newUser);

            if (db.SaveChanges() == 0) // Check if user was inserted, and it exists
            {
                return false;
            }

            return true;
        }

        public bool BlPasswordAvailable(string password)
        {
            UserDb db = new UserDb();
            User user = db.SelectByPassword(password);
            return (user == null); // return true if there is no user with this password
        }

        public bool BlUsernameAvailable(string username)
        {
            UserDb db = new UserDb();
            User user = db.SelectByUsername(username);
            return (user == null); //return true if there is no user with this username
        }

        public UserList BlGetAllUsers()
        {
            UserDb db = new UserDb();
            UserList userList = db.SelectAll();
            return userList;
        }

        // creates a new game and returns it
        // creates a new Game in the GameDB, and creates new connections in PlayerGameDB
        public Game BlStartGame(Player p, int playerCount)
        {
            //if there is a game in gameList containing this player and if game is active
            Game temp = _gameList.Find(g => g.Players.Find(q => q.UserId == p.UserId) != null);

            // return the game to the player!
            if (temp != null) {
                if (_waitingList[playerCount - 2].Count > 0) _waitingList[playerCount - 2].Clear();
                return temp;
            }

            // if the list is null, create a new one.
            //if (_waitingList[playerCount - 2] == null) _waitingList[playerCount - 2] = new PlayerList();

            // if the list contains a table, clear it! its old!
            if (_waitingList[playerCount - 2].Count > playerCount) _waitingList[playerCount - 2].Clear();


            // if the player isn't in the waiting list add him.
            if (_waitingList[playerCount - 2].Find(q => q.UserId == p.UserId) == null) _waitingList[playerCount - 2].Add(p);


            // if the player list is the size wanted including the requesting player, 
            if (_waitingList[playerCount - 2].Count == playerCount && p.UserId == _waitingList[playerCount - 2][playerCount - 1].UserId)
            {
                // create a new game containing all the players on the last player's request
                _game = new Game(_waitingList[playerCount - 2]); // create a new game with the players

                foreach (var t in _game.Players)
                {
                    t.Hand = BuildShuffledHand(8);
                }

                _game.Players.Add(new Player(){Username = "table"}); // adding the table as a player

                _game.Players[playerCount].Hand = BuildShuffledHand(100); // giving the table 100 shuffled cards

                _game = BlStartGameDatabase(_game); // add this game to the database!

                //_game.Active = true;

                _gameList.Add((Game)_game.Clone()); // add this game to the game list

                //_waitingList[playerCount - 2].Clear();

                return _game; // return this game
            }

            return null;
        }

        public bool BlStopSearchingForGame(Player remove)
        {
            foreach (PlayerList w in _waitingList)
            {
                w.Remove(w.Find(p => p.UserId == remove.UserId));
            }

            return true;
        }


        //public int BlUserInStandby(User u)
        //{
        //    return _standbyPlayers.ContainsKey()Find(p => p.UserId == u.Id);
        //}

        public bool BlPlayerQuit(Player remove)
        {
            foreach (Game g in _gameList)
            {
                Player temp = g.Players.Find(p => p.UserId == remove.UserId);
                if (temp != null)
                {
                    //_standbyPlayers.Add(temp, g.Id);
                    g.Players.Remove(temp);
                    return true;
                }
            }
            return false;
        }

        //public bool BlPlayerQuit(Player remove)
        //{
        //    foreach (Game g in _gameList)
        //    {
        //        Player temp = g.Players.Find(p => p.UserId == remove.UserId);
        //        if (temp != null && g.Active)
        //        {
        //            _standbyPlayers.Add(temp, g.Id);
        //            g.Players.Remove(temp);
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //public Player BlPlayerReturn(User returningPlayer)
        //{
        //    int gameId = BlUserInStandby(returningPlayer);

        //    if (gameId == 0) return null;

        //    //Player temp = g.Players.Find(p => p.Username == add.UserId.ToString());

        //    _gameList[gameId].Players.Add()
        //    player = temp;
        //    //g.Active = true;

        //    return player;


        //    return null;
        //}

        // insert game into database, including connections in PlayerGameDB and PlayerCardDB
        public Game BlStartGameDatabase(Game g)
        {
            PlayerDb playerDb = new PlayerDb();
            GameDb gameDb = new GameDb();
            PlayerGameDb playerGameDb = new PlayerGameDb();
            PlayerCardDb playerCardDb = new PlayerCardDb();

            ConnectionList playerGameConnectionList = new ConnectionList();
            ConnectionList playerCardConnectionList = new ConnectionList();
            Connection temp = new Connection();

            int lastGameId = gameDb.GetLastGame().Id;
            int lastPlayerId = playerDb.GetLastPlayer().Id;

            g.Id = ++lastGameId;

            foreach (var p in g.Players)
            {
                if (p.Username == "table") p.Id = -g.Id;
                else p.Id = ++lastPlayerId;
            }


            foreach (Player p in g.Players)
            {
                temp.SideA = p.Id; // increment the player id for each player
                temp.SideB = g.Id; // the game id will be the same when inserted
                temp.ConnectionType = "player-game";
                playerGameConnectionList.Add((Connection)temp.Clone());

                foreach (Card c in p.Hand)
                {
                    temp.SideA = p.Id;
                    temp.SideB = c.Id;
                    temp.ConnectionType = "player-card";
                    playerCardConnectionList.Add((Connection)temp.Clone());
                }
            }

            playerDb.InsertList(g.Players); // Insert players into database

            gameDb.Insert(g); // Insert game into database

            playerGameDb.InsertList(playerGameConnectionList); // insert player - game connections

            playerCardDb.InsertList(playerCardConnectionList); // insert player - card connections


            // save the changes and insert the data into the database 

            Thread thread = new Thread(BlSaveChanges); // save the changes on a different thread!
            thread.Start();

            return g;
        }

        public void BlAddCard(Message m)
        {
            PlayerCardDb db = new PlayerCardDb();

            Connection c = new Connection() {
                ConnectionType = Model.Connection._connectionType.player_card,
                SideA = m.Target,
                SideB = m.Card.Id
            };

            db.Insert(c);
        }

        public void BlRemoveCard(Message m)
        {
            PlayerCardDb db = new PlayerCardDb();

            db.GetConnectionByPlayerIdAndCardId(m.Target, m.Card);

            db.Delete()
        }

        public void BlSaveChanges()
        {
            GameDb gameDb = new GameDb();
            gameDb.SaveChanges();
        }

        public CardList BuildShuffledHand(int length)
        {
            Thread.Sleep(50);

            CardList hand = new CardList();
            Card temp;
            Random rand = new Random();

            for (int i = 0; i < length; i++)
            {
                temp = _deck[rand.Next(0, 64)];
                if (length < 65)//if hand is smaller than the deck length, make sure there are no doubles
                {
                    while (hand.Find(q => q.Value == temp.Value && q.Color == temp.Color) != null)// if the card is already in the hand, fetch for a different card
                    {
                        temp = _deck[rand.Next(0, 65)];
                    }
                    hand.Add(temp);
                }
                else
                {
                    hand.Add(temp);// if the hand's length is greater than the deck, just insert random cards
                }
            }

            return hand;
        }
    }
}

