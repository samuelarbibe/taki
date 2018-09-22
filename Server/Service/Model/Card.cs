using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Model
{
    [DataContract]
    public class Card : BaseEntity
    {
        private string _color;
        private int _value;
        private bool _special;
        private string _image;

        
        public string Color { get => _color; set => _color = value; }

        
        public int Value { get => _value; set => this._value = value; }

        
        public bool Special { get => _special; set => _special = value; }

        
        public string Image { get => _image; set => _image = value; }
    }
}
