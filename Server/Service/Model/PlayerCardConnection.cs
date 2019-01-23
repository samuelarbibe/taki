using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class PlayerCardConnection : BaseEntity, ICloneable
    {
        private Player _player;
        private Card _card;

        [DataMember]
        public Card Card { get => _card; set => _card = value; }

        [DataMember]
        public Player Player { get => _player; set => _player = value; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}