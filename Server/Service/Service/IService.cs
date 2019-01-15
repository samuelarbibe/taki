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
        bool PasswordAvailable(string password);

        [OperationContract]
        GameList GetAllUserGames(int UserId);

        [OperationContract]
        bool UsernameAvailable(string username);

        [OperationContract]
        bool StopSearchingForGame(Player p);

        [OperationContract]
        int GetPlayersFound(int playerCount);

        [OperationContract]
        bool PlayerQuit(Player p);

        [OperationContract]
        UserList GetAllUsers();

        [OperationContract]
        void SaveChanges();

    }

}
