using System.Data.OleDb;
using Model;

namespace ViewModel
{
    //create SQL
    public delegate void CreateSql(BaseEntity entity, OleDbCommand command);

    public class ChangeEntity
    {
        private CreateSql _createSql;
        private BaseEntity _entity;


        public ChangeEntity(CreateSql createSql, BaseEntity entity)
        {
            _entity = entity;
            _createSql = createSql;
        }

        public BaseEntity Entity
        {
            get => _entity;
            set => _entity = value;
        }

        public CreateSql CreateSql
        {
            get => _createSql;
            set => _createSql = value;
        }
    }
}