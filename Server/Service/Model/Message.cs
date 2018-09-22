using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Message : BaseEntity
    {
        private int _sender;
        private string _action;
        private Card _card;

        
        public int Sender { get => _sender; set => _sender = value; }

        
        public string Action { get => _action; set => _action = value; }

        
        public Card Card { get => _card; set => _card = value; }
    }
}
