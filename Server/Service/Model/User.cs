using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class User : BaseEntity
    {
        private string _firstName;
        private string _lastName;
        private string _username;
        private string _password;
        private int _level;
        private int _score;
        private bool _admin;

        [DataMember]
        public string FirstName {get => _firstName; set => _firstName = value;  }

        [DataMember]
        public string LastName { get => _lastName; set => _lastName = value; }

        [DataMember]
        public string Username { get => _username; set => _username = value; }

        [DataMember]
        public string Password { get => _password; set => _password = value; }

        [DataMember]
        public int Score { get => _score; set => _score = value; }

        [DataMember]
        public int Level { get => _level; set => _level = value; }

        [DataMember]
        public bool Admin { get => _admin; set => _admin = value; }

    }
}
