using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{
    

    public class AIMetadataRepository
        : IAIMetadataRepository
    {
        public async Task SaveMetadataAsync(
            string connectionString,
            AIMetadata model)
        {
            var sql = @"

INSERT INTO AI_METADATA
(
    DocumentID,
    MetadataKey,
    MetadataValue,
    ConfidenceScore
)
VALUES
(
    @DocumentID,
    @MetadataKey,
    @MetadataValue,
    @ConfidenceScore
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

        public async Task SaveTagAsync(
            string connectionString,
            AITag model)
        {
            var sql = @"

INSERT INTO AI_TAGS
(
    DocumentID,
    TagName
)
VALUES
(
    @DocumentID,
    @TagName
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
    }
}
