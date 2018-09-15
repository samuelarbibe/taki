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

        [DataMember]
        public Hand Hand { set; get; }

        [DataMember]
        public int Temp_score { set; get; }

        [DataMember]
        public int Game_id { set; get; }
    }
}
