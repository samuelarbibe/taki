using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [CollectionDataContract]
    public class PlayerList : List<Player>, ICloneable
    {
        public PlayerList() { }

        public PlayerList(IEnumerable<Player> list) : base(list) { }

        public PlayerList(IEnumerable<BaseEntity> list) : base(list.Cast<Player>().ToList()) { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
