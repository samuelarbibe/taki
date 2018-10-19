using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class User : BaseEntity
    {
        private bool _admin;
        private string _firstName;
        private string _lastName;
        private int _level;
        private int _losses;
        private string _password;
        private int _score;
        private string _username;
        private int _wins;

        [DataMember]
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        [DataMember]
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        [DataMember]
        public string Username
        {
            get => _username;
            set => _username = value;
        }

        [DataMember]
        public string Password
        {
            get => _password;
            set => _password = value;
        }

        [DataMember]
        public int Score
        {
            get => _score;
            set => _score = value;
        }

        [DataMember]
        public int Level
        {
            get => _level;
            set => _level = value;
        }

        [DataMember]
        public bool Admin
        {
            get => _admin;
            set => _admin = value;
        }

        [DataMember]
        public int Wins
        {
            get => _wins;
            set => _wins = value;
        }

        [DataMember]
        public int Losses
        {
            get => _losses;
            set => _losses = value;
        }
    }
}