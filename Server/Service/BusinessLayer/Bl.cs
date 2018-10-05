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

        static PlayerList waitingList = new PlayerList();

        static GameList gameList = new GameList();

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
        public static Game BlStartGame(Player p, int playerCount)
        {
            if (waitingList.Find(q => q.Id == p.Id) == null)// if the player is already in the waiting list, return null
            {
                if (waitingList.Count == (playerCount - 1))// if the player list is the size wanted including the requesting player, make a new game with the players in the waiting list and wipe the waiting list.
                {
                    waitingList.Add(p);

                    Game g = new Game() {Players = waitingList, StartTime = DateTime.Now};

                    for (int i = 0; i < playerCount; i++)
                    {
                        g.Players[i].Hand = new Hand(8);// giving every player a shuffled hand 
                    }

                    g.Players.Add(new Player());// adding the table as a player
                    g.Players[playerCount].Hand = new Hand(100); // giving the table 100 shuffled cards

                    waitingList.Clear();

                    return g;
                }
                else
                {
                    waitingList.Add(p);
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

