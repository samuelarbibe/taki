using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;


namespace BusinessLayer
{
    public class Bl
    {
        static List<PlayerList> _waitingList = new List<PlayerList>
        {
            // creating a list of waiting lists
            new PlayerList { }, //waitingList[0] = players looking for 2 player games
            new PlayerList { }, //waitingList[1] = players looking for 3 player games
            new PlayerList { }, //waitingList[2] = players looking for 4 player games
        };

        static GameList _gameList = new GameList();

        static Game _game;

        public Card BlBuildDeck()
        {
            CardDb db = new CardDb();
            CardList deck = db.SelectAll();
            return deck[0] as Card;
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
                return userList[0] as User;
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
            //if there is a game in gameList containing this player
            Game temp = _gameList.Find(g => g.Players.Find(q => q.Id == p.Id) != null); 


            // return the game to the player!
            if (temp != null) return temp;


            // if the list contains a table, clear it! its old!
            if (_waitingList[playerCount - 2].Count > playerCount) _waitingList[playerCount - 2].Clear();


            // if the player isn't in the waiting list add him.
            if (_waitingList[playerCount - 2].Find(q => q.Id == p.Id) == null) _waitingList[playerCount - 2].Add(p);


            // if the player list is the size wanted including the requesting player, 
            if (_waitingList[playerCount - 2].Count == playerCount)
            {
                // create a new game containing all the players on the last player's request
                if (p.Id == _waitingList[playerCount - 2][playerCount - 1].Id)
                {
                    _game = new Game() {Players = _waitingList[playerCount - 2], StartTime = DateTime.Now};

                    for (int i = 0; i < playerCount; i++)
                    {
                        _game.Players[i].Hand = new Hand(8); // giving every player a shuffled hand 
                    }

                    _game.Players.Add(new Player(true)); // adding the table as a player
                    _game.Players[playerCount].Hand = new Hand(100); // giving the table 100 shuffled cards


                    _gameList.Add((Game) _game.Clone()); // add this game to the game list

                    try
                    {
                        return _game; // return this game
                    }
                    finally
                    {
                        BlStartGameDatabase(_game); // add this game to the database!
                    }
                }
            }

            return null;
        }


        // insert game into database, including connections in PlayerGameDB and PlayerCardDB
        public void BlStartGameDatabase(Game g)
        {
            PlayerDb playerDb = new PlayerDb();
            GameDb gameDb = new GameDb();
            PlayerGameDb playerGameDb = new PlayerGameDb();
            PlayerCardDb playerCardDb = new PlayerCardDb();

            ConnectionList playerGameConnectionList = new ConnectionList();
            ConnectionList playerCardConnectionList = new ConnectionList();
            Connection temp = new Connection();

            playerDb.InsertList(g.Players); // Insert players into database

            gameDb.Insert(g); // Insert game into database


            int lastGameId = gameDb.GetLastGame().Id;
            int lastPlayerId = playerDb.GetLastPlayerID().Id;

            foreach (Player p in g.Players)
            {
                temp.SideA = ++lastPlayerId; // increment the player id for each player
                temp.SideB = (lastGameId + 1); // the game id will be the same when inserted
                playerGameConnectionList.Add(temp);

                foreach (Card c in p.Hand)
                {
                    temp.SideA = lastPlayerId;
                    temp.SideB = c.Id;
                    playerCardConnectionList.Add(temp);
                }
            }

            playerGameDb.InsertList(playerGameConnectionList); // insert player - game connections

            playerCardDb.InsertList(playerCardConnectionList); // insert player - card connections


            // save the changes and insert the data into the database 
            playerDb.SaveChanges();
            gameDb.SaveChanges();
            playerGameDb.SaveChanges();
            playerCardDb.SaveChanges();
        }
    }
}

