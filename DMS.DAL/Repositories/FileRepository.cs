
using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{
    public class FileRepository : IFileRepository
    {
        public async Task<int>
            InsertFileAsync(
                string connectionString,
                FileEntity file)
        {
            var sql = @"

INSERT INTO FILES
(
    FileName,
    OriginalFileName,
    FileExtension,
    FilePath,
    FileSize,
    ContentType,
    UploadedBy,
    IsDeleted
)
VALUES
(
    @FileName,
    @OriginalFileName,
    @FileExtension,
    @FilePath,
    @FileSize,
    @ContentType,
    @UploadedBy,
    0
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .ExecuteScalarAsync<int>(
                    sql,
                    file);
        }

        public async Task
            LinkDocumentFileAsync(
                string connectionString,
                DocumentFile model)
        {
            var sql = @"

INSERT INTO DOCUMENT_FILES
(
    DocumentID,
    FileID
)
VALUES
(
    @DocumentID,
    @FileID
)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                model);
        }

        public async Task<FileEntity>
    GetByIdAsync(
        string connectionString,
        int fileId)
        {
            var sql = @"

SELECT *
FROM FILES
WHERE FileID = @fileId
AND IsDeleted = 0

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .QueryFirstOrDefaultAsync<FileEntity>(
                    sql,
                    new { fileId });
        }

        public async Task<List<FilePreview>>
            GetByDocumentAsync(
                string connectionString,
                int documentId)
        {
            var sql = @"

SELECT

    F.FileID,

    F.FileName,

    F.OriginalFileName,

    F.FileExtension,

    F.ContentType,

    F.FileSize

FROM DOCUMENT_FILES DF

INNER JOIN FILES F
ON DF.FileID = F.FileID

WHERE DF.DocumentID = @documentId

AND F.IsDeleted = 0

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var data =
                await connection
                    .QueryAsync<FilePreview>(
                        sql,
                        new { documentId });

            return data.ToList();
        }
    }
}
