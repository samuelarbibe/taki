using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Game : BaseEntity, ICloneable
    {

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

            foreach (Player p in pl)
            {
                temp.Add((Player) p.Clone());
            }

            Players = temp;
            EndTime = DateTime.Now;
        }


        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public int Losser { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public PlayerList Players { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}