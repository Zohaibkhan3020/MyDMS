using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController
    : ControllerBase
    {
        private readonly INotificationService
            _service;

        public NotificationController(
            INotificationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult>
            AddNotification(
                CreateNotificationDto dto)
        {
            await _service
                .AddNotificationAsync(
                    dto);

            return Ok();
        }

        [HttpPost("email")]
        public async Task<IActionResult>
            QueueEmail(
                EmailDto dto)
        {
            await _service
                .QueueEmailAsync(
                    dto);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult>
            GetMyNotifications()
        {
            var userId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier));

            return Ok(
                await _service
                    .GetUserNotificationsAsync(
                        userId));
        }

        [HttpPost("read/{id}")]
        public async Task<IActionResult>
            MarkAsRead(
                int id)
        {
            await _service
                .MarkAsReadAsync(id);

            return Ok();
        }
    }
}
