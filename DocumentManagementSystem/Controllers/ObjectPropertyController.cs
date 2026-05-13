using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("api/object-properties")]
    public class ObjectPropertyController
    : ControllerBase
    {
        private readonly IObjectPropertyService
            _service;

        public ObjectPropertyController(IObjectPropertyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateObjectPropertyDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateObjectPropertyDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int vaultId, int propertyId)
        {
            await _service.DeleteAsync(vaultId,propertyId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int vaultId)
        {
            return Ok(await _service.GetAllAsync(vaultId));
        }

        [HttpGet("by-object-type")]
        public async Task<IActionResult>GetByObjectType(int vaultId,int objectTypeId)
        {
            return Ok(await _service.GetByObjectTypeAsync(vaultId,objectTypeId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int vaultId,int id)
        {
            return Ok( await _service.GetByIdAsync(vaultId,id));
        }
    }
}
