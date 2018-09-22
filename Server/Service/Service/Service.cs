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
            return t.Login(username, password);
        }

        public bool Register(string firstName, string lastName, string username, string password)
        {
            Test t = new Test();
            return t.Register(firstName, lastName, username, password);
        }

        public bool PasswordAvailable(string password)
        {
            Test t =new Test();
            return t.PasswordAvailable(password);
        }

        public Game StartGame()
        {
            return null;
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
