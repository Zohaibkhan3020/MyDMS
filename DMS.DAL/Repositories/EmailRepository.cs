using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;

namespace DMS.DAL.Repositories
{
    

    public class EmailRepository
        : IEmailRepository
    {
        private readonly
            ISqlConnectionFactory
                _connectionFactory;

        public EmailRepository(
            ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory =
                connectionFactory;
        }

        public async Task SaveLogAsync(
            EmailLog model)
        {
            var sql = @"

INSERT INTO EMAIL_LOGS
(
    ToEmail,
    Subject,
    BodyHTML,
    SentOn,
    IsSuccess,
    ErrorMessage
)
VALUES
(
    @ToEmail,
    @Subject,
    @BodyHTML,
    @SentOn,
    @IsSuccess,
    @ErrorMessage
)

";

            using var connection =
                _connectionFactory
                    .CreateConnection();

            await connection
                .ExecuteAsync(
                    sql,
                    model);
        }
    }
}
