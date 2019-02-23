using System;
using System.Data.OleDb;
using Model;

namespace ViewModel
{
    public class GameDb : BaseDb
    {
        protected override BaseEntity NewEntity()
        {
            return new Game();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            PlayerDb db = new PlayerDb();
            Game game = entity as Game;

            game.Id = (int) Reader["ID"];


            game.Players.Add((Player) db.GetPlayerById((int) Reader["player_1_id"]));
            game.Players.Add((Player) db.GetPlayerById((int) Reader["player_2_id"]));
            game.Players.Add((Player) db.GetPlayerById((int) Reader["player_3_id"]));
            game.Players.Add((Player) db.GetPlayerById((int) Reader["player_4_id"]));
            game.Players.Add((Player) db.GetPlayerById((int) Reader["table_id"]));


            game.StartTime = (DateTime) Reader["start_date"];
            game.EndTime = (DateTime) Reader["end_date"];
            game.Loser = (int) Reader["losser_id"];

            return game;
        }

        public GameList SelectAll()
        {
            Command.CommandText = "SELECT * FROM Game_Table";
            GameList temp = new GameList(Select());
            return temp;
        }

        public Game GetLastGame()
        {
            Command.CommandText = "SELECT * FROM Game_Table WHERE ID = (SELECT MAX(ID) FROM Game_Table)";
            GameList temp = new GameList(Select());
            return temp.Count > 0 ? temp[0] : new Game(0);
        }

        public Game GetGameById(int id)
        {
            Command.CommandText = "SELECT * FROM Game_Table WHERE [ID] = " + id + "";
            GameList temp = new GameList(Select());
            return temp.Count > 0 ? temp[0] : null;
        }

        public GameList SelectByUserId(int userId)
        {
            Command.CommandText = "SELECT * FROM Game_Table WHERE(ID IN " +
                                  "(SELECT Player_Game_Table.game_id" +
                                  " FROM(Player_Table INNER JOIN" +
                                  " Player_Game_Table ON Player_Table.ID = Player_Game_Table.player_id)" +
                                  " WHERE (Player_Table.user_id = @Id)))";

            Command.Parameters.Add(new OleDbParameter("@Id", userId));

            GameList temp = new GameList(Select());
            return temp;
        }

        public int GetLastGameId()
        {
            Command.CommandText = "SELECT MAX(ID) FROM Game_Table";
            GameList temp = new GameList(Select());
            return temp.Count > 0 ? temp[0].Id : new Game(0).Id;
        }


        public override void Insert(BaseEntity entity)
        {
            if (entity is Game u) Inserted.Add(new ChangeEntity(CreateInsertSql, entity));
        }

        public override void Update(BaseEntity entity)
        {
            if (entity is Game u) Updated.Add(new ChangeEntity(CreateUpdateSql, entity));
        }

        public override void Delete(BaseEntity entity)
        {
            Game game = entity as Game;
            PlayerGameDb pGdb = new PlayerGameDb(); //delete all connections related to this game

            ConnectionList pg = pGdb.SelectByGame(game);


            if (pg != null)
            {
                foreach (PlayerGameConnection c in pg
                ) //delete all player-games connections to this Game id using PlayerGameDB.CreateDeleteSql
                    Updated.Add(new ChangeEntity(pGdb.CreateDeleteSql, c));

                Updated.Add(new ChangeEntity(CreateDeleteSql, entity)); //delete the player itself
            }
        }


        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            Game game = entity as Game;

            command.CommandText = "DELETE FROM Game_Table WHERE ID = @game_id";

            //PlayerGameDB playerGame = new PlayerGameDB();

            //parameters

            command.Parameters.Add(new OleDbParameter("@game_id", game.Id));

            Console.WriteLine("All Connections and games related to this game have been deleted");
        }


        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            Game g = entity as Game;

            command.CommandText =
                "INSERT INTO Game_Table ([start_date], [end_date], [player_1_id], [player_2_id], [player_3_id], [player_4_id], [table_id], [losser_id]) VALUES (" +
                "'" + g.StartTime.ToString("G") + "', '" + g.EndTime.ToString("G") +
                "', @p1, @p2, @p3, @p4, @table, @loss)";

            command.Parameters.AddWithValue("@p1", g.Players[0].Id);
            command.Parameters.AddWithValue("@p2", g.Players[1].Id);

            switch (g.Players.Count)
            {
                case 3:

                    command.Parameters.AddWithValue("@p3", int.Parse("0"));
                    command.Parameters.AddWithValue("@p4", int.Parse("0"));
                    command.Parameters.AddWithValue("@table", g.Players[2].Id);

                    break;

                case 4:

                    command.Parameters.AddWithValue("@p3", g.Players[2].Id);
                    command.Parameters.AddWithValue("@p4", int.Parse("0"));
                    command.Parameters.AddWithValue("@table", g.Players[3].Id);
                    break;

                case 5:

                    command.Parameters.AddWithValue("@p3", g.Players[2].Id);
                    command.Parameters.AddWithValue("@p4", g.Players[3].Id);
                    command.Parameters.AddWithValue("@table", g.Players[4].Id);
                    break;
            }

            //parameters

            command.Parameters.AddWithValue("@loss", int.Parse("0"));

            Console.WriteLine("Game [" + g.Id + "] Added to the Database");
        }

        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            Game g = entity as Game;

            command.CommandText =
                "UPDATE Game_Table SET start_date = '" + g.StartTime.ToString("G") + "', end_date = '" +
                g.EndTime.ToString("G") +
                "', player_1_id = @p1, player_2_id = @p2, player_3_id = @p3, player_4_id = @p4, table_id = @table, losser_id = @losser";

            command.Parameters.AddWithValue("@p1", g.Players[0].Id);
            command.Parameters.AddWithValue("@p2", g.Players[1].Id);

            switch (g.Players.Count)
            {
                case 3:

                    command.Parameters.AddWithValue("@p3", int.Parse("0"));
                    command.Parameters.AddWithValue("@p4", int.Parse("0"));
                    command.Parameters.AddWithValue("@table", g.Players[2].Id);

                    break;

                case 4:

                    command.Parameters.AddWithValue("@p3", g.Players[2].Id);
                    command.Parameters.AddWithValue("@p4", int.Parse("0"));
                    command.Parameters.AddWithValue("@table", g.Players[3].Id);
                    break;

                case 5:

                    command.Parameters.AddWithValue("@p3", g.Players[2].Id);
                    command.Parameters.AddWithValue("@p4", g.Players[3].Id);
                    command.Parameters.AddWithValue("@table", g.Players[4].Id);
                    break;
            }

            //command.Parameters.AddWithValue("@sDate", (string)g.StartTime.ToString("G"));
            //command.Parameters.AddWithValue("@eDate", (string)g.StartTime.ToString("G"));
            command.Parameters.AddWithValue("@losser", g.Loser);
            Console.WriteLine("Game [" + g.Id + "] Updated");
        }
    }
}