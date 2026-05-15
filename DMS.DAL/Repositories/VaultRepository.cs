using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;

namespace DMS.DAL.Repositories
{
    public class VaultRepository : IVaultRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public VaultRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InsertAsync(Vault model)
        {
            var sql = @"

INSERT INTO VAULTS
(
    ServerID,
    VaultName,
    DatabaseName,
    FileRootPath,
    IsActive
)
VALUES
(
    @ServerID,
    @VaultName,
    @DatabaseName,
    @FileRootPath,
    1
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<Vault>> GetAllAsync(int ServerID)
        {
            var sql = @"SELECT * FROM Vaults WHERE ServerID = @ServerID ORDER BY VaultName";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Vault>(sql, new { ServerID });
        }

        public async Task<Vault>GetByIdAsync(int vaultId)
        {
            var sql = @"SELECT * FROM VAULTS WHERE VaultID = @vaultId";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<Vault>(sql,new { vaultId });
        }
    }
}
