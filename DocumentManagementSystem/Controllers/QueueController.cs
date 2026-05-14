using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    

    [Authorize]

    [ApiController]

    [Route("api/queue")]
    public class QueueController
        : ControllerBase
    {
        private readonly
            IQueueService
                _service;

        public QueueController(
            IQueueService service)
        {
            _service =
                service;
        }

        [HttpPost("enqueue")]
        public async Task<IActionResult>
            Enqueue(
                QueueJobDto dto)
        {
            var id =
                await _service
                    .EnqueueAsync(dto);

            return Ok(
                new
                {
                    JobID = id
                });
        }
    }
}
