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
    public class UserDb : BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new User();
        }


        protected override BaseEntity CreateModel(BaseEntity entity)
        {

            User user = entity as User;
            user.Id = (int) Reader["ID"];
            user.Username = Reader["username"].ToString();
            user.Password = Reader["password"].ToString();
            user.FirstName = Reader["first_name"].ToString();
            user.LastName = Reader["last_name"].ToString();
            user.Score = (int) Reader["score"];
            user.Level = (int)Reader["level"];
            user.Admin = Reader["admin"] as bool? ?? false;
            user.Wins = (int) Reader["wins"];
            user.Losses = (int )Reader["losses"];
            return user;

        }

        public UserList SelectAll()
        {
            Command.CommandText = ("SELECT * FROM User_Table");
            UserList temp = new UserList(Select());
            return temp;
        }
        

        public UserList SelectByName(string firstName, string lastName)
        {
            Command.CommandText = ("SELECT * FROM User_Table WHERE 'first_name'= @fName AND 'last_name' = @lName");


            //parameters
            Command.Parameters.Add(new OleDbParameter("@fName", firstName));
            Command.Parameters.Add(new OleDbParameter("@lName", lastName));


            UserList temp = new UserList(Select());
            return temp;
        }


        public UserList SelectByUsernameAndPassword(string username, string password)
        {
            Command.CommandText = ("SELECT * FROM User_Table WHERE [username] = @uName AND [password] = @pWord");


            //parameters
            Command.Parameters.Add(new OleDbParameter("@uName", username));
            Command.Parameters.Add(new OleDbParameter("@pWord", password));


            UserList temp = new UserList(Select());
            return temp;
        }


        public User SelectById(int id)
        {

            Command.CommandText = ("SELECT * FROM User_Table WHERE 'ID' = '@id'");

            //parameters
            Command.Parameters.Add(new OleDbParameter("@Id", id));

            UserList temp = new UserList(Select());

            if (temp.Count() == 1)
            {
                return temp[0] as User;
            }
            return null;
        }

        public User SelectByPassword(string password)
        {

            Command.CommandText = ("SELECT * FROM User_Table WHERE [password] = @Password");

            //parameters
            Command.Parameters.Add(new OleDbParameter("@Password", password));

            UserList temp = new UserList(Select());

            if (temp.Count() == 1)
            {
                return temp[0] as User;
            }
            return null;
        }

        public User SelectByUsername(string username)
        {

            Command.CommandText = ("SELECT * FROM User_Table WHERE [username] = @Username");

            //parameters
            Command.Parameters.Add(new OleDbParameter("@Username", username));

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

            command.CommandText = ("INSERT INTO User_Table ( [username], [password], first_name, last_name) VALUES ( @username,  @password, @firstName, @lastName)");

            //parameters

            command.Parameters.Add(new OleDbParameter("@username", user.Username));
            command.Parameters.Add(new OleDbParameter("@password", user.Password));
            command.Parameters.Add(new OleDbParameter("@firstName", user.FirstName));
            command.Parameters.Add(new OleDbParameter("@lastName", user.LastName));
        }


        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            User user = entity as User;

            command.CommandText = ("DELETE FROM User_Table WHERE ID = @id");

            //parameters

            command.Parameters.Add(new OleDbParameter("@id", user.Id));
        }
    
        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}

