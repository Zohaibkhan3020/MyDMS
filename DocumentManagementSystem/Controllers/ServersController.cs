using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly IServerService _service;

        public ServersController(IServerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateServerDto dto)
        {
            int userId = 1;
            int roleId = 1;

            var result = await _service.CreateAsync(dto, userId, roleId);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Server model)
        {
            int roleId = 1;

            var result = await _service.UpdateAsync(model, roleId);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int roleId = 1;

            var result = await _service.DeleteAsync(id, roleId);

            return Ok(result);
        }
    }
}
