using System;
using System.Data.OleDb;
using System.Linq;
using Model;

namespace ViewModel
{
    public class CardDb : BaseDb
    {


        protected override BaseEntity NewEntity()
        {
            return new Card();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Card card = entity as Card;

            card.COLOR = (Card.Color)Enum.Parse(typeof(Card.Color), Reader["color"].ToString());
            card.VALUE = (Card.Value)(int)Reader["value"];
            card.Special = (bool) Reader["special"];
            card.Image = Reader["source"].ToString();
            card.Id = (int) Reader["ID"];

            return card;
        }

        public CardList SelectAll()
        {
            Command.CommandText = "SELECT * FROM Card_Table";
            CardList list = new CardList(Select());
            return list;
        }


        public Card SelectById(int id)
        {
            Command.CommandText = "SELECT * FROM Card_Table WHERE [ID] = @id";

            //parameters
            Command.Parameters.Add(new OleDbParameter("@Id", id));

            CardList temp = new CardList(Select());

            if (temp.Any()) return temp[0];

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