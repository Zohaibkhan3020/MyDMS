
using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using System.Data;

namespace DMS.DAL.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IDbConnection _db;

        public UserRepository(ISqlConnectionFactory connectionFactory,IDbConnection db)
        {
            _connectionFactory = connectionFactory;
            _db = db;
        }

        public async Task<int> InsertAsync(User model)
        {
            var sql = @"

INSERT INTO USERS
(
    FullName,
    Username,
    Email,
    PasswordHash,
    IsActive
)
VALUES
(
    @FullName,
    @Username,
    @Email,
    @PasswordHash,
    1
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            //using var connection = _connectionFactory.CreateConnection();

            return await _db.ExecuteScalarAsync<int>(sql,model);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var sql = @"SELECT * FROM USERS";

            //using var connection = _connectionFactory.CreateConnection();

            return await _db.QueryAsync<User>(sql);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var sql = @"SELECT * FROM USERS WHERE Username = @username";

            //using var connection = _connectionFactory.CreateConnection();

            return await _db.QueryFirstOrDefaultAsync<User>(sql,new { username });
        }
        public async Task<User> ValidateUserAsync(string username)
        {
            var sql = @"

                        SELECT *
                        FROM USERS
                        WHERE Username = @username
                        AND IsActive = 1

                        ";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { username });
        }
        public async Task UpdateLoginSuccess(int userId)
        {
            var sql = @"

UPDATE USERS
SET

    LastLogin = GETDATE(),

    FailedLoginAttempts = 0,
    
    IsLocked = 0

WHERE UserID = @userId

";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new { userId });
        }
        public async Task IncreaseFailedAttempts(int userId)
        {
            var sql = @"

UPDATE USERS
SET
    FailedLoginAttempts = ISNULL(FailedLoginAttempts, 0) + 1,
    LockoutEndTime = 
        CASE 
            WHEN ISNULL(FailedLoginAttempts, 0) + 1 >= 3 
            THEN DATEADD(MINUTE, 15, GETDATE())
            ELSE LockoutEndTime
        END,
    IsLocked = 
        CASE 
            WHEN ISNULL(FailedLoginAttempts, 0) + 1 >= 3 
            THEN 1 
            ELSE IsLocked 
        END
WHERE UserID = @userId;";


            using var connection =
                _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql,new { userId });
        }
    }
}
