using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PlayerList : List<Player>
    {
        public PlayerList() { }

        public PlayerList(IEnumerable<Player> list) : base(list) { }

        public PlayerList(IEnumerable<BaseEntity> list) : base(list.Cast<Player>().ToList()) { }
    }
}
