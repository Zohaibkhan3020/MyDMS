using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Services
{
    public class DatabaseCreatorService
    {
        private readonly IConfiguration _configuration;

        public DatabaseCreatorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task CreateDatabaseAsync(string databaseName)
        {
            var connectionString =
                _configuration.GetConnectionString("MasterConnection");

            using var connection =
                new SqlConnection(connectionString);

            await connection.OpenAsync();

            var sql = $@"
IF NOT EXISTS
(
    SELECT *
    FROM sys.databases
    WHERE name = '{databaseName}'
)
BEGIN
    CREATE DATABASE [{databaseName}]
END
";

            await connection.ExecuteAsync(sql);
        }
    }
}
