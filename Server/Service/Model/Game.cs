using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Game : BaseEntity, ICloneable
    {
        private DateTime _endTime;

        private PlayerList _players;
        private CardList _deck;
        private DateTime _startTime;
        private int _losser;

        public Game()
        {
            Players = new PlayerList();
            EndTime = DateTime.Now;
        }

        public Game(int id)
        {
            EndTime = DateTime.Now;
            Id = 0;
        }

        public Game(PlayerList pl)
        {
            PlayerList temp = new PlayerList();

            foreach(Player p in pl)
            {
                temp.Add((Player)p.Clone());
            }

            Players = temp;
            EndTime = DateTime.Now;
        }


        [DataMember]
        public DateTime StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        [DataMember]
        public int Losser
        {
            get => _losser;
            set => _losser = value;
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