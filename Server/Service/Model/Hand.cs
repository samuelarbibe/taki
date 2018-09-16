using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Hand : CardList
    {
        private int min_length;

        public int Min_length { get => min_length; set => min_length = value; }
    }
}
