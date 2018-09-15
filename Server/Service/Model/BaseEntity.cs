using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using System.Text;

namespace Model
{
    [DataContract]
    public class BaseEntity
    {
        
        private int id;

        [DataMember]
        public int Id { get; set; }
    }
}
