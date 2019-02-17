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
        [DataMember]
        public User User1 { get; set; }

        [DataMember]
        public User User2 { get; set; }
    }
}