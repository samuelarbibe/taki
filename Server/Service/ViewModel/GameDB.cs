using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class GameDb : BaseDb
    {

        private static GameList _list;

        protected override BaseEntity NewEntity()
        {
            return new Game();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            PlayerDb db = new PlayerDb();
            Game game = entity as Game;

            game.Id = (int)Reader["ID"];
            game.Players[0] = db.SelectById((int)Reader["player_1_id"]); 
            game.Players[1] = db.SelectById((int)Reader["player_2_id"]);
            game.Players[2] = db.SelectById((int)Reader["player_3_id"]);
            game.Players[3] = db.SelectById((int)Reader["player_4_id"]);
            game.Players[4] = db.SelectById((int)Reader["table_id"]);

            game.StartTime = DateTime.Now;

            return game;
        }


        public override void Insert(BaseEntity entity)
        {
            Game p = entity as Game;
            if (p != null)
            {
                Inserted.Add(new ChangeEntity(this.CreateInsertSql, entity));
            }
        }


        public GameList SelectAll()
        {

            Command.CommandText = ("SELECT * FROM Game_Table");
            GameList temp = new GameList(Select());
            return temp;
        }

        public Game GetLastGame()
        {
            Command.CommandText = ("SELECT TOP 1 * FROM Game_Table ORDER BY ID DESC");
            GameList temp = new GameList(Select());
            return temp.Count > 0 ? temp[0] : new Game(0);
        }

        //public Game SelectByPlayers(PlayerList p)
        //{

        //    Command.CommandText = ("SELECT * Game_Table (date, EndTime, player_1_id, player_2_id, player_3_id, player_4_id, table_id, winner_id) VALUES ( @date, @EndTime, @p1, @p2, @p3, @p4, @table, @win)");

        //    switch (p.Count)
        //    {
        //        case 3:
        //            Command.Parameters.Add(new OleDbParameter("@p1", p[0].Id));
        //            Command.Parameters.Add(new OleDbParameter("@p2", p[1].Id));
        //            Command.Parameters.Add(new OleDbParameter("@p3", 0));
        //            Command.Parameters.Add(new OleDbParameter("@p4", 0));
        //            Command.Parameters.Add(new OleDbParameter("@table", 0));
        //            break;

        //        case 4:
        //            Command.Parameters.Add(new OleDbParameter("@p1", p[0].Id));
        //            Command.Parameters.Add(new OleDbParameter("@p2", p[1].Id));
        //            Command.Parameters.Add(new OleDbParameter("@p3", p[2].Id));
        //            Command.Parameters.Add(new OleDbParameter("@p4", 0));
        //            Command.Parameters.Add(new OleDbParameter("@table", p[3].Id));
        //            break;

        //        case 5:
        //            Command.Parameters.Add(new OleDbParameter("@p1", p[0].Id));
        //            Command.Parameters.Add(new OleDbParameter("@p2", p[1].Id));
        //            Command.Parameters.Add(new OleDbParameter("@p3", p[2].Id));
        //            Command.Parameters.Add(new OleDbParameter("@p4", p[3].Id));
        //            Command.Parameters.Add(new OleDbParameter("@table", p[4].Id));
        //            break;

        //    }

        //    GameList temp = new GameList(Select());
        //    return temp[0];
        //}

        public override void Delete(BaseEntity entity)
        {
            Game game = entity as Game;
            PlayerGameDb pGdb = new PlayerGameDb();//delete all connections realted to this game

            ConnectionList pg = pGdb.SelectByGameId(game.Id);


            if (pg != null)
            {
                foreach (Connection c in pg)//delete all player-games connections to this Game id using PlayerGameDB.CreateDeleteSql
                {
                    Updated.Add(new ChangeEntity(pGdb.CreateDeleteSql, c));
                }

                Updated.Add(new ChangeEntity(this.CreateDeleteSql, entity));//delete the player itself
            }
        }


        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            Game game = entity as Game;

            command.CommandText = ("DELETE FROM Game_Table WHERE ID = @game_id");

            //PlayerGameDB playerGame = new PlayerGameDB();

            //parameters

            command.Parameters.Add(new OleDbParameter("@game_id", game.Id));

            Console.WriteLine("All Connections and games related to this game have been deleted");
        }


        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            Game g = entity as Game;

            command.CommandText = ("INSERT INTO Game_Table ([startDate], [endDate], [player_1_id], [player_2_id], [player_3_id], [player_4_id], [table_id], [winner_id]) VALUES ('"+ DateTime.Now.ToString("G") + "','"+DateTime.Now.ToString("G")+"', "+g.Players[0].Id+ ", " + g.Players[0].Id + ", 0,0, "+-g.Id+", 0)");

            //switch (g.Players.Count)
            //{
            //    case 3:
            //        command.Parameters.AddWithValue("@p1", (int)g.Players[0].Id);
            //        command.Parameters.AddWithValue("@p2", (int)g.Players[1].Id);
            //        command.Parameters.AddWithValue("@p3", (int)Int32.Parse("0"));
            //        command.Parameters.AddWithValue("@p4", (int)Int32.Parse("0"));
                    
            //        break;

            //    case 4:
            //        command.Parameters.AddWithValue("@p1", g.Players[0].Id);
            //        command.Parameters.AddWithValue("@p2", g.Players[1].Id);
            //        command.Parameters.AddWithValue("@p3", g.Players[2].Id);
            //        command.Parameters.AddWithValue("@p4", Int32.Parse("0"));
            //        break;

            //    case 5:
            //        command.Parameters.AddWithValue("@p1", g.Players[0].Id);
            //        command.Parameters.AddWithValue("@p2", g.Players[1].Id);
            //        command.Parameters.AddWithValue("@p3", g.Players[2].Id);
            //        command.Parameters.AddWithValue("@p4", g.Players[3].Id);
            //        break;
            //}

            ////parameters

            //command.Parameters.AddWithValue("@table", (int)-g.Id);
            //command.Parameters.AddWithValue("@sDate", (string)g.StartTime.ToString("G"));
            //command.Parameters.AddWithValue("@eDate", (string)g.StartTime.ToString("G"));
            //command.Parameters.AddWithValue("@win", (int)Int32.Parse("0"));
        }

        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
