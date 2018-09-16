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
        public Card buildDeck()
        {
            test t = new test();

            return t.BlBuildDeck();
        }

        public Game startGame()
        {
            return null;
        }

        public PlayerList GetPlayerList()
        {
            return null;
        }
    }
}
