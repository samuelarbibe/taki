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
        public Card BlBuildDeck()
        {
            CardDB db = new CardDB();
            CardList deck = db.SelectAll();
            return deck[0] as Card;
        }

        //recieves a messege from the service, calculates according to the algorithms
        //makes changes to the database 
        //returns 
        public Messege Action(MessegeList list)
        {
            //return a messe
            return null;
        }
    }
}
