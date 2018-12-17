using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Card : BaseEntity
    {
        private string _color;
        private string _image;
        private bool _special;
        private int _value;

        public enum Color
        {
            green,
            blue,
            red,
            yellow,
            multi
        }

        public enum Value
        { 
            Zero,
            One,
            PlusTwo,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Stop,
            Plus,
            SwitchDirection,
            Taki,
            SwitchColor,
            SwitchHand,
            PlusThree,
            TakiAll,
            SwitchColorAll,
            SwitchHandAll
        }


        [DataMember]

        public Color COLOR { get; set; }

        [DataMember]

        public Value VALUE { get; set; }

        [DataMember]
        public string Image { get => _image; set => _image = value; }

        [DataMember]
        public bool Special { get => _special; set => _special = value; }
    }
}