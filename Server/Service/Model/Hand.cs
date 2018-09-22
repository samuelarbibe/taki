using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Hand : CardList
    {
        private int _minLength;

        
        public int MinLength { get => _minLength; set => _minLength = value; }
    }
}
