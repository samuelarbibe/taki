using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Game : BaseEntity, ICloneable
    {
        private DateTime _endTime;

        private PlayerList _players;
        private DateTime _startTime;
        private int _winner;

        public Game()
        {
            Players = new PlayerList();
        }

        public Game(int id)
        {
            EndTime = DateTime.Now;
            Id = 0;
        }

        public Game(PlayerList pl)
        {
            Players = pl;
        }


        [DataMember]
        public DateTime StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        [DataMember]
        public int Winner
        {
            get => _winner;
            set => _winner = value;
        }

        [DataMember]
        public DateTime EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        [DataMember]
        public PlayerList Players
        {
            get => _players;
            set => _players = value;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}