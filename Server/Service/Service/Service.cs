using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;
using BusinessLayer;

namespace Service
{
    public class Service : IService
    {
        
        public Card BuildDeck()
        {
            Bl bl = new Bl();

            return bl.BlBuildDeck();
        }

        public UserList GetAllUsers()
        {
            Bl bl = new Bl();
            return bl.BlGetAllUsers();
        }

        public User Login(string username, string password)
        {
            Bl bl = new Bl();
            return bl.BlLogin(username, password);
        }

        public bool Register(string firstName, string lastName, string username, string password)
        {
            Bl bl = new Bl();
            return bl.BlRegister(firstName, lastName, username, password);
        }

        public bool PasswordAvailable(string password)
        {
            Bl bl = new Bl();
            return bl.BlPasswordAvailable(password);
        }

        public bool UsernameAvailable(string username)
        {
            Bl bl = new Bl();
            return bl.BlUsernameAvailable(username);
        }

        public Game StartGame(User u, int playerCount)
        {
            Player player = new Player(u);
            Bl bl = new Bl();
            return bl.BlStartGame(player, playerCount);
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
