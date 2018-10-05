using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [CollectionDataContract]
    public class UserList : List<User>
    {

        public UserList() { }

        public UserList(IEnumerable<User> list) : base(list) { }

        public UserList(IEnumerable<BaseEntity> list) : base(list.Cast<User>().ToList()) { }
    }
}
