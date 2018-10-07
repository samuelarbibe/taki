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
            BL bl = new BL();

            return bl.BlBuildDeck();
        }

        public UserList GetAllUsers()
        {
            BL bl = new BL();
            return bl.BlGetAllUsers();
        }

        public User Login(string username, string password)
        {
            BL bl = new BL();
            return bl.BlLogin(username, password);
        }

        public bool Register(string firstName, string lastName, string username, string password)
        {
            BL bl = new BL();
            return bl.BlRegister(firstName, lastName, username, password);
        }

        public bool PasswordAvailable(string password)
        {
            BL bl = new BL();
            return bl.BlPasswordAvailable(password);
        }

        public bool UsernameAvailable(string username)
        {
            BL bl = new BL();
            return bl.BlUsernameAvailable(username);
        }

        public async Task<Game> StartGameAsync(User u, int playerCount)
        {
            Player player = new Player(u);
            return await BL.BlStartGame(player, playerCount);
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
