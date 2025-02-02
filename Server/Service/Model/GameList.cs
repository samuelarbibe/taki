﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Model
{
    [CollectionDataContract]
    public class GameList : List<Game>
    {
        public GameList()
        {
        }

        public GameList(IEnumerable<Game> list) : base(list)
        {
        }

        public GameList(IEnumerable<BaseEntity> list) : base(list.Cast<Game>().ToList())
        {
        }
    }
}