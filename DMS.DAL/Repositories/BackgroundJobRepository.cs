using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;

namespace DMS.DAL.Repositories
{

    public class BackgroundJobRepository
        : IBackgroundJobRepository
    {
        private readonly
            ISqlConnectionFactory
                _connectionFactory;

        public BackgroundJobRepository(
            ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory =
                connectionFactory;
        }

        public async Task<int>
            CreateAsync(
                BackgroundJob model)
        {
            var sql = @"

INSERT INTO BACKGROUND_JOBS
(
    JobType,
    Payload,
    Status
)
VALUES
(
    @JobType,
    @Payload,
    'Pending'
)

SELECT CAST(SCOPE_IDENTITY() AS INT)

";

            using var connection =
                _connectionFactory
                    .CreateConnection();

            return await connection
                .ExecuteScalarAsync<int>(
                    sql,
                    model);
        }

        public async Task<List<BackgroundJob>>
            GetPendingJobsAsync()
        {
            var sql = @"

SELECT *
FROM BACKGROUND_JOBS
WHERE Status = 'Pending'

";

            using var connection =
                _connectionFactory
                    .CreateConnection();

            var result =
                await connection
                    .QueryAsync<BackgroundJob>(
                        sql);

            return result.ToList();
        }

        public async Task UpdateStatusAsync(
            int jobId,
            string status,
            string error = null)
        {
            var sql = @"

UPDATE BACKGROUND_JOBS
SET
    Status = @status,
    ProcessedOn = GETDATE(),
    ErrorMessage = @error
WHERE JobID = @jobId

";

            using var connection =
                _connectionFactory
                    .CreateConnection();

            await connection
                .ExecuteAsync(
                    sql,
                    new
                    {
                        jobId,
                        status,
                        error
                    });
        }
    }
}
