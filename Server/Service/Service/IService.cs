using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        CardList GetCardList();

        [OperationContract]
        CardList BuildDeck();

        [OperationContract]
        Game StartGame(Player p, int playerCount);

        [OperationContract]
        PlayerList GetPlayerList();

        [OperationContract]
        void AddAction(Message m);

        [OperationContract]
        void AddActions(MessageList ml);

        [OperationContract]
        MessageList DoAction(int gameId, int playerId);

        [OperationContract]
        User Login(string username, string password);

        [OperationContract]
        bool Logout(int userId);

        [OperationContract]
        bool Register(string firstName, string lastName, string username, string password);

        [OperationContract]
        int DeleteUser(User user);

        [OperationContract]
        int UpdateUser(User user);

        [OperationContract]
        bool PasswordAvailable(string password);

        [OperationContract]
        GameList GetAllUserGames(int userId);

        [OperationContract]
        User GetUserByUsername(string username);

        [OperationContract]
        User GetUserById(int id);

        [OperationContract]
        bool UsernameAvailable(string username);

        [OperationContract]
        UserList GetAllUserFriends(int userId);

        [OperationContract]
        GameList GetMutualGames(int u1, int u2);

        [OperationContract]
        bool AreFriends(int user1Id, int user2Id);

        [OperationContract]
        void MakeFriends(User u1, User u2);

        [OperationContract]
        void RemoveFriend(User u1, User u2);

        [OperationContract]
        bool StopSearchingForGame(Player p);

        [OperationContract]
        int GetPlayersFound(int playerCount);

        [OperationContract]
        bool PlayerQuit(Player p);

        [OperationContract]
        UserList GetAllUsers();

        [OperationContract]
        int SaveChanges();
    }
}