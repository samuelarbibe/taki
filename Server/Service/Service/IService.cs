﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;
using ViewModel;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Card BuildDeck();

        [OperationContract]
        Game StartGame();

        [OperationContract]
         PlayerList GetPlayerList();

        [OperationContract]
        MessageList Action(Message m);

        [OperationContract]
        User Login(string username, string password);

        [OperationContract]
        bool Register(string firstName, string lastName, string username, string password);

        [OperationContract]
        bool PasswordAvailable(string password);
    }

}
