using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public enum Action
    {
        [EnumMember]
        Add,
        [EnumMember]
        Remove,
        [EnumMember]
        NextTurn,
        [EnumMember]
        PlayerQuit,
        [EnumMember]
        SwitchRotation,
        [EnumMember]
        SwitchHand,
        [EnumMember]
        PlusTwo,
        [EnumMember]
        Win,
        [EnumMember]
        Loss
    }

    [DataContract]
    public class Message : BaseEntity
    {
        
        [DataMember]
        public Player Target { get; set; }

        [DataMember] public Action Action { get; set; }

        [DataMember]
        public Card Card { get; set; }

        [DataMember]
        public int GameId { get; set; }

        [DataMember]
        public int Reciever { get; set; }
    }
}