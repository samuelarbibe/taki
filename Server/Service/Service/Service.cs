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
        public CardList buildDeck()
        {
            test t = new test();
            CardDB db = new CardDB();
            CardList deck = db.SelectAll();

            return t.BlBuildDeck();
        }
    }
}
