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

            command.CommandText = ("SELECT * FROM Games_Table");
            GameList temp = new GameList(Select());
            return temp;

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
