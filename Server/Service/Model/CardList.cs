using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Model
{
    [CollectionDataContract]
    public class CardList : List<Card>
    {
        //public static CardList Deck;

        public CardList()
        {
        }

        public CardList(IEnumerable<Card> list) : base(list)
        {
        }

        public CardList(IEnumerable<BaseEntity> list) : base(list.Cast<Card>().ToList())
        {
        }


       
        public void Shuffle()
        {
            Random rng = new Random();

            int n = this.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = this[k];
                this[k] = this[n];
                this[n] = value;
            }
        }
    }
}


