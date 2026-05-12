using DMS.BLL.DTOs;
using DMS.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController
        : ControllerBase
    {
        private readonly AuthService
            _service;

        public AuthController(
            AuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult>
            Login(LoginDto dto)
        {
            return Ok(
                await _service
                    .LoginAsync(dto));
        }
    }
}
