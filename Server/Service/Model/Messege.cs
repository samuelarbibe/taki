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

        public int Sender { get => sender; set => sender = value; }

        public string Action { get => action; set => action = value; }

        public Card Card { get => card; set => card = value; }
    }
}
