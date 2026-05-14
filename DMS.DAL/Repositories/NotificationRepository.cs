using Dapper;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DMS.DAL.Repositories
{
    

    public class NotificationRepository
        : INotificationRepository
    {
        private readonly IConfiguration
            _configuration;

        public NotificationRepository(
            IConfiguration configuration)
        {
            _configuration =
                configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(
                _configuration
                    .GetConnectionString(
                        "MasterConnection"));
        }

        public async Task AddNotificationAsync(
            Notification model)
        {
            var sql = @"

INSERT INTO NOTIFICATIONS
(
    UserID,
    Title,
    Message,
    NotificationType,
    ReferenceID
)
VALUES
(
    @UserID,
    @Title,
    @Message,
    @NotificationType,
    @ReferenceID
)

";

            using var connection =
                GetConnection();

            await connection
                .ExecuteAsync(
                    sql,
                    model);
        }

        public async Task AddEmailQueueAsync(
            EmailQueue model)
        {
            var sql = @"

INSERT INTO EMAIL_QUEUE
(
    ToEmail,
    Subject,
    Body
)
VALUES
(
    @ToEmail,
    @Subject,
    @Body
)

";

            using var connection =
                GetConnection();

            await connection
                .ExecuteAsync(
                    sql,
                    model);
        }

        public async Task<List<Notification>>
            GetUserNotificationsAsync(
                int userId)
        {
            var sql = @"

SELECT *
FROM NOTIFICATIONS
WHERE UserID = @userId
ORDER BY NotificationID DESC

";

            using var connection =
                GetConnection();

            var result =
                await connection
                    .QueryAsync<Notification>(
                        sql,
                        new { userId });

            return result.ToList();
        }

        public async Task MarkAsReadAsync(
            int notificationId)
        {
            var sql = @"

UPDATE NOTIFICATIONS
SET IsRead = 1
WHERE NotificationID = @notificationId

";

            using var connection =
                GetConnection();

            await connection
                .ExecuteAsync(
                    sql,
                    new { notificationId });
        }

        public async Task<List<EmailQueue>>
            GetPendingEmailsAsync()
        {
            var sql = @"

SELECT *
FROM EMAIL_QUEUE
WHERE IsSent = 0

";

            using var connection =
                GetConnection();

            var result =
                await connection
                    .QueryAsync<EmailQueue>(
                        sql);

            return result.ToList();
        }

        public async Task MarkEmailSentAsync(
            int emailQueueId)
        {
            var sql = @"

UPDATE EMAIL_QUEUE
SET
    IsSent = 1,
    SentOn = GETDATE()
WHERE EmailQueueID = @emailQueueId

";

            using var connection =
                GetConnection();

            await connection
                .ExecuteAsync(
                    sql,
                    new { emailQueueId });
        }
    }
}
