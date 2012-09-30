using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Configuration;
using System.Data.SqlClient;

namespace NiCris.Web.Helpers
{
    public static class SecurityHelper
    {
        public static string GetLoggedInWindowsUserName(IPrincipal principal)
        {
            string user = (principal == null) ? string.Empty : principal.Identity.Name;
            return user;
        }

        public static string GetDataSource()
        {
            const string CONN_STRING_NAME = "NiCrisConnectionStringName";

            string connStringValue = ConfigurationManager.AppSettings[CONN_STRING_NAME];
            string connectionString = ConfigurationManager.ConnectionStrings[connStringValue].ConnectionString;

            var builder = new SqlConnectionStringBuilder(connectionString);
            var dataSource = builder.DataSource;

            return dataSource;
        }

        public static string GetInitialCatalog()
        {
            const string CONN_STRING_NAME = "NiCrisConnectionStringName";

            string connStringValue = ConfigurationManager.AppSettings[CONN_STRING_NAME];
            string connectionString = ConfigurationManager.ConnectionStrings[connStringValue].ConnectionString;

            var builder = new SqlConnectionStringBuilder(connectionString);
            var initialCatalog = builder.InitialCatalog;

            return initialCatalog;
        }
    }
}