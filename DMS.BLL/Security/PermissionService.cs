using Dapper;
using DMS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Security
{
    public class PermissionService
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public PermissionService(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> HasServerPermission(int roleId, int serverId,string permission)
        {
            var sql = $@" SELECT COUNT(1) FROM SERVER_PERMISSIONS
                          WHERE ServerID = @serverId AND RoleID = @roleId AND {permission} = 1 ";
            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.ExecuteScalarAsync<int>(sql,new
            {
                roleId,
                serverId
            }
            );
            return result > 0;
        }
    }
}
