using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using Model;

namespace ViewModel
{
    //create SQL
    public delegate void CreateSql(BaseEntity entity, OleDbCommand command);
    public class ChangeEntity
    {
        private BaseEntity entity;
        private CreateSql createSql;


        public ChangeEntity(CreateSql createSql, BaseEntity entity)
        {
            this.entity = entity;
            this.createSql = createSql;
        }

        public BaseEntity Entity { get { return entity; } set { entity = value; } }
        public CreateSql CreateSql { get { return createSql; } set { createSql = value; } }
    }
}
