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

        [DataMember]
        public string Color
        {
            get => _color;
            set => _color = value;
        }

        [DataMember]
        public int Value
        {
            get => _value;
            set => _value = value;
        }

        [DataMember]
        public bool Special
        {
            get => _special;
            set => _special = value;
        }

        [DataMember]
        public string Image
        {
            get => _image;
            set => _image = value;
        }
    }
}