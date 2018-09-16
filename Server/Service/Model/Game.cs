using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Game : BaseEntity
    {

        private PlayerList players;
        private DateTime startTime;
        private int winner;


        public DateTime StartTime { get { return startTime; } set { startTime = value; } }

        public int Winner { get => winner; set => winner = value; }

        public PlayerList Players { get => players; set => players = value; }
    }
}
