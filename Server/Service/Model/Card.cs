using System;

namespace Model
{
    public class Card : BaseEntity
    {
        private string color;
        private int value;
        private bool special;
        private string image;


        public string Color { set; get; }

        public int Value { set; get; }

        public bool Special { set; get; }

        public string Image { set; get; }
    }
}
