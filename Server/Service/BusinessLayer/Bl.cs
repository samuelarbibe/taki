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

        private static UserList _loggedPlayers = new UserList();
        private static CardDb _cardDb = new CardDb();
        private static CardList _deck = CardDb.SelectAll();
        private static GameList _gameList = new GameList();
        private static List<CardList> _gameDecks = new List<CardList>();
        private static Game _game;

        public static UserList LoggedPlayers { get => _loggedPlayers; set => _loggedPlayers = value; }
        public static CardDb CardDb { get => _cardDb; set => _cardDb = value; }
        public static CardList Deck { get => _deck; set => _deck = value; }
        public static GameList GameList { get => _gameList; set => _gameList = value; }

        public CardList BlBuildDeck()
        {
            CardList cardList = CardDb.SelectAll();
            cardList.Shuffle();
            return cardList;
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
            if (userList != null && userList.Count > 0 && LoggedPlayers.Find(u => u.Id == userList[0].Id) == null)
            {
                LoggedPlayers.Add(userList[0]);
                return userList[0];
            }
            return null;
        }

        public bool BlLogout(int userId)
        {
            User temp = LoggedPlayers.Find(u => u.Id == userId);
            if (temp != null)
            {
                LoggedPlayers.Remove(temp);
                return true;
            }

            return false;
        }

        public bool BlRegister(string firstName, string lastName, string username, string password)
        {
            if (BlUsernameAvailable(username) == false || BlPasswordAvailable(password) == false) return false;

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

        public int BlDeleteUser(User user)
        {
            UserDb db = new UserDb();
            db.Delete(user);
            return db.SaveChanges();
        }

        public int BlUpdateUser(User user)
        {
            UserDb db = new UserDb();
            db.Update(user);
            return db.SaveChanges();
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

        public User BlGetUserByUsername(string Username){
            UserDb db = new UserDb();
            User user = db.SelectByUsername(Username);
            return user;
        }

        public GameList BlGetAllUserGames(int UserId)
        {
            GameDb gameDB = new GameDb();

            GameList temp = gameDB.SelectByUserId(UserId);
            return temp;
        }

        // creates a new game and returns it
        // creates a new Game in the GameDB, and creates new connections in PlayerGameDB
        public Game BlStartGame(Player p, int playerCount)
        {
            //if there is a game in gameList containing this player and if game is active
            Game temp = GameList.Find(g => g.Players.Find(q => q.UserId == p.UserId) != null);

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
                    t.Hand = BuildShuffledHand(6, false);
                }

                _game.Players.Add(new Player(){Username = "table"}); // adding the table as a player

                _game.Players[playerCount].Hand = BuildShuffledHand(59, false); // giving the table 100 shuffled cards

                _game = BlStartGameDatabase(_game); // add this game to the database!

                //_game.Active = true;

                GameList.Add((Game)_game.Clone()); // add this game to the game list

                _game.StartTime = DateTime.Now; // start the game

                return _game; // return this game
            }

            return null;
        }

        public int BlGetPlayersFound(int playerCount)
        {
            return _waitingList[playerCount - 2].Count;
        }

        public bool BlStopSearchingForGame(Player remove)
        {
            foreach (PlayerList w in _waitingList)
            {
                w.Remove(w.Find(p => p.UserId == remove.UserId));
            }

            return true;
        }

        public bool BlPlayerQuit(Player remove)
        {
            foreach (Game g in GameList)
            {
                Player temp = g.Players.Find(p => p.UserId == remove.UserId);
                if (temp != null)
                {
                    g.Players.Remove(temp);
                    return true;
                }
            }
            return false;
        }

        // insert game into database, including connections in PlayerGameDB and PlayerCardDB
        public Game BlStartGameDatabase(Game g)
        {
            PlayerDb playerDb = new PlayerDb();
            GameDb gameDb = new GameDb();
            PlayerGameDb playerGameDb = new PlayerGameDb();
            PlayerCardDb playerCardDb = new PlayerCardDb();

            playerDb.InsertList(g.Players);

            gameDb.Insert(g); // asign ID to game, and all players' IDs in there

            playerGameDb.Insert(g);

            playerCardDb.Insert(g);

            // save the changes and insert the data into the database 

            gameDb.SaveChanges();

            return g;
        }

        public void BlAddCard(Message m)
        {
            PlayerCardDb db = new PlayerCardDb();

            PlayerCardConnection c = new PlayerCardConnection() {
                Player = m.Target,
                Card = m.Card
            };

            db.Insert(c);
        }

        public void BlWin(Message m)
        {
            PlayerDb playerDb = new PlayerDb();
            UserDb userDb = new UserDb();
            User u = userDb.SelectById(m.Target.UserId);

            u.Score += 1000;
            u.Wins += 1;
            u.Level = (u.Score - u.Score % 1000) / 1000;

            userDb.Update(u);
        }

        public void BlLoss(Message m)
        {
            PlayerDb playerDb = new PlayerDb();
            UserDb userDb = new UserDb();
            User u = userDb.SelectById(m.Target.UserId);

            GameDb gameDb = new GameDb();
            Game g = gameDb.GetGameById(m.GameId);

            g.Players.RemoveAll(p => p == null);

            g.EndTime = DateTime.Now;

            g.Losser = m.Target.Id;

            u.Score += 200;
            u.Losses += 1;
            u.Level = (u.Score - u.Score % 1000) / 1000;

            userDb.Update(u);
            gameDb.Update(g);

            userDb.SaveChanges();
        }

        public void BlSwitchHands(Message m)
        {
            PlayerCardDb db = new PlayerCardDb();
            PlayerDb playerDb = new PlayerDb();

            db.SwitchConnectionsByPlayersId(m.Target, playerDb.GetPlayerById(m.Card.Id));
        }

        public int SaveChanges() {

            PlayerCardDb sb = new PlayerCardDb();
            return sb.SaveChanges();
        }

        public void BlRemoveCard(Message m)
        {
            PlayerCardDb db = new PlayerCardDb();

            PlayerCardConnection temp = db.GetConnectionByPlayerIdAndCardId(m.Target, m.Card);

            db.Delete(temp);
        }

        public void BlSaveChanges()
        {
            GameDb gameDb = new GameDb();
            gameDb.SaveChanges();
        }

        public CardList BuildShuffledHand(int length, bool noSpecial)
        {
            Thread.Sleep(50);

            CardList hand = new CardList();
            Card temp;
            Random rand = new Random();

            for (int i = 0; i < length; i++)
            {
                temp = Deck[rand.Next(0, 59)];
                if (length < 60)//if hand is smaller than the deck length, make sure there are no doubles
                {
                    if (!noSpecial)
                    {
                        while (hand.Find(q => q.Id == temp.Id) != null)// if the card is already in the hand, fetch for a different card
                        {
                            temp = Deck[rand.Next(0, 59)];
                        }
                    }
                    else
                    {
                        while (temp.Special)
                        {
                            temp = Deck[rand.Next(0, 59)];
                        }
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

