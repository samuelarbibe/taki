using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class Connection : BaseEntity, ICloneable
    {
        private int _sideA; // player_id
        private int _sideB; // game_id or card_id
        private string _connectionType;

        
        public int SideA { get => _sideA; set => _sideA = value; }

        
        public int SideB { get => _sideB; set => _sideB = value; }

        
        public string ConnectionType { get => _connectionType; set => _connectionType = value; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
