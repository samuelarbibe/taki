using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Model;
using ViewModel;


namespace BusinessLayer
{
    public class Bl
    {
        //TODO 
        //create a save changes timer for each game

        private static readonly List<PlayerList> WaitingList = new List<PlayerList>
        {
            // creating a list of waiting lists
            new PlayerList(), //waitingList[0] = players looking for 2 player games
            new PlayerList(), //waitingList[1] = players looking for 3 player games
            new PlayerList() //waitingList[2] = players looking for 4 player games
        };

        private static Game _game;

        public static UserList LoggedPlayers { get; set; } = new UserList();

        public static CardDb CardDb = new CardDb();

        public static CardList Deck = new CardList();

        public static GameList ActiveGameList { get; set; } = new GameList();

        public static BackgroundWorker SaveChangesBackgroundWorker;

        private void SetSaveChanges()
        {
            SaveChangesBackgroundWorker = new BackgroundWorker();
            SaveChangesBackgroundWorker.DoWork += BlSaveChanges;
            SaveChangesBackgroundWorker.WorkerSupportsCancellation = true;
            SaveChangesBackgroundWorker.RunWorkerCompleted += SaveChangesBackgroundWorker_RunWorkerCompleted;

            SaveChangesBackgroundWorker.RunWorkerAsync();
        }

        private void BlSaveChanges(object sender, DoWorkEventArgs args)
        {
            Thread.Sleep(500);
            GameDb gameDb = new GameDb();
            gameDb.SaveChanges();
        }

