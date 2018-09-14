using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Model
{

    [CollectionDataContract]
    public class CardList : List<Card>
    {

        public CardList() { }

        public CardList(IEnumerable<Card> list) : base(list) { }

        public CardList(IEnumerable<BaseEntity> list) : base(list.Cast<Card>().ToList()) { }
    }
}
