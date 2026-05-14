using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Services
{
    public class NotificationService
    : INotificationService
    {
        private readonly INotificationRepository
            _repository;

        public NotificationService(
            INotificationRepository repository)
        {
            _repository =
                repository;
        }

        public async Task AddNotificationAsync(
            CreateNotificationDto dto)
        {
            await _repository
                .AddNotificationAsync(
                    new Notification
                    {
                        UserID =
                            dto.UserID,

                        Title =
                            dto.Title,

                        Message =
                            dto.Message,

                        NotificationType =
                            dto.NotificationType,

                        ReferenceID =
                            dto.ReferenceID,

                        IsRead = false
                    });
        }

        public async Task QueueEmailAsync(
            EmailDto dto)
        {
            await _repository
                .AddEmailQueueAsync(
                    new EmailQueue
                    {
                        ToEmail =
                            dto.ToEmail,

                        Subject =
                            dto.Subject,

                        Body =
                            dto.Body,

                        IsSent = false
                    });
        }

        public async Task<List<Notification>>
            GetUserNotificationsAsync(
                int userId)
        {
            return await _repository
                .GetUserNotificationsAsync(
                    userId);
        }

        public async Task MarkAsReadAsync(
            int notificationId)
        {
            await _repository
                .MarkAsReadAsync(
                    notificationId);
        }
    }
}
