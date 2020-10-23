using System.Collections.Generic;
using System.Data.SqlClient;

namespace TVA.DATA.DAL.Entity.Parameters
{
    public class SQLParameters
    {
        private readonly List<SqlParameter> _parameter;

        public SQLParameters()
        {
            _parameter = new List<SqlParameter>();
        }

        public void Add_Parameter(string parameter, object value)
        {
            _parameter.Add(new SqlParameter(parameter, value));
        }

        public SqlParameter[] ToArray()
        {
            return _parameter.ToArray();
        }

        public List<SqlParameter> GetListParam()
        {
            return _parameter;
        }
    }
}
