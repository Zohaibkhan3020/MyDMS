using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace DMS.DAL.Repositories
{
    
    public class CheckInOutRepository
        : ICheckInOutRepository
    {
        public async Task<bool>
            IsCheckedOutAsync(
                string connectionString,
                int documentId)
        {
            var sql = @"

SELECT COUNT(1)
FROM DOCUMENTS
WHERE DocumentID = @documentId
AND IsCheckedOut = 1

";

            using var connection =
                new SqlConnection(
                    connectionString);

            var count =
                await connection
                    .ExecuteScalarAsync<int>(
                        sql,
                        new { documentId });

            return count > 0;
        }

        public async Task CheckOutAsync(
            string connectionString,
            DocumentCheckout model)
        {
            var sql = @"

INSERT INTO DOCUMENT_CHECKOUTS
(
    DocumentID,
    CheckedOutBy,
    MachineName,
    IPAddress,
    Comments
)
VALUES
(
    @DocumentID,
    @CheckedOutBy,
    @MachineName,
    @IPAddress,
    @Comments
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

        public async Task CreateLockAsync(
            string connectionString,
            DocumentLock model)
        {
            var sql = @"

INSERT INTO DOCUMENT_LOCKS
(
    DocumentID,
    LockedBy,
    LockType,
    IsLocked
)
VALUES
(
    @DocumentID,
    @LockedBy,
    @LockType,
    1
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

        public async Task UpdateDocumentCheckOutAsync(
            string connectionString,
            int documentId,
            int userId)
        {
            var sql = @"

UPDATE DOCUMENTS
SET
    IsCheckedOut = 1,
    CheckedOutBy = @userId,
    CheckedOutOn = GETDATE()
WHERE DocumentID = @documentId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection
                .ExecuteAsync(
                    sql,
                    new
                    {
                        documentId,
                        userId
                    });
        }

        public async Task CheckInAsync(
            string connectionString,
            int documentId)
        {
            var sql = @"

UPDATE DOCUMENTS
SET
    IsCheckedOut = 0,
    CheckedOutBy = NULL,
    CheckedOutOn = NULL
WHERE DocumentID = @documentId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection
                .ExecuteAsync(
                    sql,
                    new { documentId });
        }

        public async Task RemoveLockAsync(
            string connectionString,
            int documentId)
        {
            var sql = @"

UPDATE DOCUMENT_LOCKS
SET
    IsLocked = 0
WHERE DocumentID = @documentId

";

            using var connection =
                new SqlConnection(
                    connectionString);

            await connection
                .ExecuteAsync(
                    sql,
                    new { documentId });
        }
    }
}
