using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Player : User
    {
        private CardList _hand;
        private int _user_id;
        private int _tempScore;

        public Player() { }

        public Player(int id)
        {
            this.Id = 0;
        }

        public Player(bool isTable)
        {
            this.Username = "table";
        }

        //[DataMember]
        public CardList Hand { get => _hand; set => _hand = value; }

        [DataMember]
        public int TempScore { get => _tempScore; set => _tempScore = value; }

        [DataMember]
        public int User_id { get => _user_id; set => _user_id = value; }
    }
}
