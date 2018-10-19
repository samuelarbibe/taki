using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Connection : BaseEntity, ICloneable
    {
        private string _connectionType;
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


        public string ConnectionType
        {
            get => _connectionType;
            set => _connectionType = value;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}