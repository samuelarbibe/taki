using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Messege : BaseEntity
    {
        private int sender;
        private string action;
        private Card card;

        [DataMember]
        public int Sender { set; get; }

        [DataMember]
        public string Action { set; get; }

        [DataMember]
        public Card Card { set; get; }
    }
}
