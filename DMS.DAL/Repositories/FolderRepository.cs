using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{
    
    public class FolderRepository
        : IFolderRepository
    {
        public async Task<int>
            CreateFolderAsync(
                string connectionString,
                Folder folder)
        {
            var sql = @"

INSERT INTO FOLDERS
(
    ParentFolderID,
    FolderName,
    FolderPath,
    FolderLevel,
    IsDeleted,
    CreatedBy
)
VALUES
(
    @ParentFolderID,
    @FolderName,
    @FolderPath,
    @FolderLevel,
    0,
    @CreatedBy
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .ExecuteScalarAsync<int>(
                    sql,
                    folder);
        }

        public async Task<Folder>
            GetFolderAsync(
                string connectionString,
                int folderId)
        {
            var sql = @"

SELECT *
FROM FOLDERS
WHERE FolderID = @folderId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .QueryFirstOrDefaultAsync<Folder>(
                    sql,
                    new { folderId });
        }

        public async Task<List<Folder>>
            GetAllFoldersAsync(
                string connectionString)
        {
            var sql = @"

SELECT *
FROM FOLDERS
WHERE IsDeleted = 0

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var result =
                await connection
                    .QueryAsync<Folder>(
                        sql);

            return result.ToList();
        }

        public async Task UpdateFolderPathAsync(
            string connectionString,
            int folderId,
            string path,
            int level)
        {
            var sql = @"

UPDATE FOLDERS
SET
    FolderPath = @path,
    FolderLevel = @level
WHERE FolderID = @folderId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection
                .ExecuteAsync(
                    sql,
                    new
                    {
                        folderId,
                        path,
                        level
                    });
        }

        public async Task MoveFolderAsync(
            string connectionString,
            int folderId,
            int? parentFolderId)
        {
            var sql = @"

UPDATE FOLDERS
SET ParentFolderID = @parentFolderId
WHERE FolderID = @folderId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection
                .ExecuteAsync(
                    sql,
                    new
                    {
                        folderId,
                        parentFolderId
                    });
        }
    }
}
