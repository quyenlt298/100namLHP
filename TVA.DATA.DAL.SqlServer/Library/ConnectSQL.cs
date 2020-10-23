using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System;
using TVA.DATA.DAL.IData.IDatabase;
using TVA.DATA.DAL.Entity.Parameters;
using static TVA.DATA.DAL.Entity.DataType.ActionType;
using TVA.DATA.DAL.SqlServer.Configs;

namespace DVG.BDS.DAL.SQLServer.Library
{
    public class ConnectSQL : IConnectDatabase
    {
        #region attribute

        private readonly string _connectionString = string.Empty;
        private readonly string _connectionStringMap = string.Empty;
        private readonly int _connectDbTimeOut = 30000;
        #endregion atribute

        #region Constructor
        public ConnectSQL(IConfiguration configuration, string groupSettingKey)
        {
            _connectionString = configuration[$"{groupSettingKey}:ConnectionString"];
            _connectionStringMap = configuration[$"{groupSettingKey}:ConnectionStringMap"];

            if (int.TryParse(configuration[$"{groupSettingKey}:ConnectDBTimeOut"], out int timeOut))
            {
                _connectDbTimeOut = timeOut;
            }
        }

        public ConnectSQL(IOptions<ConfigDb> options)
        {
            _connectionString = options.Value.ConnectionString;
            _connectDbTimeOut = options.Value.ConnectDbTimeOut;
        }
        #endregion Constructor

        #region Methods
        public void ExecuteNonQuery(string query, SQLParameters parameter, ExecuteType type)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();

                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = type == ExecuteType.StoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
                    cmd.CommandText = query;
                    cmd.CommandTimeout = _connectDbTimeOut;

                    if (parameter != null)
                        cmd.Parameters.AddRange(parameter.ToArray());

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public DataTable ExecuteToTable(string query, SQLParameters parameter, ExecuteType type)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();

                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.Parameters.Clear();
                    cmd.CommandType = type == ExecuteType.StoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
                    cmd.CommandText = query;
                    cmd.CommandTimeout = _connectDbTimeOut;

                    if (parameter != null)
                        cmd.Parameters.AddRange(parameter.ToArray());

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        adapter.Fill(tbl);
                        return tbl;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public DataSet ExecuteToDataset(string query, SQLParameters parameter, ExecuteType type)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();

                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.Parameters.Clear();
                    cmd.CommandType = type == ExecuteType.StoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
                    cmd.CommandText = query;
                    cmd.CommandTimeout = _connectDbTimeOut;

                    if (parameter != null)
                        cmd.Parameters.AddRange(parameter.ToArray());

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataSet dts = new DataSet();
                        adapter.Fill(dts);
                        return dts;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public T ExecuteScalar<T>(string query, SQLParameters parameter, ExecuteType type)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();

                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = type == ExecuteType.StoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
                    cmd.CommandText = query;
                    cmd.CommandTimeout = _connectDbTimeOut;

                    if (parameter != null)
                        cmd.Parameters.AddRange(parameter.ToArray());

                    object obj = cmd.ExecuteScalar();

                    if (obj == null)
                    {
                        return default(T);
                    }

                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }

                    return (T)obj;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion
    }
}