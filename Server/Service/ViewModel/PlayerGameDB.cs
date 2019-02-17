using System;
using System.Data.OleDb;
using Model;

namespace ViewModel
{
    public class PlayerGameDb : BaseDb
    {
        protected override BaseEntity NewEntity()
        {
            return new PlayerGameConnection();
        }


        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            PlayerDb playerDb = new PlayerDb();
            GameDb gameDb = new GameDb();

            PlayerGameConnection con = entity as PlayerGameConnection;
            con.Id = (int) Reader["ID"];
            con.Player = playerDb.GetPlayerById((int) Reader["player_id"]);
            con.Game = gameDb.GetGameById((int) Reader["game_id"]);
            return con;
        }


        public void InsertList(ConnectionList entity)
        {
            ConnectionList cl = entity;
            foreach (PlayerGameConnection playerGameConnection in cl)
                if (playerGameConnection != null)
                    Inserted.Add(new ChangeEntity(CreateInsertSql, playerGameConnection));
        }


        public void Insert(Game game)
        {
            //Inserted.Add(new ChangeEntity(CreateInsertSql, game));
            foreach (Player p in game.Players)
            {
                Inserted.Add(new ChangeEntity(CreateInsertSql, new PlayerGameConnection()
                {
                    Player = p, // increment the player id for each player
                    Game = game // the game id will be the same when inserted
                }));
            }
        }


        public ConnectionList SelectByGame(Game game)
        {
            Command.CommandText = "SELECT * FROM Player_Game_Table WHERE 'game_id'= @id";


            //parameters
            Command.Parameters.Add(new OleDbParameter("@id", game.Id));


            ConnectionList conList = new ConnectionList(Select());
            return conList;
        }


        public ConnectionList SelectByPlayer(Player player)
        {
            Command.CommandText = "SELECT * FROM Player_Game_Table WHERE [player_id] = @id";


            //parameters
            Command.Parameters.Add(new OleDbParameter("@id", player.Id));


            ConnectionList conList = new ConnectionList(Select());
            return conList;
        }

        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            PlayerGameConnection con = entity as PlayerGameConnection;


            command.CommandText = "INSERT INTO Player_Game_Table (player_id, game_id) VALUES (@player_id, @game_id)";

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.Player.Id));
            command.Parameters.Add(new OleDbParameter("@game_id", con.Game.Id));

            Console.WriteLine("PlayerGameConnection between player [" + con.Player.Id + "] and game [" + con.Game.Id +
                              "] INSERTED");
        }


        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            PlayerGameConnection con = entity as PlayerGameConnection;

            command.CommandText = "DELETE FROM Player_Game_Table WHERE player_id = @player_id AND game_id = @game_id";

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.Player.Id));
            command.Parameters.Add(new OleDbParameter("@game_id", con.Game.Id));

            Console.WriteLine("PlayerGameConnection between player [" + con.Player.Id + "] and game [" + con.Game.Id +
                              "] DELETED");
        }

        //BaseDB abstract implementation.
        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}