using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Connection : BaseEntity, ICloneable
    {
        public enum ConnectionType {
            PlayerCard,
            PlayerGame
        }
        private int _sideA; // player_id
        private int _sideB; // game_id or card_id


        public int SideA
        {
            get => _sideA;
            set => _sideA = value;
        }


        public int SideB
        {
            get => _sideB;
            set => _sideB = value;
        }


        public ConnectionType CONTYPE { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}