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

        //public override void Delete(BaseEntity entity)
        //{
        //    LessonDB db = new LessonDB();

        //    LessonsList sl = db.SelectByStudent(entity as Student);

        //    Student student = entity as Student;
        //    if (student != null)
        //    {
        //        if (sl.Count != 0)
        //        {
        //            updated.Add(new ChangeEntity(db.CreateDeleteSql, entity));
        //        }
        //        updated.Add(new ChangeEntity(this.CreateDeleteSql, entity));
        //        updated.Add(new ChangeEntity(base.CreateDeleteSql, entity));
        //    }
        //}

        //public override void Update(BaseEntity entity)
        //{
        //    Student student = entity as Student;
        //    if (student != null)
        //    {
        //        updated.Add(new ChangeEntity(base.CreateUpdateSql, entity));
        //    }
        //}

        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            User user = entity as User;

            command.CommandText = ("INSERT INTO Player_Table (ID) VALUES (@id)");

            //parameters

            command.Parameters.Add(new OleDbParameter("@id", user.Id));
        }


        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            User student = entity as User;

            command.CommandText = ("DELETE FROM User_Table WHERE ID = @id");

            //parameters

            command.Parameters.Add(new OleDbParameter("@id", student.Id));
        }

        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
