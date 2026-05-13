using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories
{
    public class DynamicRecordRepository : IDynamicRecordRepository
    {
        public async Task<int>
            InsertDocumentAsync(
                string connectionString,
                Document document)
        {
            var sql = @"

INSERT INTO DOCUMENTS
(
    ObjectTypeID,
    Title,
    CreatedBy,
    IsDeleted
)
VALUES
(
    @ObjectTypeID,
    @Title,
    @CreatedBy,
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
                    document);
        }

        public async Task InsertMetadataAsync(
            string connectionString,
            MetadataValue metadata)
        {
            var sql = @"

INSERT INTO METADATA_VALUES
(
    DocumentID,
    PropertyID,
    ValueText
)
VALUES
(
    @DocumentID,
    @PropertyID,
    @ValueText
)

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection.ExecuteAsync(
                sql,
                metadata);
        }

        public async Task<List<Document>>
    GetDocumentsAsync(
        string connectionString,
        int objectTypeId)
        {
            var sql = @"

SELECT *
FROM DOCUMENTS
WHERE ObjectTypeID = @objectTypeId
AND IsDeleted = 0

ORDER BY DocumentID DESC

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var data =
                await connection.QueryAsync<Document>(
                    sql,
                    new { objectTypeId });

            return data.ToList();
        }

        public async Task<List<dynamic>>
            GetMetadataAsync(
                string connectionString,
                int documentId)
        {
            var sql = @"

SELECT

    P.DisplayName,

    M.ValueText

FROM METADATA_VALUES M

INNER JOIN OBJECT_PROPERTIES P
ON M.PropertyID = P.PropertyID

WHERE M.DocumentID = @documentId

ORDER BY P.SortOrder

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var data =
                await connection.QueryAsync(
                    sql,
                    new { documentId });

            return data.ToList();
        }
    }
}
