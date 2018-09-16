using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Player : User
    {
        private Hand hand;
        private int temp_score;
        private int game_id;

        public Hand Hand { get => hand; set => hand = value; }

        public int Temp_score { get => temp_score; set => temp_score = value; }

        public int Game_id { get => game_id; set => game_id = value; }
    }
}
