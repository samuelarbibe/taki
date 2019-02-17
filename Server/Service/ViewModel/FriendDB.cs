using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class FriendDb : BaseDb
    {
        protected override BaseEntity NewEntity()
        {
            return new Friendship();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            UserDb userDb = new UserDb();

            Friendship con = entity as Friendship;
            con.Id = (int)Reader["ID"];
            con.User1 = userDb.SelectById((int)Reader["user_1_id"]);
            con.User2 = userDb.SelectById((int)Reader["user_2_id"]);
            return con;
        }

        public UserList GetAllUserFriends(int userId)
        {
            UserList ul = new UserList();
            ConnectionList temp = this.SelectByUserId(userId);

            foreach(Friendship fr in temp)
            {
                if(fr.User1.Id == userId)
                {
                    ul.Add(fr.User2);
                }
                else
                {
                    ul.Add(fr.User1);
                }
            }

            return ul;
        }

        public ConnectionList SelectByUserId(int id)
        {
            Command.CommandText = "SELECT * FROM Friends_Table WHERE ([user_1_id] = @id) OR ([user_2_id] = @id)";


            //parameters
            Command.Parameters.Add(new OleDbParameter("@id", id));


            ConnectionList conList = new ConnectionList(Select());
            return conList;
        }

        public ConnectionList SelectByUsersId(int id1, int id2)
        {
            Command.CommandText = "SELECT * FROM Friends_Table WHERE ([user_1_id] = @id1 AND [user_2_id] = @id2) OR ([user_1_id] = @id2 AND [user_2_id] = @id1)";


            //parameters
            Command.Parameters.Add(new OleDbParameter("@id1", id1));
            Command.Parameters.Add(new OleDbParameter("@id2", id2));


            ConnectionList conList = new ConnectionList(Select());
            return conList;
        }

        public override void Insert(BaseEntity baseEntity)
        {
            if (baseEntity is Friendship) {
                if (((Friendship)baseEntity).User1 != null && ((Friendship)baseEntity).User2 != null)
                {
                    Inserted.Add(new ChangeEntity(CreateInsertSql, baseEntity));
                }
            }
        }

        public override void Delete(BaseEntity baseEntity)
        {
            if (baseEntity is Friendship)
            {
                Updated.Add(new ChangeEntity(CreateDeleteSql, baseEntity));
            }
        }

        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            Friendship con = entity as Friendship;

            command.CommandText = "DELETE FROM Friends_table WHERE [ID] = @id ";

            //parameters
            command.Parameters.Add(new OleDbParameter("@id", con.Id));

            Console.WriteLine("Friendship between player [" + con.User1.Id + "] and card [" + con.User2.Id + "] DELETED");
        }

        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            Friendship con = entity as Friendship;

            command.CommandText = "INSERT INTO Friends_Table (user_1_id, user_2_id) VALUES (@id1, @id2)";

            //parameters

            command.Parameters.Add(new OleDbParameter("@id1", con.User1.Id));
            command.Parameters.Add(new OleDbParameter("@id2", con.User1.Id));

            Console.WriteLine("Friendship between player [" + con.User1.Id + "] and card [" + con.User2.Id +
                              "] INSERTED");
        }

        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            throw new NotImplementedException();
        }

    }
}
