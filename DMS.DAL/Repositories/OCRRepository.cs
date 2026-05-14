using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{
    
    public class OCRRepository
        : IOCRRepository
    {
        public async Task SaveOCRTextAsync(
            string connectionString,
            OCRContent model)
        {
            var sql = @"

INSERT INTO OCR_CONTENT
(
    DocumentID,
    ExtractedText,
    ExtractedBy
)
VALUES
(
    @DocumentID,
    @ExtractedText,
    @ExtractedBy
)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection
                .ExecuteAsync(
                    sql,
                    model);
        }

        public async Task SaveMetadataAsync(
            string connectionString,
            OCRMetadata model)
        {
            var sql = @"

INSERT INTO OCR_METADATA
(
    DocumentID,
    MetadataKey,
    MetadataValue
)
VALUES
(
    @DocumentID,
    @MetadataKey,
    @MetadataValue
)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection
                .ExecuteAsync(
                    sql,
                    model);
        }

        public async Task<string>
            GetOCRTextAsync(
                string connectionString,
                int documentId)
        {
            var sql = @"

SELECT TOP 1 ExtractedText
FROM OCR_CONTENT
WHERE DocumentID = @documentId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            return await connection
                .ExecuteScalarAsync<string>(
                    sql,
                    new { documentId });
        }
    }
}
