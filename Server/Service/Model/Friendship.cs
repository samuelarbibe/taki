using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{   
    [DataContract]
    public class Friendship : BaseEntity
    {
        private User _user1;
        private User _user2;

        [DataMember]
        public User User1 { get => _user1; set => _user1 = value; }

        [DataMember]
        public User User2 { get => _user2; set => _user2 = value; }
    }
}
