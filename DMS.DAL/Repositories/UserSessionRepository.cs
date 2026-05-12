using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;

namespace DMS.DAL.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public UserSessionRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory =  connectionFactory;
        }

        public async Task<int>InsertAsync(UserSession model)
        {
            var sql = @"

INSERT INTO USER_SESSIONS
(
    UserID,
    JwtToken,
    IPAddress,
    BrowserInfo,
    ExpiryTime,
    IsActive
)
VALUES
(
    @UserID,
    @JwtToken,
    @IPAddress,
    @BrowserInfo,
    @ExpiryTime,
    1
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<int>(sql, model);
        }

        public async Task DeactivateSessions(int userId)
        {
            var sql = @"

UPDATE USER_SESSIONS
SET IsActive = 0
WHERE UserID = @userId

";

            using var connection =    _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql,new { userId });
        }
    }
}
