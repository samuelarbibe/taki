using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class User : BaseEntity
    {
        private string first_name;
        private string last_name;
        private string username;
        private string password;
        private int score;

        public string First_name { set; get; }
        public string Last_name { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public int Score { set; get; }
    }
}
