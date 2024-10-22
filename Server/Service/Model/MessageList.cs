﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Model
{
    [CollectionDataContract]
    public class MessageList : List<Message>
    {
        public MessageList()
        {
        }

        public MessageList(IEnumerable<Message> list) : base(list)
        {
        }

        public MessageList(IEnumerable<BaseEntity> list) : base(list.Cast<Message>().ToList())
        {
        }
    }
}