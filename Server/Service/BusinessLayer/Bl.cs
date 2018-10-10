using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;


namespace BusinessLayer
{
    public class BL
    {
        static List<PlayerList> waitingList = new List<PlayerList>{ // creating a list of waiting lists
            new PlayerList {},//waitingList[0] = players looking for 2 player games
            new PlayerList {},//waitingList[1] = players looking for 3 player games
            new PlayerList {},//waitingList[2] = players looking for 4 player games
        };
        
        
        

        static GameList gameList = new GameList();

        static Game g = null;

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
            return (user == null);//return true if there is no user with this username
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
            if (gameList.Count > 0)
            {
                if(waitingList[playerCount - 2].Count > 0 && p.Id == waitingList[playerCount - 2][playerCount].Id) // when the last player request gets here, delete g and waiting list, and return a copy of g
                {
                    Game temp = g;
                    g = null;
                    waitingList[playerCount - 2].Clear();
                    return gameList[0];
                }
                return g;
            }

            if (waitingList[playerCount - 2].Find(q => q.Id == p.Id) == null)// if the player isnt in the waiting list add him.
            {
                waitingList[playerCount - 2].Add(p);

                if (waitingList[playerCount - 2].Count == playerCount)// if the player list is the size wanted including the requesting player, 
                {                                    //make a new game with the players in the waiting list and wipe the waiting list.

                    if (p.Id == (((waitingList[playerCount - 2])))[playerCount - 1].Id) // create a new game containing all the players on the last player's request
                    { 
                        g = new Game() { Players = waitingList[playerCount - 2], StartTime = DateTime.Now };

                        for (int i = 0; i < playerCount; i++)
                        {
                            g.Players[i].Hand = new Hand(8);// giving every player a shuffled hand 
                        }

                        g.Players.Add(new Player(true));// adding the table as a player
                        g.Players[playerCount].Hand = new Hand(100); // giving the table 100 shuffled cards


                        gameList.Add((Game)g.Clone()); // add this game to the static game list
                    }
                }
            }
            return null;
        }
        

        public bool BlStartGameDatabase(Game g)
        {
            // insert game into database, including connections in PlayerGameDB and PlayerCardDB
            return true;
        }
    }
}