        private void SaveChangesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ActiveGameList.Count > 0) SaveChangesBackgroundWorker.RunWorkerAsync();
        }

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

        public User BlGetUserByUsername(string username)
        {
            UserDb db = new UserDb();
            User user = db.SelectByUsername(username);
            return user;
        }

        public User BlGetUserById(int id)
        {
            UserDb db = new UserDb();
            User user = db.SelectById(id);
            return user;
        }

        public GameList BlGetAllUserGames(int userId)
        {
            GameDb gameDb = new GameDb();

            GameList temp = gameDb.SelectByUserId(userId);
            return temp;
        }

        // return a list containing all the user's friends
        public UserList BlGetAllUserFriends(int userId)
        {
            FriendDb db = new FriendDb();

            UserList temp = db.GetAllUserFriends(userId);
            return temp;
        }

        public bool BlAreFriends(int user1Id, int user2Id)
        {
            FriendDb db = new FriendDb();

            return (db.SelectByUsersId(user1Id, user2Id).Count > 0);
        }

        public void BlMakeFriends(User u1, User u2)
        {
            FriendDb db = new FriendDb();

            Friendship fr = new Friendship()
            {
                User1 = u1,
                User2 = u2
            };

            db.Insert(fr);
            if (ActiveGameList.Count == 0) SaveChanges(); // if there is no backgroudnWorker Running, SaveChanges independently
        }

        public void BlRemoveFriends(User u1, User u2)
        {
            FriendDb db = new FriendDb();

            Friendship fr = (Friendship)db.SelectByUsersId(u1.Id, u2.Id)[0];
       
            db.Delete(fr);
            SaveChanges();
        }

        // creates a new game and returns it
        public Game BlStartGame(Player p, int playerCount)
        {
            //if there is a game in gameList containing this player and if game is active
            Game temp = ActiveGameList.Find(g => g.Players.Find(q => q.UserId == p.UserId) != null);

            // return the game to the player!
            if (temp != null)
            {
                if (WaitingList[playerCount - 2].Count > 0) WaitingList[playerCount - 2].Clear();
                return temp;
            }

            // if the list contains a table, clear it! its old!
            if (WaitingList[playerCount - 2].Count > playerCount) WaitingList[playerCount - 2].Clear();


            // if the player isn't in the waiting list add him.
            if (WaitingList[playerCount - 2].Find(q => q.UserId == p.UserId) == null)
                WaitingList[playerCount - 2].Add(p);


            // if the player list is the size wanted including the requesting player, 
            if (WaitingList[playerCount - 2].Count == playerCount &&
                p.UserId == WaitingList[playerCount - 2][playerCount - 1].UserId)
            {
                // create a new game containing all the players on the last player's request
                _game = new Game(WaitingList[playerCount - 2]); // create a new game with the players

                foreach (var t in _game.Players)
                {
                    t.Hand = BuildShuffledHand(6, false);
                }

                _game.Players.Add(new Player() {Username = "table"}); // adding the table as a player

                _game.Players[playerCount].Hand = BuildShuffledHand(59, true); // giving the table 100 shuffled cards

                _game = BlStartGameDatabase(_game); // add this game to the database!

                // if no active games exist, no background worker is active.
                // so set one:
                if (ActiveGameList.Count == 0)
                {
                    SetSaveChanges();
                }

                ActiveGameList.Add((Game) _game.Clone()); // add this game to the active games list

                _game.StartTime = DateTime.Now; // start the game

                return _game; // return this game
            }

            return null;
        }

        public int BlGetPlayersFound(int playerCount)
        {
            return WaitingList[playerCount - 2].Count;
        }

        public bool BlStopSearchingForGame(Player remove)
        {
            foreach (PlayerList w in WaitingList)
            {
                w.Remove(w.Find(p => p.UserId == remove.UserId));
            }

            return true;
        }

        public bool BlPlayerQuit(Player remove)
        {
            foreach (Game g in ActiveGameList)
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

            gameDb.Insert(g); // assign ID to game, and all players' IDs in there

            playerGameDb.Insert(g);

            playerCardDb.Insert(g);

            // save the changes and insert the data into the database 

            gameDb.SaveChanges();

            return g;
        }

        public void BlAddCard(Message m)
        {
            PlayerCardDb db = new PlayerCardDb();

            PlayerCardConnection c = new PlayerCardConnection()
            {
                Player = m.Target,
                Card = m.Card
            };

            db.Insert(c);
        }

        public void BlWin(Message m)
        {
            UserDb userDb = new UserDb();
            User u = userDb.SelectById(m.Target.UserId);

            u.Score += 1000;
            u.Wins += 1;
            u.Level = (u.Score - u.Score % 1000) / 1000;

            userDb.Update(u);
        }

        //this is called when a game is finished.
        //declares the loser to the game db.
        public void BlLoss(Message m)
        {
            UserDb userDb = new UserDb();
            User u = userDb.SelectById(m.Target.UserId);

            GameDb gameDb = new GameDb();
            Game g = gameDb.GetGameById(m.GameId);

            g.Players.RemoveAll(p => p == null);

            g.EndTime = DateTime.Now;

            g.Loser = m.Target.Id;

            u.Score += 200;
            u.Losses += 1;
            u.Level = (u.Score - u.Score % 1000) / 1000;

            userDb.Update(u);
            gameDb.Update(g);

            // remove this game from static local list
            ActiveGameList.Remove(ActiveGameList.Find(gm => gm.Id == m.GameId));

            userDb.SaveChanges();
        }

        public void BlSwitchHands(Message m)
        {
            PlayerCardDb db = new PlayerCardDb();
            PlayerDb playerDb = new PlayerDb();

            db.SwitchConnectionsByPlayersId(m.Target, playerDb.GetPlayerById(m.Card.Id));
        }

        public int SaveChanges()
        {
            PlayerCardDb sb = new PlayerCardDb();
            return sb.SaveChanges();
        }

        public void BlRemoveCard(Message m)
        {
            PlayerCardDb db = new PlayerCardDb();

            PlayerCardConnection temp = db.GetConnectionByPlayerIdAndCardId(m.Target, m.Card);

            db.Delete(temp);
        }

        

        public CardList BuildShuffledHand(int length, bool noSpecial)
        {
            Deck = CardDb.SelectAll();

            CardList hand = new CardList();
            Card temp;
            Random rand = new Random();

            for (int i = 0; i < length; i++)
            {
                temp = Deck[rand.Next(0, 59)];
                if (length < 60) //if hand is smaller than the deck length, make sure there are no doubles
                {
                    if (!noSpecial)
                    {
                        while (hand.Find(q => q.Id == temp.Id) != null
                        ) // if the card is already in the hand, fetch for a different card
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
                    hand.Add(temp); // if the hand's length is greater than the deck, just insert random cards
                }
            }

            return hand;
        }
    }
}