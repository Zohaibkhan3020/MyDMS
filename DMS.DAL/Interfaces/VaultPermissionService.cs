using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public class VaultPermissionService
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public VaultPermissionService(
            ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> CanCreateVault(
            int serverId,
            int roleId)
        {
            var sql = @"

SELECT COUNT(1)
FROM SERVER_PERMISSIONS
WHERE ServerID = @serverId
AND RoleID = @roleId
AND CanCreateVault = 1

";

            using var connection =
                _connectionFactory.CreateConnection();

            var result =
                await connection.ExecuteScalarAsync<int>(
                    sql,
                    new
                    {
                        serverId,
                        roleId
                    });

            return result > 0;
        }
    }
}
