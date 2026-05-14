using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInOutController
    : ControllerBase
    {
        private readonly ICheckInOutService
            _service;

        private readonly IHttpContextAccessor
            _httpContextAccessor;

        public CheckInOutController(
            ICheckInOutService service,

            IHttpContextAccessor
                httpContextAccessor)
        {
            _service =
                service;

            _httpContextAccessor =
                httpContextAccessor;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult>
            CheckOut(
                CheckOutDto dto)
        {
            var userId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier));

            var machine =
                Environment.MachineName;

            var ip =
                _httpContextAccessor
                    .HttpContext?
                    .Connection?
                    .RemoteIpAddress?
                    .ToString();

            await _service
                .CheckOutAsync(
                    dto,
                    userId,
                    machine,
                    ip);

            return Ok(
                "Document checked out");
        }

        [HttpPost("checkin")]
        public async Task<IActionResult>
            CheckIn(
                CheckInDto dto)
        {
            await _service
                .CheckInAsync(
                    dto);

            return Ok(
                "Document checked in");
        }
    }
}
