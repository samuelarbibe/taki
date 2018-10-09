using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class CardDb : BaseDB
    {

        private static CardList _list;


        protected override BaseEntity NewEntity()
        {
            return new Card();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Card card = entity as Card;

            card.Color = Reader["color"].ToString();
            card.Value = (int)Reader["value"];
            card.Special = (bool)Reader["special"];
            card.Image = Reader["image"].ToString();
            card.Id = (int)Reader["ID"];

            return card;
        }

        public CardList SelectAll()
        {
            Command.CommandText = ("SELECT * FROM Card_Table");
            CardList list = new CardList(base.Select());
            return list;
        }


        public CardList SelectById(int id)
        {

            Command.CommandText = ("SELECT * FROM Card_Table WHERE 'ID' = '@id'");

            //parameters
            Command.Parameters.Add(new OleDbParameter("@Id", id));

            CardList temp = new CardList(Select());

            if (temp.Count() > 0)
            {
                return temp;
            }
            return null;
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
