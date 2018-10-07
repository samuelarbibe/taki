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

        public int MinLength { get => _minLength; set => _minLength = value; }

        public Hand(int length)
        {
            Card temp;
            Random rand = new Random();

            for (int i = 0; i < length; i++)
            {
                temp = CardList.Deck[rand.Next(0, 65)];
                if (length < 65)//if hand is smaller than the deck length, make sure there are no doubles
                {
                    while (this.Find(q => q.Value == temp.Value && q.Color == temp.Color) != null)// if the card is already in the hand, fetch for a different card
                    {
                        temp = CardList.Deck[rand.Next(0, 65)];
                    }
                }
                else
                {
                    this.Add(temp);// if the hand's length is greater than the deck, just insert random cards
                }

            }
        }
    }
}
