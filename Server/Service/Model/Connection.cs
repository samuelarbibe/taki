using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Connection : BaseEntity, ICloneable
    {
        public enum _connectionType {
            player_card,
            game_card
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


        public _connectionType ConnectionType { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}