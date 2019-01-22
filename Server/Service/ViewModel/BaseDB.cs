using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using Model;

namespace ViewModel
{
    public abstract class BaseDb
    {
        //update, insert, delete
        protected static List<ChangeEntity> Inserted = new List<ChangeEntity>();
        protected static List<ChangeEntity> Updated = new List<ChangeEntity>();
        protected OleDbCommand Command;
        protected OleDbConnection con;

        //database
        protected string ConnectionString;
        protected OleDbDataReader Reader;

        /*---------------------------------------------------------------*/

        public BaseDb()
        {
            string s1 = "";

            if (ConnectionString == null)
            {
                string[] arguments = Environment.GetCommandLineArgs();
                string s;
                if (arguments.Length == 1)
                {
                    s = arguments[0];
                }
                else
                {
                    s = arguments[1];
                    s = s.Replace("/service:", "");
                }

                string[] ss = s.Split('\\');

                int x = ss.Length - 4;
                ss[x] = "ViewModel";
                Array.Resize(ref ss, x + 1);

                s1 = string.Join("\\", ss);
            }

            ConnectionString =
                @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + s1 +
                @"\Database.accdb; Persist Security Info = True";

            con = new OleDbConnection(ConnectionString);
            Command = new OleDbCommand();
        }


        //base entity
        protected abstract BaseEntity NewEntity();


        //base model
        protected abstract BaseEntity CreateModel(BaseEntity entity);


        public abstract void CreateInsertSql(BaseEntity entity, OleDbCommand command);
        public abstract void CreateUpdateSql(BaseEntity entity, OleDbCommand command);
        public abstract void CreateDeleteSql(BaseEntity entity, OleDbCommand command);


        public virtual void Insert(BaseEntity entity)
        {
            BaseEntity reqEntity = NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
                Inserted.Add(new ChangeEntity(CreateInsertSql, entity));
        }

        public virtual void Update(BaseEntity entity)
        {
            BaseEntity reqEntity = NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
                Updated.Add(new ChangeEntity(CreateUpdateSql, entity));
        }

        public virtual void Delete(BaseEntity entity)
        {
            BaseEntity reqEntity = NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
                Updated.Add(new ChangeEntity(CreateDeleteSql, entity));
        }

        /*---------------------------------------------------------------*/

        public int SaveChanges()
        {
            OleDbCommand command = new OleDbCommand();
            OleDbTransaction trans = null;

            int recordsAffected = 0;
            int errorIndex = 0;
            try
            {
                con.Open();
                trans = con.BeginTransaction();
                command.Connection = con;
                command.Transaction = trans;

                //inserted
                foreach (var item in Inserted)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    recordsAffected += command.ExecuteNonQuery();


                    command.CommandText = "SELECT @@Identity"; //get last ID on this session
                    int temp = (int)command.ExecuteScalar();
                    item.Entity.Id = temp;

                    errorIndex++;
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

                trans.Commit();
            }
            catch (Exception e)
            {               
                Debug.WriteLine("\n" + e.Message + "\nSQL:" + command.CommandText);

                try
                {
                    trans.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
            finally
            {
                Reader?.Close();

                if (con.State == ConnectionState.Open) con.Close();
            }

            return recordsAffected;
        }

        /*---------------------------------------------------------------*/

        protected List<BaseEntity> Select()
        {
            List<BaseEntity> list = new List<BaseEntity>();

            try
            {
                Command.Connection = con;
                con.Open();
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

                if (con.State == ConnectionState.Open) con.Close();
            }

            return list;
        }
    }
}