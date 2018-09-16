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

        public int SideA { get => sideA; set => sideA = value; }

        public int SideB { get => sideB; set => sideB = value; }

        public string Connection_type { get => connection_type; set => connection_type = value; }
    }
}
