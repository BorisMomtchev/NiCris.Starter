using System.Configuration;
using System.Data.Linq.Mapping;

namespace NiCris.DataAccess.SQL.LinqToSQL
{
    public static class DataContextFactory
    {
        private static readonly string _connectionString;
        private static readonly MappingSource _mappingSource;

        /// <summary>
        /// Static constructor.
        /// </summary>
        /// <remarks>
        /// Static initialization of connectionstring and mappingSource.
        /// This significantly increases performance, primarily due to mappingSource cache.
        /// </remarks>        
        static DataContextFactory()
        {
            string connectionStringName = ConfigurationManager.AppSettings.Get("NiCrisConnectionStringName");
            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            Database context = new Database(_connectionString);
            _mappingSource = context.Mapping.MappingSource;
        }

        /// <summary>
        /// Rapidly creates a new DataContext using cached connectionstring and mapping source.
        /// </summary>
        /// <returns></returns>
        public static Database CreateContext()
        {
            return new Database(_connectionString, _mappingSource);
        }
    }
}
