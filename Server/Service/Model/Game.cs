using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Game : BaseEntity, ICloneable
    {

        private PlayerList _players;
        private DateTime _startTime;
        private TimeSpan _duration;
        private int _winner;

        public Game(){}

        public Game(int id)
        {
            this._duration = new TimeSpan(0,0,0);
            this.Id = 0;
        }

        [DataMember]
        public DateTime StartTime { get { return _startTime; } set { _startTime = value; } }

        [DataMember]
        public int Winner { get => _winner; set => _winner = value; }

        [DataMember]
        public PlayerList Players { get => _players; set => _players = value; }

        [DataMember]
        public TimeSpan Duration { get => _duration; set => _duration = value; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
