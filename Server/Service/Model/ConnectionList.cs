using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Model
{
    [CollectionDataContract]
    public class ConnectionList : List<Connection>
    {
        public ConnectionList()
        {
        }

        public ConnectionList(IEnumerable<Connection> list) : base(list)
        {
        }

        public ConnectionList(IEnumerable<BaseEntity> list) : base(list.Cast<Connection>().ToList())
        {
        }
    }
}