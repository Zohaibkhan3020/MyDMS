using DMS.BLL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FullTextSearchController
    : ControllerBase
    {
        private readonly
            IFullTextSearchService
                _service;

        public FullTextSearchController(
            IFullTextSearchService service)
        {
            _service =
                service;
        }

        [HttpPost("build-index")]
        public async Task<IActionResult>
            BuildIndex(
                int vaultId,
                int documentId)
        {
            await _service
                .BuildIndexAsync(
                    vaultId,
                    documentId);

            return Ok(
                "Index Created");
        }

        [HttpPost("search")]
        public async Task<IActionResult>
            Search(
                SearchRequestDto dto)
        {
            return Ok(
                await _service
                    .SearchAsync(dto));
        }
    }
}
