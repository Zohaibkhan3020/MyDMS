using DMS.BLL.DTOs;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface INotificationService
    {
        Task AddNotificationAsync(
            CreateNotificationDto dto);

        Task QueueEmailAsync(
            EmailDto dto);

        Task<List<Notification>>
            GetUserNotificationsAsync(
                int userId);

        Task MarkAsReadAsync(
            int notificationId);
    }
}
