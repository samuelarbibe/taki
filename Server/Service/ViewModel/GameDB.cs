using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class GameDB : BaseDB
    {

        private static GameList list;

        protected override BaseEntity newEntity()
        {
            return new Game();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            PlayerDB db = new PlayerDB();
            Game Game = entity as Game;

            Game.Id = (int)reader["ID"];
            Game.Players[0] = db.SelectByID((int)reader["player_1_ID"]);
            Game.Players[1] = db.SelectByID((int)reader["player_2_ID"]);
            Game.Players[2] = db.SelectByID((int)reader["player_3_ID"]);
            Game.Players[3] = db.SelectByID((int)reader["player_4_ID"]);

            Game.StartTime = DateTime.Now;

            return Game;
        }

        public GameList SelectAll()
        {

            command.CommandText = ("SELECT * FROM Game_Table");
            GameList temp = new GameList(Select());
            return temp;

        }

         public override void Delete(BaseEntity entity)
        {
            Game game = entity as Game;
            PlayerGameDB PGdb = new PlayerGameDB();//delete all connections rtelated to this game

            ConnectionList PG = PGdb.SelectByGameID(game.Id);


            if (p != null)
            {
                foreach (Connection c in PG)//delete all player-games connections to this Game id using PlayerGameDB.CreateDeleteSql
                {
                    updated.Add(new ChangeEntity(PGdb.CreateDeleteSql, c));
                }

                updated.Add(new ChangeEntity(this.CreateDeleteSql, entity));//delete the player itself
            }
        }


        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            Game game = entity as Game;

            command.CommandText = ("DELETE FROM Game_Table WHERE ID = @game_id");

            PlayerGameDB player-game = new PlayerGameDB();

            //parameters

            command.Parameters.Add(new OleDbParameter("@game_id", game.Id));

            Console.WriteLine("connection between player" + con.SideA + " and card" + con.SideB + " has been deleted");
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
