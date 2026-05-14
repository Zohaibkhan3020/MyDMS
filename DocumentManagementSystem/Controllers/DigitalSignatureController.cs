using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    

    [Authorize]

    [ApiController]

    [Route("api/signature")]
    public class DigitalSignatureController
        : ControllerBase
    {
        private readonly
            IDigitalSignatureService
                _service;

        public DigitalSignatureController(
            IDigitalSignatureService service)
        {
            _service =
                service;
        }

        [HttpPost("sign")]
        public async Task<IActionResult>
            Sign(
                SignDocumentDto dto)
        {
            var hash =
                await _service
                    .SignDocumentAsync(dto);

            return Ok(
                new
                {
                    SignatureHash = hash
                });
        }

        [HttpPost("watermark")]
        public async Task<IActionResult>
            Watermark(
                WatermarkDto dto)
        {
            var file =
                await _service
                    .ApplyWatermarkAsync(dto);

            return Ok(
                new
                {
                    File = file
                });
        }
    }
}
