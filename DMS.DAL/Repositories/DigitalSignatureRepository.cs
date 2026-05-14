using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{
    

    public class DigitalSignatureRepository
        : IDigitalSignatureRepository
    {
        public async Task SaveSignatureAsync(
            string connectionString,
            DocumentSignature model)
        {
            var sql = @"

INSERT INTO DOCUMENT_SIGNATURES
(
    DocumentID,
    SignedBy,
    SignatureHash,
    IsValid
)
VALUES
(
    @DocumentID,
    @SignedBy,
    @SignatureHash,
    @IsValid
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

        public async Task SaveWatermarkAsync(
            string connectionString,
            DocumentWatermark model)
        {
            var sql = @"

INSERT INTO DOCUMENT_WATERMARKS
(
    DocumentID,
    WatermarkText
)
VALUES
(
    @DocumentID,
    @WatermarkText
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
