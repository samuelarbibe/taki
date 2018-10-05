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
                while (this.Find(q => q.Value == temp.Value && q.Color == temp.Color) != null)// if the card is already in the hand, fetch for a different card
                {
                    temp = CardList.Deck[rand.Next(0, 65)];
                }

                this.Add(temp);
            }
        }
    }
}
