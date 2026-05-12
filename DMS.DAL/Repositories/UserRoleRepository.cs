
using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;

namespace DMS.DAL.Repositories
{

    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public UserRoleRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InsertAsync(UserRole model)
        {
            var sql = @"

INSERT INTO USER_ROLES
(
    UserID,
    RoleID
)
VALUES
(
    @UserID,
    @RoleID
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<int>(sql,model);
        }
    }
}
