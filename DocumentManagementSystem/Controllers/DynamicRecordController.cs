using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicRecordController
    : ControllerBase
    {
        private readonly IDynamicRecordService
            _service;

        public DynamicRecordController(
            IDynamicRecordService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult>
            Save(
                SaveDynamicRecordDto dto)
        {
            var userId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier));

            return Ok(
                await _service
                    .SaveAsync(
                        dto,
                        userId));
        }
        [HttpGet]
        public async Task<IActionResult>
    GetRecords(
        int vaultId,
        int objectTypeId)
        {
            return Ok(
                await _service
                    .GetRecordsAsync(
                        vaultId,
                        objectTypeId));
        }
    }
}
