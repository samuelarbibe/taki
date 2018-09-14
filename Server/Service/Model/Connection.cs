using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Connection : BaseEntity
    {
        private int sideA;
        private int sideB;
        private string connection_type;

        public int SideA { set; get; }
        public int SideB { set; get; }
        public string Connection_type { set; get; }
    }
}
