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
    public abstract class BaseDB
    {

        //database
        protected string ConnectionString;
        protected OleDbConnection Connection;
        protected OleDbCommand Command;
        protected OleDbDataReader Reader;

 

        //base entity
        protected abstract BaseEntity NewEntity();



        //base model
        protected abstract BaseEntity CreateModel(BaseEntity entity);



        //update, insert, delete
        protected static List<ChangeEntity> Inserted = new List<ChangeEntity>();
        protected static List<ChangeEntity> Updated = new List<ChangeEntity>();



        public abstract void CreateInsertSql(BaseEntity entity, OleDbCommand command);
        public abstract void CreateUpdateSql(BaseEntity entity, OleDbCommand command);
        public abstract void CreateDeleteSql(BaseEntity entity, OleDbCommand command);



        public virtual void Insert(BaseEntity entity)
        {

            BaseEntity reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                Inserted.Add(new ChangeEntity(this.CreateInsertSql, entity));
            }
        }

        public virtual void Update(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                Updated.Add(new ChangeEntity(this.CreateUpdateSql, entity));
            }
        }

        public virtual void Delete(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                Updated.Add(new ChangeEntity(this.CreateDeleteSql, entity));
            }
        }

        /*---------------------------------------------------------------*/

        public BaseDB()
        {

            string s1 = "";

            if (ConnectionString == null)
            {
                String[] arguments = Environment.GetCommandLineArgs();
                string s;
                if (arguments.Length == 1) // direct execution
                { s = arguments[0]; }
                else  // service execution
                {
                    s = arguments[1];
                    s = s.Replace("/service:", "");  // remove /service: from the begining of the command line
                }
                string[] ss = s.Split('\\');   // פירוק המחרוזת למערך (ע"פ / שמפריד

                int x = ss.Length - 4;  //הורדתי 3 תיקיות מהסוף
                ss[x] = "ViewModel";   // ....שיניתי את התיקיה האחרונה ל
                Array.Resize(ref ss, x + 1);  // תיקון של אורך המערך, לאורך העכשווי

                s1 = String.Join("\\", ss);  // חיבור מחדש של המערך - עם / מפריד
            }


            ConnectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + s1 + @"\Database.accdb; Persist Security Info = True";

            Connection = new OleDbConnection(ConnectionString);
            Command = new OleDbCommand();
        }

        /*---------------------------------------------------------------*/

        public int SaveChanges()
        {

            int recordsAffected = 0;
            try
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = Connection;
                Connection.Open();

                //inserted
                foreach (var item in Inserted)
                {

                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    recordsAffected += command.ExecuteNonQuery();

                    command.CommandText = "SELECT @@identity "; //get last ID
                    item.Entity.Id = (int)command.ExecuteScalar();
                }
                Inserted.Clear();

                //updated, deleted
                foreach (var item in Updated)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    recordsAffected += command.ExecuteNonQuery();
                }
                Updated.Clear();

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + Command.CommandText);
            }
            finally
            {
                Reader?.Close();

                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            return recordsAffected;
        }

        /*---------------------------------------------------------------*/

        protected List<BaseEntity> Select()
        {
            List<BaseEntity> list = new List<BaseEntity>();

            try
            {
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    BaseEntity entity = NewEntity();
                    list.Add(CreateModel(entity));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! " + e.Message);
            }
            finally
            {
                Reader?.Close();

                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            return list;
        }
    }
}
