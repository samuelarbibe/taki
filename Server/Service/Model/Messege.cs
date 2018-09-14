using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Messege : BaseEntity
    {
        private int sender;
        private string action;
        private Card card;

        public int Sender { set; get; }
        public string Action { set; get; }
        public Card Card { set; get; }
    }
}
