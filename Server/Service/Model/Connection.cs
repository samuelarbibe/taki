using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class Connection : BaseEntity
    {
        private int sideA;
        private int sideB;
        private string connection_type;

        [DataMember]
        public int SideA { set; get; }

        [DataMember]
        public int SideB { set; get; }

        [DataMember]
        public string Connection_type { set; get; }
    }
}
