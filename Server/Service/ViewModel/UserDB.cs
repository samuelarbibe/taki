using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.OleDb;
using Model;

namespace ViewModel
{
    public class UserDB : BaseDB
    {
        protected override BaseEntity newEntity()
        {
            return new User();
        }


        protected override BaseEntity CreateModel(BaseEntity entity)
        {

            User user = entity as User;
            user.First_name = reader["first_name"].ToString();
            user.Last_name = reader["last_name"].ToString();
            user.Id = (int)reader["ID"];
            user.Score = (int)reader["score"];
            return user;

        }

        public UserList SelectAll()
        {
            command.CommandText = ("SELECT * FROM User_Table");
            UserList temp = new UserList(Select());
            return temp;
        }
        

        public UserList SelectByName(string username)
        {
            command.CommandText = ("SELECT * FROM User_Table WHERE 'first_name'= @uName");


            //parameters
            command.Parameters.Add(new OleDbParameter("@uName", username));


            UserList temp = new UserList(Select());
            return temp;
        }

        public User SelectByID(int id)
        {

            command.CommandText = ("SELECT * FROM User_Table WHERE 'ID' = '@id'");

            //parameters
            command.Parameters.Add(new OleDbParameter("@Id", id));

            UserList temp = new UserList(Select());

            if (temp.Count() == 1)
            {
                return temp[0] as User;
            }
            return null;
        }

        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            User user = entity as User;

            command.CommandText = ("INSERT INTO User_Table (ID) VALUES (@id)");

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

