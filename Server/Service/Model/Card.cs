using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Model
{
    [DataContract]
    public class Card : BaseEntity
    {
        private string color;
        private int value;
        private bool special;
        private string image;

        public string Color { get => color; set => color = value; }

        public int Value { get => value; set => this.value = value; }

        public bool Special { get => special; set => special = value; }

        public string Image { get => image; set => image = value; }
    }
}
