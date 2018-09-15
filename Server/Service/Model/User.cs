using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class User : BaseEntity
    {
        private string first_name;
        private string last_name;
        private string username;
        private string password;
        private int score;

        [DataMember]
        public string First_name { set; get; }

        [DataMember]
        public string Last_name { set; get; }

        [DataMember]
        public string Username { set; get; }

        [DataMember]
        public string Password { set; get; }

        [DataMember]
        public int Score { set; get; }
    }
}
