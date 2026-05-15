using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaultsController : ControllerBase
    {
        private readonly IVaultService _service;

        public VaultsController(IVaultService service)
        {
            _service = service;
        }

        [HttpGet("{ServerID}")]
        public async Task<IActionResult> GetAll(int ServerID)
        {
            var result = await _service.GetAllAsync(ServerID);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVaultDto dto)
        {
            int userId = 1;

            int roleId = 1;

            var result = await _service.CreateAsync(dto,userId,roleId);

            return Ok(result);
        }
    }

}
