using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;
using BusinessLayer;
using System.Threading;

namespace Service
{
    public class Service : IService
    {
        private static MessageList _pendingChanges = new MessageList(); // pending messages for each player 

        public static MessageList PendingChanges { get => _pendingChanges; set => _pendingChanges = value; }

        public Card BuildDeck()
        {
            Bl bl = new Bl();

            return bl.BlBuildDeck();
        }

        public CardList GetCardList()
        {
            return null;
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

        public Game StartGame(Player p, int playerCount)
        {
            Bl bl = new Bl();
            Game g = bl.BlStartGame(p, playerCount);
            //if(g != null)FirstTurn(g);            
            return g;
        }

        public bool StopSearchingForGame(Player p)
        {
            Bl bl = new Bl();
            return bl.BlStopSearchingForGame(p);
        }

        public bool PlayerQuit(Player p)
        {
            Bl bl = new Bl();
            return bl.BlPlayerQuit(p);
        }

        public PlayerList GetPlayerList()
        {
            return null;
        }

        public void AddAction(Message m)
        {
            PendingChanges.Add(m);
        }

        public void AddActions(MessageList ml)
        {
            PendingChanges.AddRange(ml);
        }

        public MessageList DoAction(int gameId, int playerId)
        {
            MessageList temp = new MessageList();

            if (PendingChanges == null || PendingChanges.Count == 0) return null;
            else  
            {
                foreach (Message m in PendingChanges.ToList())
                {
                    if (m.GameId == gameId && m.Reciever == playerId)
                    {
                        temp.Add(m);
                        PendingChanges.Remove(m);

                        if (m.Reciever == m.Target)// make shure this happens once for each message
                        {
                            if (m.Action == Message._action.add)
                            {
                                Bl bl = new Bl();

                            }

                            else if (m.Action == Message._action.remove)
                            {
                                Bl bl = new Bl();
                            }
                        }
                    }
                }
            } 
            return temp;
        }

    }
}
