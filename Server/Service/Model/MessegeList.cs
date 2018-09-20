using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [CollectionDataContract]
    public class MessegeList : List<Messege>
    {
        public MessegeList() { }

        public MessegeList(IEnumerable<Messege> list) : base(list) { }

        public MessegeList(IEnumerable<BaseEntity> list) : base(list.Cast<Messege>().ToList()) { }
    }
}