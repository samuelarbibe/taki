using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class CardDB : BaseDB
    {

        private static CardList list;


        protected override BaseEntity newEntity()
        {
            return new Card();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Card card = entity as Card;

            card.Color = reader["color"].ToString();
            card.Value = (int)reader["value"];
            card.Special = (bool)reader["special"];
            card.Image = reader["image"].ToString();
            card.Id = (int)reader["ID"];

            return card;
        }

        public CardList SelectAll()
        {
            command.CommandText = ("SELECT * FROM Cards_Table");
            CardList list = new CardList(base.Select());
            return list;
        }

        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            throw new NotImplementedException();
        }

        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            throw new NotImplementedException();
        }

        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
