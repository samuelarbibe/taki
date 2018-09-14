using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using Model;

namespace ViewModel
{
    public class PlayerDB : UserDB
    {
        protected override BaseEntity newEntity()
        {
            return new User();
        }


        protected override BaseEntity CreateModel(BaseEntity entity)
        {

            Player p = entity as Player;
            base.CreateModel(p);
            p.Hand = null;
            p.Temp_score = 0;
            p.Game_id = 0;
            return p;
        }

        public PlayerList SelectAll()
        {

            command.CommandText = ("SELECT *, User_Table.ID as id FROM (User_Table INNER JOIN  PlayerTable ON User_Table.ID = Player_Table.user_id)");
            PlayerList temp = new PlayerList(Select());
            return temp;

        }


        //public UserList SelectByName(string FirstName, string LastName)
        //{
        //    command.CommandText = ("SELECT * FROM User_Table WHERE 'first_name'= @fName AND 'last_name' = @lName");


        //    //parameters
        //    command.Parameters.Add(new OleDbParameter("@fName", FirstName));
        //    command.Parameters.Add(new OleDbParameter("@lName", LastName));


        //    UserList temp = new UserList(Select());
        //    return temp;
        //}

        public Player SelectByID(int id)
        {

            command.CommandText = ("SELECT * FROM Player_Table WHERE 'ID' = '@id'");

            //parameters
            command.Parameters.Add(new OleDbParameter("@Id", id));

            PlayerList temp = new PlayerList(Select());

            if (temp.Count() == 1)
            {
                return temp[0] as Player;
            }
            return null;
        }

        public override void Insert(BaseEntity entity)
        {
            Player p = entity as Player;
            if (p != null)
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSql, entity));
            }
        }
 


        public override void Delete(BaseEntity entity)
        {
            Player p = entity as Player;
            PlayerCardDB PCdb = new PlayerCardDB();
            PlayerGameDB PGdb = new PlayerGameDB();

            ConnectionList PC = PCdb.SelectByPlayerID(p.Id);
            ConnectionList PG = PGdb.SelectByPlayerID(p.Id);


            if (p != null)
            {
                foreach(Connection c in PC)//delete all cards connections to this player using PlayerCardDB
                {
                    updated.Add(new ChangeEntity(PCdb.CreateDeleteSql, c));
                }

                foreach (Connection c in PG)//delete all games connections to this player using PlayerGameDB
                {
                    updated.Add(new ChangeEntity(PGdb.CreateDeleteSql, c));
                }

                updated.Add(new ChangeEntity(this.CreateDeleteSql, entity));//delete the player itself
            }
        }

        public override void Update(BaseEntity entity)
        {
            Player player = entity as Player;
            if (player != null)
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSql, entity));
            }
        }

        //this function recieves a USER entity, and creats a new player for that user, therfore, it uses User.id
        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            User user = entity as User;

            command.CommandText = ("INSERT INTO Player_Table (user_id) VALUES (@id)");

            //parameters

            command.Parameters.Add(new OleDbParameter("@id", user.Id));
        }


        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            User user = entity as User;

            command.CommandText = ("DELETE FROM User_Table WHERE user_id = @id");

            //parameters

            command.Parameters.Add(new OleDbParameter("@id", user.Id));
        }


        //only used to update the score
        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            Player player = entity as Player;

            command.CommandText = ("UPDATE Player_Table SET temp_score = @score");

            //parameters

            command.Parameters.Add(new OleDbParameter("@score", player.Temp_score));
        }
    }
}
