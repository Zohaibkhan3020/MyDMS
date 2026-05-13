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
        private readonly IFileService _service;

        public FileController(IFileService service)
        {
            _service = service;
        }

        [HttpPost("upload")]

        [RequestSizeLimit(500_000_000)]
        public async Task<IActionResult> Upload([FromForm] UploadFileDto dto)
        {
            var userId =  int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok( await _service.UploadAsync(dto,4));
        }

        [HttpGet("preview")]
        public async Task<IActionResult>
    Preview(
        int vaultId,
        int fileId)
        {
            var file =
                await _service
                    .GetFileAsync(
                        vaultId,
                        fileId);

            if (file == null)
            {
                return NotFound();
            }

            var stream =
                new FileStream(
                    file.FilePath,
                    FileMode.Open,
                    FileAccess.Read);

            return File(
                stream,
                file.ContentType,
                enableRangeProcessing: true);
        }

        [HttpGet("download")]
        public async Task<IActionResult>
            Download(
                int vaultId,
                int fileId)
        {
            var file =
                await _service
                    .GetFileAsync(
                        vaultId,
                        fileId);

            if (file == null)
            {
                return NotFound();
            }

            var bytes =
                await System.IO.File
                    .ReadAllBytesAsync(
                        file.FilePath);

            return File(
                bytes,
                file.ContentType,
                file.OriginalFileName);
        }

        [HttpGet("by-document")]
        public async Task<IActionResult>
            GetByDocument(
                int vaultId,
                int documentId)
        {
            return Ok(
                await _service
                    .GetByDocumentAsync(
                        vaultId,
                        documentId));
        }
    }
}
