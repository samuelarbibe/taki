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
            game.Players[4] = db.SelectById((int)Reader["deck_id"]);

            game.StartTime = DateTime.Now;

            return game;
        }

        public GameList SelectAll()
        {

            Command.CommandText = ("SELECT * FROM Game_Table");
            GameList temp = new GameList(Select());
            return temp;

        }

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
            throw new NotImplementedException();
        }

        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
