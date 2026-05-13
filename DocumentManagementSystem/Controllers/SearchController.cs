using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController
    : ControllerBase
    {
        private readonly ISearchService
            _service;

        public SearchController(
            ISearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult>Search(int vaultId,string keyword)
        {
            return Ok(await _service.SearchAsync(vaultId,keyword));
        }
    }
}
