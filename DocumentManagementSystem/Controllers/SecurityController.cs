using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController
    : ControllerBase
    {
        private readonly ISecurityService
            _service;

        public SecurityController(
            ISecurityService service)
        {
            _service = service;
        }

        [HttpPost("object")]
        public async Task<IActionResult>
            AddObjectPermission(
                ObjectPermissionDto dto)
        {
            await _service
                .AddObjectPermissionAsync(
                    dto);

            return Ok();
        }

        [HttpPost("document")]
        public async Task<IActionResult>
            AddDocumentPermission(
                DocumentPermissionDto dto)
        {
            await _service
                .AddDocumentPermissionAsync(
                    dto);

            return Ok();
        }

        [HttpPost("property")]
        public async Task<IActionResult>
            AddPropertyPermission(
                PropertyPermissionDto dto)
        {
            await _service
                .AddPropertyPermissionAsync(
                    dto);

            return Ok();
        }

        [HttpPost("workflow")]
        public async Task<IActionResult>
            AddWorkflowPermission(
                WorkflowPermissionDto dto)
        {
            await _service
                .AddWorkflowPermissionAsync(
                    dto);

            return Ok();
        }
    }
}
