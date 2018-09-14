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

        [DataMember]
        public string Color { set; get; }

        [DataMember]
        public int Value { set; get; }

        [DataMember]
        public bool Special { set; get; }

        [DataMember]
        public string Image { set; get; }
    }
}
