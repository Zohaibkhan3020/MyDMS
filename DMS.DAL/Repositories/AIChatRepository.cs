using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;

namespace DMS.DAL.Repositories
{
    

    public class AIChatRepository
        : IAIChatRepository
    {
        private readonly
            ISqlConnectionFactory
                _connectionFactory;

        public AIChatRepository(
            ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory =
                connectionFactory;
        }

        public async Task<int>
            CreateSessionAsync(
                AIChatSession model)
        {
            var sql = @"

INSERT INTO AI_CHAT_SESSIONS
(
    UserID
)
VALUES
(
    @UserID
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

        public async Task SaveMessageAsync(
            AIChatMessage model)
        {
            var sql = @"

INSERT INTO AI_CHAT_MESSAGES
(
    SessionID,
    SenderType,
    MessageText
)
VALUES
(
    @SessionID,
    @SenderType,
    @MessageText
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

        public async Task<List<AIChatMessage>>
            GetMessagesAsync(
                int sessionId)
        {
            var sql = @"

SELECT *
FROM AI_CHAT_MESSAGES
WHERE SessionID = @sessionId
ORDER BY MessageID

";

            using var connection =
                _connectionFactory
                    .CreateConnection();

            var result =
                await connection
                    .QueryAsync<AIChatMessage>(
                        sql,
                        new
                        {
                            sessionId
                        });

            return result.ToList();
        }

        public async Task SaveMemoryAsync(
            AIMemory model)
        {
            var sql = @"

INSERT INTO AI_MEMORY
(
    UserID,
    MemoryKey,
    MemoryValue
)
VALUES
(
    @UserID,
    @MemoryKey,
    @MemoryValue
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

        public async Task<List<AIMemory>>
            GetMemoryAsync(
                int userId)
        {
            var sql = @"

SELECT *
FROM AI_MEMORY
WHERE UserID = @userId

";

            using var connection =
                _connectionFactory
                    .CreateConnection();

            var result =
                await connection
                    .QueryAsync<AIMemory>(
                        sql,
                        new
                        {
                            userId
                        });

            return result.ToList();
        }
    }
}
