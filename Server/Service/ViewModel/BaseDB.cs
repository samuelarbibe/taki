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
        protected string connectionString;
        protected OleDbConnection connection;
        protected OleDbCommand command;
        protected OleDbDataReader reader;

 

        //base entity
        protected abstract BaseEntity newEntity();



        //base model
        protected abstract BaseEntity CreateModel(BaseEntity entity);



        //update, insert, delete
        protected static List<ChangeEntity> inserted = new List<ChangeEntity>();
        protected static List<ChangeEntity> updated = new List<ChangeEntity>();



        public abstract void CreateInsertSql(BaseEntity entity, OleDbCommand command);
        public abstract void CreateUpdateSql(BaseEntity entity, OleDbCommand command);
        public abstract void CreateDeleteSql(BaseEntity entity, OleDbCommand command);



        public virtual void Insert(BaseEntity entity)
        {

            BaseEntity reqEntity = this.newEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                inserted.Add(new ChangeEntity(this.CreateInsertSql, entity));
            }
        }

        public virtual void Update(BaseEntity entity)
        {
            BaseEntity reqEntity = this.newEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                updated.Add(new ChangeEntity(this.CreateUpdateSql, entity));
            }
        }

        public virtual void Delete(BaseEntity entity)
        {
            BaseEntity reqEntity = this.newEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
            {
                updated.Add(new ChangeEntity(this.CreateDeleteSql, entity));
            }
        }

        /*---------------------------------------------------------------*/

        public BaseDB()
        {
            connectionString =
           // @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\samue\Desktop\Taki\Server\Service\ViewModel\Database.accdb;Persist Security ////Info=True";
           @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Users\User1\Github\Server\Service\ViewModel\Database.accdb; Persist Security Info = True";

            connection = new OleDbConnection(connectionString);
            command = new OleDbCommand();
        }

        /*---------------------------------------------------------------*/

        public int SaveChanges()
        {

            int records_affected = 0;
            try
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                connection.Open();

                //inserted
                foreach (var item in inserted)
                {

                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records_affected += command.ExecuteNonQuery();

                    command.CommandText = "SELECT @@IDENTITY"; //get last ID
                    item.Entity.Id = (int)command.ExecuteScalar();
                }
                inserted.Clear();

                //updated, deleted
                foreach (var item in updated)
                {
                    command.Parameters.Clear();
                    item.CreateSql(item.Entity, command);
                    records_affected += command.ExecuteNonQuery();
                }
                updated.Clear();

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                reader?.Close();

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return records_affected;
        }

        /*---------------------------------------------------------------*/

        protected List<BaseEntity> Select()
        {
            List<BaseEntity> list = new List<BaseEntity>();

            try
            {
                command.Connection = connection;
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BaseEntity entity = newEntity();
                    list.Add(CreateModel(entity));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! " + e.Message);
            }
            finally
            {
                reader?.Close();

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return list;
        }
    }
}
