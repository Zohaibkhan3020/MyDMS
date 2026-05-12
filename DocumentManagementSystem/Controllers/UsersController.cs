
using DMS.BLL.DTOs;
using DMS.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(
            UserService service)
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
            CreateUserDto dto)
        {
            return Ok(
                await _service.CreateAsync(dto));
        }
    }
}
