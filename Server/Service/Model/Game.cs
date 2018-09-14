using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Game : BaseEntity
    {
        private PlayerList players;
        private int winner;
        private int player_count;



        public Player GetPlayer_1()
        {
            return players[0];
        }

        public void SetPlayer_1(Player x)
        {
            this.players[0] = x;
        }


        public Player GetPlayer_2()
        {
            return players[1];
        }

        public void SetPlayer_2(Player x)
        {
            this.players[1] = x;
        }



        public Player GetPlayer_3()
        {
            return players[2];
        }

        public void SetPlayer_3(Player x)
        {
            this.players[2] = x;
        }



        public Player GetPlayer_4()
        {
            return players[3];
        }

        public void SetPlayer_4(Player x)
        {
            this.players[3] = x;
        }


        public int Player_count { set; get; }
        public int Winner { set; get; }
    }
}
