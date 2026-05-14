using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(
            Notification model);

        Task AddEmailQueueAsync(
            EmailQueue model);

        Task<List<Notification>>
            GetUserNotificationsAsync(
                int userId);

        Task MarkAsReadAsync(
            int notificationId);

        Task<List<EmailQueue>>
            GetPendingEmailsAsync();

        Task MarkEmailSentAsync(
            int emailQueueId);
    }
}
