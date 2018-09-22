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
        private BaseEntity _entity;
        private CreateSql _createSql;


        public ChangeEntity(CreateSql createSql, BaseEntity entity)
        {
            this._entity = entity;
            this._createSql = createSql;
        }

        public BaseEntity Entity { get { return _entity; } set { _entity = value; } }
        public CreateSql CreateSql { get { return _createSql; } set { _createSql = value; } }
    }
}
