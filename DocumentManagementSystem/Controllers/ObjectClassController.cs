using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectClassController
    : ControllerBase
    {
        private readonly IObjectClassService
            _service;

        public ObjectClassController(
            IObjectClassService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult>
            Create(
                CreateObjectClassDto dto)
        {
            var userId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier));

            return Ok(
                await _service
                    .CreateAsync(
                        dto,
                        userId));
        }

        [HttpPut]
        public async Task<IActionResult>
            Update(
                UpdateObjectClassDto dto)
        {
            await _service
                .UpdateAsync(dto);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult>
            Delete(
                int vaultId,
                int classId)
        {
            await _service
                .DeleteAsync(
                    vaultId,
                    classId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult>
            GetAll(
                int vaultId)
        {
            return Ok(
                await _service
                    .GetAllAsync(
                        vaultId));
        }

        [HttpGet("by-object-type")]
        public async Task<IActionResult>
            GetByObjectType(
                int vaultId,
                int objectTypeId)
        {
            return Ok(
                await _service
                    .GetByObjectTypeAsync(
                        vaultId,
                        objectTypeId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetById(
                int vaultId,
                int id)
        {
            return Ok(
                await _service
                    .GetByIdAsync(
                        vaultId,
                        id));
        }
    }
}
