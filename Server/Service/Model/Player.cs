using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Player : User
    {
        private Hand hand;
        private int temp_score;
        private int game_id;

        public Hand Hand { set; get; }

        public int Temp_score { set; get; }
        public int Game_id { set; get; }
    }
}
