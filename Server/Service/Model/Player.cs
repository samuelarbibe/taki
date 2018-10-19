using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Player : User
    {
        private CardList _hand;
        private int _tempScore;
        private int _userId;

        public Player()
        {
        }

        public Player(int id)
        {
            Id = 0;
        }

        //[DataMember]
        public CardList Hand
        {
            get => _hand;
            set => _hand = value;
        }

        [DataMember]
        public int TempScore
        {
            get => _tempScore;
            set => _tempScore = value;
        }

        [DataMember]
        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }
    }
}