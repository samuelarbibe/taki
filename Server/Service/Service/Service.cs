using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;
using ViewModel;
using BL;

namespace Service
{
    public class Service : IService
    {
        public Card BuildDeck()
        {
            Test t = new Test();

            return t.BlBuildDeck();
        }

        public User Login(string username, string password)
        {
            Test t = new Test();
            return t.BlLogin(username, password);
        }

        public bool Register(string firstName, string lastName, string username, string password)
        {
            Test t = new Test();
            return t.BlRegister(firstName, lastName, username, password);
        }

        public bool PasswordAvailable(string password)
        {
            Test t =new Test();
            return t.BlPasswordAvailable(password);
        }

<<<<<<< HEAD
        public bool UsernameAvailable(string username)
        {
            Test t = new Test();
            return t.UsernameAvailable(username);
        }

        public Game StartGame()
=======
        public Game StartGame(User user)
>>>>>>> e79d0940a8ec04732c5902691eaebec0fe68f592
        {
            Test t = new Test();
            return t.BlStartGame(user);
        }

        public PlayerList GetPlayerList()
        {
            return null;
        }

        public MessageList Action(Message m)
        {
            //send m to BL
            //return a new Massage to the Client, with orders what to do with the cards and players
            return null;
        }
    }
}
