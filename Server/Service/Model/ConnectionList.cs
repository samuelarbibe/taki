using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Model
{
    [CollectionDataContract]
    public class ConnectionList : List<BaseEntity>
    {
        public ConnectionList()
        {
        }

        public ConnectionList(IEnumerable<BaseEntity> list) : base(list)
        {
        }

        public ConnectionList(IEnumerable<PlayerCardConnection> list) : base(list.Cast<PlayerCardConnection>().ToList())
        {
        }

        public ConnectionList(IEnumerable<PlayerGameConnection> list) : base(list.Cast<PlayerGameConnection>().ToList())
        {
        }
    }
}