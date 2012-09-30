using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlServerCe;

namespace NiCris.DataAccess.Dapper
{
    public class ConnectionFactory
    {
        public static DbConnection GetOpenConnection()
        {
            var connStringName = ConfigurationManager.AppSettings.Get("NiCrisCeConnectionStringName");
            var connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            var conn = new SqlCeConnection(connString);
            // var conn = new SqlConnection(connString);

            conn.Open();
            return conn;
        }
    }
}
