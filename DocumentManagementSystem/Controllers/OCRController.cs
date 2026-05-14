using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OCRController
    : ControllerBase
    {
        private readonly IOCRService
            _service;

        public OCRController(
            IOCRService service)
        {
            _service =
                service;
        }

        [HttpPost("extract")]
        public async Task<IActionResult>
            Extract(
                int vaultId,

                int documentId,

                string filePath)
        {
            var userId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier));

            return Ok(
                await _service
                    .ExtractAsync(
                        vaultId,
                        documentId,
                        filePath,
                        userId));
        }

        [HttpGet("{vaultId}/{documentId}")]
        public async Task<IActionResult>
            GetOCR(
                int vaultId,

                int documentId)
        {
            return Ok(
                await _service
                    .GetOCRTextAsync(
                        vaultId,
                        documentId));
        }
    }
}
