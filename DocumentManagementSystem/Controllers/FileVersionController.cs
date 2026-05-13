using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileVersionController
    : ControllerBase
    {
        private readonly IFileVersionService
            _service;

        public FileVersionController(
            IFileVersionService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult>
            UploadVersion(
                [FromForm] UploadVersionDto dto)
        {
            var userId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier));

            await _service
                .UploadVersionAsync(
                    dto,
                    userId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult>
            GetVersions(
                int vaultId,
                int fileId)
        {
            return Ok(
                await _service
                    .GetVersionsAsync(
                        vaultId,
                        fileId));
        }
    }
}
