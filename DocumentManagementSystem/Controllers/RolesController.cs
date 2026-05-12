using DMS.BLL.DTOs;
using DMS.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleService _service;

        public RolesController(
            RoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateRoleDto dto)
        {
            return Ok(
                await _service.CreateAsync(dto));
        }
    }
}
