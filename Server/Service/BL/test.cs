using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;

namespace BL
{
    public class test
    {
        public CardList BlBuildDeck()
        {
            CardDB db = new CardDB();
            CardList deck = db.SelectAll();
            return deck;
        }
    }
}
