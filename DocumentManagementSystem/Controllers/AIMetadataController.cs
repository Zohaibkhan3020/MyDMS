using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    

    [Authorize]

    [ApiController]

    [Route("api/ai")]
    public class AIMetadataController
        : ControllerBase
    {
        private readonly
            IAIMetadataService
                _service;

        public AIMetadataController(
            IAIMetadataService service)
        {
            _service =
                service;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult>
            Analyze(
                AnalyzeDocumentDto dto)
        {
            return Ok(
                await _service
                    .AnalyzeAsync(dto));
        }
    }
}
