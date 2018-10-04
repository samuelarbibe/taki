using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Hand : CardList
    {
        private static int _minLength;
        private static CardList _deck = new CardList();

        public int MinLength { get => _minLength; set => _minLength = value; }

        public static Hand ShuffledHand()
        {
            Hand hand = new Hand();
            Card temp = new Card();
            Random rand = new Random();

            for (int i = 0; i < _minLength; i++)
            {
                temp = _deck[rand.Next(1, 69)];
                while (hand.Find(q => q.Color == temp.Color && q.Value == temp.Value) != null)// makes sure there are no doubles
                {
                    temp = _deck[rand.Next(1, 69)];
                }
                hand.Add(temp);
            }

            return hand;
        }

        public static Hand ShuffledTableHand()
        {
            Hand hand = new Hand();
            Card temp = new Card();
            Random rand = new Random();

            for (int i = 0; i < 100; i++)
            {
                temp = _deck[rand.Next(1, 69)];
                while (hand.Find(q => q.Color == temp.Color && q.Value == temp.Value) != null)// makes sure there are no doubles
                {
                    temp = _deck[rand.Next(1, 69)];
                }
                hand.Add(temp);
            }

            return hand;
        }
    }
}
