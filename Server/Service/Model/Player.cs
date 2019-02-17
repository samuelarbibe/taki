using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Player : User, ICloneable
    {
        public Player()
        {
        }

        public Player(int id)
        {
            Id = 0;
        }

        [DataMember]
        public CardList Hand { get; set; }

        [DataMember]
        public int TempScore { get; set; }

        [DataMember]
        public int UserId { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}