using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    

    [Authorize]

    [ApiController]

    [Route("api/email")]
    public class EmailController
        : ControllerBase
    {
        private readonly
            IEmailService
                _service;

        public EmailController(
            IEmailService service)
        {
            _service =
                service;
        }

        [HttpPost("send")]
        public async Task<IActionResult>
            Send(
                SendEmailDto dto)
        {
            await _service
                .SendAsync(dto);

            return Ok(
                "Email Sent");
        }
    }
}
