using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Message : BaseEntity
    {
        private string _action;
        private Card _card;
        private int _sender;


        public int Sender
        {
            get => _sender;
            set => _sender = value;
        }


        public string Action
        {
            get => _action;
            set => _action = value;
        }


        public Card Card
        {
            get => _card;
            set => _card = value;
        }
    }
}