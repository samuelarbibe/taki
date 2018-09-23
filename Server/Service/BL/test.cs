using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;

namespace BL
{
    public class Test
    {
        public Card BlBuildDeck()
        {
            CardDb db = new CardDb();
            CardList deck = db.SelectAll();
            return deck[0] as Card;
        }

        //receives a Message from the service, calculates according to the algorithms
        //makes changes to the database 
        //returns 
        public Message Action(MessageList list)
        {
            //return a message
            return null;
        }

        public User Login (string username, string password)
        {
            UserDb db = new UserDb();
            UserList userList = db.SelectByUsernameAndPassword(username, password);
            if (userList.Count > 0)
            {
                return userList[0] as User;
            }
            return null;
        }

        public bool Register(string firstName, string lastName, string username, string password)
        {
            UserDb db = new UserDb();
            User newUser = new User
            {
                FirstName = firstName, LastName = lastName, Username = username, Password = password
            };

            db.Insert(newUser);

            if (db.SaveChanges() == 0)//check if user was inserted, and it exists
            {
                return false;
            }

            return true;
        }

        public bool PasswordAvailable(string password)
        {
            UserDb db = new UserDb();
            User user = db.SelectByPassword(password);
            return (user == null);//return true if there is no user with this password
        }

        public bool UsernameAvailable(string username)
        {
            UserDb db = new UserDb();
            User user = db.SelectByUsername(username);
            return (user == null);//return true if there is no user with this username
        }
    }
}
