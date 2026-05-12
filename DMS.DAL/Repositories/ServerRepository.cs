using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using System.Data;

namespace DMS.DAL.Repositories
{
    public class ServerRepository : IServerRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IDbConnection _db;

        public ServerRepository(IDbConnection db, ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _db = db;
        }

        public async Task<int> InsertAsync(Server model)
        {
            var sql = @"
INSERT INTO SERVERS
(
    ServerName,
    ServerIP,
    DatabaseServer,
    StorageRootPath,
    IsActive,
    CreatedBy
)
VALUES
(
    @ServerName,
    @ServerIP,
    @DatabaseServer,
    @StorageRootPath,
    1,
    @CreatedBy
)

SELECT CAST(SCOPE_IDENTITY() as int)
";

            using var connection = _connectionFactory.CreateConnection();

            return await _db.ExecuteScalarAsync<int>(sql, model);
        }

        public async Task<int> UpdateAsync(Server model)
        {
            var sql = @"
UPDATE SERVERS
SET
    ServerName = @ServerName,
    ServerIP = @ServerIP,
    DatabaseServer = @DatabaseServer,
    StorageRootPath = @StorageRootPath,
    UpdatedBy = @CreatedBy,
    UpdatedOn = GETDATE()
WHERE ServerID = @ServerID
";

            using var connection = _connectionFactory.CreateConnection();

            return await _db.ExecuteAsync(sql, model);
        }

        public async Task<int> DeleteAsync(int serverId)
        {
            var sql = @"
DELETE FROM SERVERS
WHERE ServerID = @serverId
";

            using var connection = _connectionFactory.CreateConnection();

            return await _db.ExecuteAsync(sql, new { serverId });
        }

        public async Task<IEnumerable<Server>> GetAllAsync()
        {
            var sql = @"SELECT * FROM SERVERS ORDER BY ServerName";

            using var connection = _connectionFactory.CreateConnection();

            return await _db.QueryAsync<Server>(sql);
        }

        public async Task<Server> GetByIdAsync(int serverId)
        {
            var sql = @"SELECT * FROM SERVERS
                        WHERE ServerID = @serverId";

            using var connection = _connectionFactory.CreateConnection();

            return await _db.QueryFirstOrDefaultAsync<Server>(sql, new { serverId });
        }
    }
}
