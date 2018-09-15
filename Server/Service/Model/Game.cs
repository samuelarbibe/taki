using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Game : BaseEntity
    {
        private Player player_1;
        private Player player_2;
        private Player player_3;
        private Player player_4;
        private int winner;
        private int player_count;

        [DataMember]
        public Player Player_1 { set; get; }

        [DataMember]
        public Player Player_2 { set; get; }

        [DataMember]
        public Player Player_3 { set; get; }

        [DataMember]
        public Player Player_4 { set; get; }

        [DataMember]
        public int Player_count { set; get; }

        [DataMember]
        public int Winner { set; get; }
    }
}
