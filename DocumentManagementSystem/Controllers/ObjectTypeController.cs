using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectTypeController
    : ControllerBase
    {
        private readonly IObjectTypeService
            _service;

        public ObjectTypeController(
            IObjectTypeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult>Create(CreateObjectTypeDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _service.CreateAsync(dto,userId));
        }

        [HttpPut]
        public async Task<IActionResult>
            Update(
                UpdateObjectTypeDto dto)
        {
            await _service.UpdateAsync(dto);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult>
            Delete(
                int vaultId,
                int objectTypeId)
        {
            await _service.DeleteAsync(
                vaultId,
                objectTypeId);

            return Ok();
        }

        [HttpGet("{vaultId}")]
        public async Task<IActionResult>GetAll(int vaultId)
        {
            return Ok(await _service.GetAllAsync(vaultId));
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult>GetById(int vaultId,int id)
        //{
        //    return Ok(await _service.GetByIdAsync(vaultId,id));
        //}
    }
}
