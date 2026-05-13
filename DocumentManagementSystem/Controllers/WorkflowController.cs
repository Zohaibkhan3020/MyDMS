using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController
        : ControllerBase
    {
        private readonly IWorkflowService
            _service;

        public WorkflowController(
            IWorkflowService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult>
            CreateWorkflow(
                CreateWorkflowDto dto)
        {
            return Ok(
                await _service
                    .CreateWorkflowAsync(
                        dto));
        }

        [HttpPost("states")]
        public async Task<IActionResult>
            CreateState(
                CreateStateDto dto)
        {
            return Ok(
                await _service
                    .CreateStateAsync(
                        dto));
        }

        [HttpPost("transitions")]
        public async Task<IActionResult>
            CreateTransition(
                CreateTransitionDto dto)
        {
            return Ok(
                await _service
                    .CreateTransitionAsync(
                        dto));
        }

        [HttpPost("start")]
        public async Task<IActionResult>
            StartWorkflow(
                StartWorkflowDto dto)
        {
            var userId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier));

            await _service
                .StartWorkflowAsync(
                    dto,
                    userId);

            return Ok();
        }

        [HttpPost("action")]
        public async Task<IActionResult>
            MoveNext(
                WorkflowActionDto dto)
        {
            var userId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier));

            await _service
                .MoveNextAsync(
                    dto,
                    userId);

            return Ok();
        }
    }
}
