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
        public static MessageList PendingChanges { get; set; } = new MessageList();

        public CardList BuildDeck()
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

        public User GetUserByUsername(string username)
        {
            Bl bl = new Bl();
            return bl.BlGetUserByUsername(username);
        }

        public User GetUserById(int id)
        {
            Bl bl = new Bl();
            return bl.BlGetUserById(id);
        }

        public User Login(string username, string password)
        {
            Bl bl = new Bl();
            return bl.BlLogin(username, password);
        }

        public bool Logout(int userId)
        {
            Bl bl = new Bl();
            return bl.BlLogout(userId);
        }

        public bool Register(string firstName, string lastName, string username, string password)
        {
            Bl bl = new Bl();
            return bl.BlRegister(firstName, lastName, username, password);
        }

        public int DeleteUser(User user)
        {
            Bl bl = new Bl();
            return bl.BlDeleteUser(user);
        }

        public int UpdateUser(User user)
        {
            Bl bl = new Bl();
            return bl.BlUpdateUser(user);
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

        public GameList GetAllUserGames(int userId)
        {
            Bl bl = new Bl();
            return bl.BlGetAllUserGames(userId);
        }

        public UserList GetAllUseFriends(int userId)
        {
            Bl bl = new Bl();
            return bl.BlGetAllUserFriends(userId);
        }

        public bool AreFriends(int user1Id, int user2Id)
        {
            Bl bl = new Bl();
            return bl.BlAreFriends(user1Id, user2Id);
        }

        public void MakeFriends(User u1, User u2)
        {
            Bl bl = new Bl();
            bl.BlMakeFriends(u1, u2);
        }

        public Game StartGame(Player p, int playerCount)
        {
            Bl bl = new Bl();
            Game g = bl.BlStartGame(p, playerCount);

            return g;
        }

        public int GetPlayersFound(int playerCount)
        {
            Bl bl = new Bl();
            return bl.BlGetPlayersFound(playerCount);
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

        public int SaveChanges()
        {
            Bl bl = new Bl();
            return bl.SaveChanges();
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

                        if (m.Reciever == m.Target.Id) // make sure this happens once for each message
                        {
                            switch (m.Action)
                            {
                                case Model.Action.Add:
                                    Bl bl = new Bl();
                                    bl.BlAddCard(m);
                                    break;

                                case Model.Action.Remove:
                                    bl = new Bl();
                                    bl.BlRemoveCard(m);
                                    break;

                                case Model.Action.SwitchHand:
                                    bl = new Bl();
                                    bl.BlSwitchHands(m);
                                    break;

                                case Model.Action.Win:
                                    bl = new Bl();
                                    bl.BlWin(m);
                                    break;

                                case Model.Action.Loss:
                                    bl = new Bl();
                                    bl.BlLoss(m);
                                    break;
                            }
                        }
                    }
                }
            }

            return temp;
        }
    }
}