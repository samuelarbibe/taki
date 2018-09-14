using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class User : BaseEntity
    {
        private string first_name;
        private string last_name;
        private int score;

        public string First_name { set; get; }
        public string Last_name { set; get; }
        public int Score { set; get; }
    }
}
