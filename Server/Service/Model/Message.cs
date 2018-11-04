using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Message : BaseEntity
    {
        private string _action;
        private int _reciever;
        private int _gameId;
        private Card _card;
        private int _target;

        [DataMember]
        public int Target
        {
            get => _target;
            set => _target = value;
        }

        [DataMember]
        public string Action
        {
            get => _action;
            set => _action = value;
        }

        [DataMember]
        public Card Card
        {
            get => _card;
            set => _card = value;
        }

        [DataMember]
        public int GameId { get => _gameId; set => _gameId = value; }

        [DataMember]
        public int Reciever { get => _reciever; set => _reciever = value; }
    }
}