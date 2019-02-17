using System.Data.OleDb;
using Model;

namespace ViewModel
{
    //create SQL
    public delegate void CreateSql(BaseEntity entity, OleDbCommand command);

    public class ChangeEntity
    {
        public ChangeEntity(CreateSql createSql, BaseEntity entity)
        {
            Entity = entity;
            CreateSql = createSql;
        }

        public BaseEntity Entity { get; set; }

        public CreateSql CreateSql { get; set; }
    }
}