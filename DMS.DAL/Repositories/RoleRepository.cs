using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IDbConnection _db;

        public RoleRepository(IDbConnection db,ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _db = db;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            var sql = @"SELECT * FROM ROLES";

            //using var connection =  _connectionFactory.CreateConnection();

            return await _db.QueryAsync<Role>(sql);
        }

        public async Task<int> InsertAsync(Role model)
        {
            var sql = @"

INSERT INTO ROLES
(
    RoleName,
    Description
)
VALUES
(
    @RoleName,
    @Description
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            //using var connection = _connectionFactory.CreateConnection();

            return await _db.ExecuteScalarAsync<int>(sql,model);
        }
    }
}
