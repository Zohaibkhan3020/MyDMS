
using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{

    public class FileVersionRepository : IFileVersionRepository
    {
        public async Task<int>
            GetNextVersionAsync(
                string connectionString,
                int fileId)
        {
            var sql = @"

SELECT ISNULL(MAX(VersionNo),0) + 1
FROM FILE_VERSIONS
WHERE FileID = @fileId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .ExecuteScalarAsync<int>(
                    sql,
                    new { fileId });
        }

        public async Task
            ResetCurrentVersionAsync(
                string connectionString,
                int fileId)
        {
            var sql = @"

UPDATE FILE_VERSIONS
SET IsCurrent = 0
WHERE FileID = @fileId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                new { fileId });
        }

        public async Task InsertAsync(
            string connectionString,
            FileVersion version)
        {
            var sql = @"

INSERT INTO FILE_VERSIONS
(
    DocumentID,
    FileID,
    VersionNo,
    VersionLabel,
    FilePath,
    FileSize,
    ContentType,
    UploadedBy,
    Comments,
    IsCurrent
)
VALUES
(
    @DocumentID,
    @FileID,
    @VersionNo,
    @VersionLabel,
    @FilePath,
    @FileSize,
    @ContentType,
    @UploadedBy,
    @Comments,
    @IsCurrent
)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                version);
        }

        public async Task<List<FileVersion>>
            GetVersionsAsync(
                string connectionString,
                int fileId)
        {
            var sql = @"

SELECT
    VersionID,
    VersionNo,
    VersionLabel,
    ContentType,
    FileSize,
    UploadedOn,
    Comments,
    IsCurrent
FROM FILE_VERSIONS
WHERE FileID = @fileId
ORDER BY VersionNo DESC

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var data =
                await connection
                    .QueryAsync<FileVersion>(
                        sql,
                        new { fileId });

            return data.ToList();
        }
    }
}
