using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Player : User
    {
        private Hand _hand;
        private int _tempScore;

        public Player() { }

        public Player(bool isTable)
        {
            this.Username = "table";
        }

        public Player(User u)
        {
            this.Id = u.Id;
            this.FirstName = u.FirstName;
            this.LastName = u.LastName;
            this.Username = u.Username;
            this.Password = u.Password;
            this.Level = u.Level;
            this.Score = u.Score;
            this.Admin = u.Admin;
            this.Wins = u.Wins;
            this.Losses = u.Losses;
            _hand = null;
            _tempScore = 0;
        }

        public Hand Hand { get => _hand; set => _hand = value; }

        public int TempScore { get => _tempScore; set => _tempScore = value; }

    }
}
