using Microsoft.Extensions.Options;
using System;
using TVA.DATA.DAL.SqlServer.Configs;

namespace DVG.BDS.DAL.SQLServer.Configs
{
    public sealed class SQLConfigSingleton
    {
        private const string _SQL_CONFIG = "ConnectionString";
        private const string _SQL_TIMEOUT = "ConnectDbTimeOut";

        public string _connectionString { set; get; }
        public int _connectDbTimeOut { set; get; }

        private static volatile SQLConfigSingleton instance;

        private static object syncRoot = new Object();

        private ConfigDb _configuration { set; get; }

        private SQLConfigSingleton()
        {
            _connectionString = _configuration.ConnectionString;
            _connectDbTimeOut = _configuration.ConnectDbTimeOut;
        }

        private SQLConfigSingleton(IOptions<ConfigDb> settings)
        {
            _configuration = settings.Value;
            _connectionString = _configuration.ConnectionString;
            _connectDbTimeOut = _configuration.ConnectDbTimeOut;
        }

        public static SQLConfigSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SQLConfigSingleton();
                        }
                    }
                }

                return instance;
            }
        }
    }
}
