using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class PlayerCardConnection : BaseEntity, ICloneable
    {
        [DataMember]
        public Card Card { get; set; }

        [DataMember]
        public Player Player { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}