using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [DataContract]
    public class Player : User
    {
        private Hand _hand;
        private int _tempScore;
        private int _gameId;

        
        public Hand Hand { get => _hand; set => _hand = value; }

        
        public int TempScore { get => _tempScore; set => _tempScore = value; }

        
        public int GameId { get => _gameId; set => _gameId = value; }
    }
}
