using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Game : BaseEntity
    {

        private PlayerList _players;
        private DateTime _startTime;
        private int _winner;

        [DataMember]
        public DateTime StartTime { get { return _startTime; } set { _startTime = value; } }

        [DataMember]
        public int Winner { get => _winner; set => _winner = value; }

        [DataMember]
        public PlayerList Players { get => _players; set => _players = value; }
    }
}
