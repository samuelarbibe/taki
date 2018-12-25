using System;
using System.Data.OleDb;
using Model;

namespace ViewModel
{
    public class PlayerCardDb : BaseDb
    {
        protected override BaseEntity NewEntity()
        {
            return new Connection();
        }


        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Connection con = entity as Connection;
            con.Id = (int) Reader["ID"];
            con.SideA = (int) Reader["player_id"];
            con.SideB = (int) Reader["card_id"];
            con.ConnectionType = Model.Connection._connectionType.player_card;
            return con;
        }

        public ConnectionList SelectByGameId(int cardId)
        {
            Command.CommandText = "SELECT * FROM Player_Card_Table WHERE 'card_id'= @id";


            //parameters
            Command.Parameters.Add(new OleDbParameter("@id", cardId));


            ConnectionList conList = new ConnectionList(Select());
            return conList;
        }

        public ConnectionList SelectByPlayerId(int playerId)
        {
            Command.CommandText = "SELECT * FROM Player_Card_Table WHERE 'player_id'= @id";


            //parameters
            Command.Parameters.Add(new OleDbParameter("@id", playerId));


            ConnectionList conList = new ConnectionList(Select());
            return conList;
        }

        public Connection GetConnectionByPlayerIdAndCardId(int playerId, int cardId)
        {
            Command.CommandText = "SELECT * FROM Player_Card_Table WHERE [player_id] = @playerId AND [card_id] = @cardId";
            
            // parameters
            Command.Parameters.Add(new OleDbParameter("@playerId", playerId));
            Command.Parameters.Add(new OleDbParameter("@cardId", cardId));



            ConnectionList conList = new ConnectionList(Select());
            return conList[0];
        }

        public ConnectionList GetConnectionsByPlayerId(int playerId)
        {
            Command.CommandText = "SELECT * FROM Player_Card_Table WHERE [player_id] = @playerId";

            // parameters
            Command.Parameters.Add(new OleDbParameter("@playerId", playerId));

            ConnectionList conList = new ConnectionList(Select());
            return conList;
        }

        public void SwitchConnectionsByPlayersId(int right, int left)
        {
            ConnectionList hand1 = GetConnectionsByPlayerId(right);
            ConnectionList hand2 = GetConnectionsByPlayerId(left);

            foreach(Connection c in hand1)
            {
                c.SideA = left; // switch the owner of the cards to the other player
                Update(c); // update
            }

            foreach(Connection c in hand2)
            {
                c.SideA = right;// switch the owner of the cards to the other player
                Update(c); // update
            }

        }

        public void InsertList(ConnectionList entity)
        {
            ConnectionList cl = entity;
            foreach (var connection in cl)
                if (connection != null)
                    Inserted.Add(new ChangeEntity(CreateInsertSql, connection));
        }

        public override void Insert(BaseEntity entity)
        {
            if (entity is Connection c) Inserted.Add(new ChangeEntity(CreateInsertSql, entity));
        }

        public override void Delete(BaseEntity entity)
        {
            if (entity is Connection u) Inserted.Add(new ChangeEntity(CreateDeleteSql, entity));

            
        }

        public override void Update(BaseEntity entity)
        {
            if (entity is Connection u) Inserted.Add(new ChangeEntity(CreateUpdateSql, entity));
        }


        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            Connection con = entity as Connection;

            command.CommandText = "INSERT INTO Player_Card_Table (player_id, card_id) VALUES (@player_id, @card_id)";

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.SideA));
            command.Parameters.Add(new OleDbParameter("@card_id", con.SideB));

            Console.WriteLine("connection between player [" + con.SideA + "] and card [" + con.SideB +
                              "] has been created and inserted");
        }

        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            Connection con = entity as Connection;

            command.CommandText = "DELETE FROM Player_Card_Table WHERE [ID] = @id ";

            //parameters

            command.Parameters.Add(new OleDbParameter("@id", con.Id));

            Console.WriteLine("connection between player [" + con.SideA + "] and card [" + con.SideB +
                              "] has been deleted");
        }


        //BUG
        //cannot update the SET the WHERE parameters
        //solution: use insert and delete for every player-card connection(delete the old, insert updated)

        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            Connection con = entity as Connection;

            command.CommandText = "UPDATE Player_Card_Table SET [player_id] = @player_id WHERE [ID] = @id";

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.SideA));
            command.Parameters.Add(new OleDbParameter("@id", con.Id));

            Console.WriteLine("connection between player" + con.SideA + " and card" + con.SideB + " has been updated");
        }
    }
}