using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class PlayerCardDB : BaseDB
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
            con.SideB = (int)reader["card_id"];
            con.Connection_type = "player-card";
            return con;
        }

        public ConnectionList SelectByGameID(int card_id)
        {
            command.CommandText = ("SELECT * FROM Player_Card_Table WHERE 'card_id'= @id");


            //parameters
            command.Parameters.Add(new OleDbParameter("@id", card_id));


            ConnectionList con_list = new ConnectionList(Select());
            return con_list;
        }

        public ConnectionList SelectByPlayerID(int player_id)
        {
            command.CommandText = ("SELECT * FROM Player_Card_Table WHERE 'player_id'= @id");


            //parameters
            command.Parameters.Add(new OleDbParameter("@id", player_id));


            ConnectionList con_list = new ConnectionList(Select());
            return con_list;
        }

        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            Connection con = entity as Connection;

            command.CommandText = ("INSERT INTO Player_Card_Table (player_id, card_id) VALUES (@player_id, @card_id)");

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.SideA));
            command.Parameters.Add(new OleDbParameter("@card_id", con.SideB));

            Console.WriteLine("connection between player" + con.SideA + " and card" + con.SideB + " has been created and inserted");
        }

        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            Connection con = entity as Connection;

            command.CommandText = ("DELETE FROM Player_Card_Table WHERE player_id = @player_id AND card_id = @card_id");

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.SideA));
            command.Parameters.Add(new OleDbParameter("@card_id", con.SideB));

            Console.WriteLine("connection between player" + con.SideA + " and card" + con.SideB + " has been deleted");
        }


        //BUG
        //cannot update the SET the WHERE parameters
        //solution: use insert and delete for every player-card connection(delete the old, insert updated)

        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            Connection con = entity as Connection;

            command.CommandText = ("UPDATE Player_Card_Table SET card_id = @card_id");

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.SideA));
            command.Parameters.Add(new OleDbParameter("@card_id", con.SideB));

            Console.WriteLine("connection between player" + con.SideA + " and card" + con.SideB + " has been updated");
        }
    }
}

