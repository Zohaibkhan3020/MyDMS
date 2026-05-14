using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
   

    //[Authorize]

    [ApiController]

    [Route("api/ai-chat")]
    public class AIChatController
        : ControllerBase
    {
        private readonly
            IAIChatService
                _service;

        public AIChatController(
            IAIChatService service)
        {
            _service =
                service;
        }

        [HttpPost("ask")]
        public async Task<IActionResult>Ask(AIChatRequestDto dto)
        {
            return Ok(await _service.AskAsync(dto));
        }
    }
}
