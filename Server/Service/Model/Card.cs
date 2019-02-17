using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public enum Color
    {
        [EnumMember]
        Green,
        [EnumMember]
        Blue,
        [EnumMember]
        Red,
        [EnumMember]
        Yellow,
        [EnumMember]
        Multi
    }

    [DataContract]
    public enum Value
    {
        [EnumMember]
        Zero,
        [EnumMember]
        One,
        [EnumMember]
        PlusTwo,
        [EnumMember]
        Three,
        [EnumMember]
        Four,
        [EnumMember]
        Five,
        [EnumMember]
        Six,
        [EnumMember]
        Seven,
        [EnumMember]
        Eight,
        [EnumMember]
        Nine,
        [EnumMember]
        Stop,
        [EnumMember]
        Plus,
        [EnumMember]
        SwitchDirection,
        [EnumMember]
        Taki,
        [EnumMember]
        SwitchColor,
        [EnumMember]
        TakiAll,
        [EnumMember]
        SwitchColorAll,
        [EnumMember]
        SwitchHandAll
    }

    [DataContract]
    public class Card : BaseEntity
    {
        [DataMember] public Color Color { get; set; }

        [DataMember] public Value Value { get; set; }

        [DataMember]
        public string Image { get; set; }

        [DataMember]
        public bool Special { get; set; }
    }
}