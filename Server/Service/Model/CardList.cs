using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CardList : List<Card>
    {
        public CardList() { }

        public CardList(IEnumerable<Card> list) : base(list) { }

        public CardList(IEnumerable<BaseEntity> list) : base(list.Cast<Card>().ToList()) { }
    }
}
