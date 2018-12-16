using System;
using System.Data.OleDb;
using Model;

namespace ViewModel
{
    public class PlayerGameDb : BaseDb
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
            con.SideB = (int) Reader["game_id"];
            con.ConnectionType = Model.Connection._connectionType.player_game;
            return con;
        }


        public void InsertList(ConnectionList entity)
        {
            ConnectionList cl = entity;
            foreach (Connection connection in cl)
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


        public ConnectionList SelectByGameId(int gameId)
        {
            Command.CommandText = "SELECT * FROM Player_Game_Table WHERE 'game_id'= @id";


            //parameters
            Command.Parameters.Add(new OleDbParameter("@id", gameId));


            ConnectionList conList = new ConnectionList(Select());
            return conList;
        }


        public ConnectionList SelectByPlayerId(int playerId)
        {
            Command.CommandText = "SELECT * FROM Player_Game_Table WHERE 'player_id'= @id";


            //parameters
            Command.Parameters.Add(new OleDbParameter("@id", playerId));


            ConnectionList conList = new ConnectionList(Select());
            return conList;
        }


        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            Connection con = entity as Connection;


            command.CommandText = "INSERT INTO Player_Game_Table (player_id, game_id) VALUES (@player_id, @game_id)";

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.SideA));
            command.Parameters.Add(new OleDbParameter("@game_id", con.SideB));

            Console.WriteLine("connection between player [" + con.SideA + "] and game [" + con.SideB +
                              "] has been created and inserted");
        }


        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            Connection con = entity as Connection;

            command.CommandText = "DELETE FROM Player_Game_Table WHERE player_id = @player_id AND game_id = @game_id";

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.SideA));
            command.Parameters.Add(new OleDbParameter("@game_id", con.SideB));

            Console.WriteLine("connection between player" + con.SideA + " and game" + con.SideB + " has been deleted");
        }

        //BaseDB abstract implementation.
        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}