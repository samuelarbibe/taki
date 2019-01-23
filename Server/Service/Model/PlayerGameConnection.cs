using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class PlayerGameConnection : BaseEntity, ICloneable
    {
        private Player _player;
        private Game _game; 

        [DataMember]
        public Player Player { get => _player; set => _player = value; }

        [DataMember]
        public Game Game { get => _game; set => _game = value; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}