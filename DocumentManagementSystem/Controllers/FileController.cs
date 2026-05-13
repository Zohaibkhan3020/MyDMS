using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController
    : ControllerBase
    {
        private readonly IFileService
            _service;

        public FileController(
            IFileService service)
        {
            _service = service;
        }

        [HttpPost("upload")]

        [RequestSizeLimit(500_000_000)]
        public async Task<IActionResult>
            Upload(
                [FromForm] UploadFileDto dto)
        {
            var userId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier));

            return Ok(
                await _service
                    .UploadAsync(
                        dto,
                        userId));
        }
    }
}
