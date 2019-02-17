using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class PlayerGameConnection : BaseEntity, ICloneable
    {
        [DataMember]
        public Player Player { get; set; }

        [DataMember]
        public Game Game { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}