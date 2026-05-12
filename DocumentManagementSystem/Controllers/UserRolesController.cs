using DMS.BLL.DTOs;
using DMS.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly UserRoleService _service;

        public UserRolesController(
            UserRoleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateUserRoleDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }
    }
}
