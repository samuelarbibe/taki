using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using System.Text;

namespace Model
{
    [DataContract]
    public class BaseEntity
    {
        private int _id;

        
        public int Id { get => _id; set => _id = value; }
    }
}
