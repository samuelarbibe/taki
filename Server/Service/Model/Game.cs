using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [DataContract]
    public class Game : BaseEntity, ICloneable
    {

        private PlayerList _players;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _winner;

        public Game(){}

        public Game(int id)
        {
            this.EndTime = DateTime.Now;
            this.Id = 0;
        }

        public Game(PlayerList pl)
        {
            this._players = pl;
        }

        [DataMember]
        public DateTime StartTime{ get => _startTime; set => _startTime = value; }

        [DataMember]
        public int Winner { get => _winner; set => _winner = value; }

        //[DataMember]
        //public PlayerList Players { get => _players; set => _players = value; }

        public PlayerList GetPlayers() {
            
            return this._players;
        }

        public void SetPlayers(PlayerList p)
        {
            this._players = p;
        }

        public void SetPlayer(Player p, int index)
        {
            this._players[index] = p;
        }
        
        [DataMember]
        public DateTime EndTime { get => _endTime; set => _endTime = value; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
