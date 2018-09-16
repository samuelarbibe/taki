using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class PlayerGameDB : BaseDB
    {   

        protected override BaseEntity newEntity()
        {
            return new Connection();
        }


        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Connection con = entity as Connection;
            con.Id = (int)reader["ID"];
            con.SideA = (int)reader["player_id"];
            con.SideB = (int)reader["game_id"];
            con.Connection_type = "player_game";
            return con;
        }


        public ConnectionList SelectByGameID(int game_id)
        {
            command.CommandText = ("SELECT * FROM Player_Game_Table WHERE 'game_id'= @id");


            //parameters
            command.Parameters.Add(new OleDbParameter("@id", game_id));


            ConnectionList con_list = new ConnectionList(Select());
            return con_list;
        }


        public ConnectionList SelectByPlayerID(int player_id)
        {
            command.CommandText = ("SELECT * FROM Player_Game_Table WHERE 'player_id'= @id");


            //parameters
            command.Parameters.Add(new OleDbParameter("@id", player_id));


            ConnectionList con_list = new ConnectionList(Select());
            return con_list;
        }


        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            Connection con = entity as Connection;

            command.CommandText = ("INSERT INTO Player_Game_Table (player_id, game_id) VALUES (@player_id, @game_id)");

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.SideA));
            command.Parameters.Add(new OleDbParameter("@game_id", con.SideB));

            Console.WriteLine("connection between player" + con.SideA + " and game" + con.SideB + " has been created and inserted");
        }


        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            Connection con = entity as Connection;

            command.CommandText = ("DELETE FROM Player_Game_Table WHERE player_id = @player_id AND game_id = @game_id");

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.SideA));
            command.Parameters.Add(new OleDbParameter("@game_id", con.SideB));

            Console.WriteLine("connection between player" + con.SideA + " and game" + con.SideB + " has been deleted");
        }

        //BaseDB abstract implemantation.
        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
