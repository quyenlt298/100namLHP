using System.Data;
using TVA.DATA.DAL.Entity.Parameters;
using static TVA.DATA.DAL.Entity.DataType.ActionType;

namespace TVA.DATA.DAL.IData.IDatabase
{
    public interface IConnectDatabase
    {
        void ExecuteNonQuery(string query, SQLParameters parameter, ExecuteType type);

        DataTable ExecuteToTable(string query, SQLParameters parameter, ExecuteType type);

        DataSet ExecuteToDataset(string query, SQLParameters parameter, ExecuteType type);

        T ExecuteScalar<T>(string query, SQLParameters parameter, ExecuteType type);
    }
}
