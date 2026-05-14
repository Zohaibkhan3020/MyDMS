using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly IFolderService _service;

        public FolderController(IFolderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult>CreateFolder(CreateFolderDto dto)
        {
            var userId =int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _service.CreateFolderAsync(dto,userId));
        }

        [HttpGet("tree/{vaultId}")]
        public async Task<IActionResult>GetTree(int vaultId)
        {
            return Ok(await _service.GetTreeAsync(vaultId));
        }

        [HttpPost("move")]
        public async Task<IActionResult>MoveFolder(MoveFolderDto dto)
        {
            await _service.MoveFolderAsync(dto);
            return Ok();
        }
    }
}
