using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class User : BaseEntity
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int Score { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public bool Admin { get; set; }

        [DataMember]
        public int Wins { get; set; }

        [DataMember]
        public int Losses { get; set; }
    }
}