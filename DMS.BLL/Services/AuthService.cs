using BCrypt.Net;
using DMS.BLL.DTOs;
using DMS.BLL.Security;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DMS.BLL.Services
{
    

    public class AuthService
    {
        private readonly IUserRepository  _userRepository;

        private readonly IUserSessionRepository _sessionRepository;

        private readonly JwtTokenGenerator _jwtTokenGenerator;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUserRepository userRepository, IUserSessionRepository sessionRepository,
            JwtTokenGenerator jwtTokenGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var user =  await _userRepository.ValidateUserAsync(dto.Username);

            if (user == null)
            {
                throw new Exception("Invalid Username");
            }
            
            var validPassword = BCrypt.Net.BCrypt.Verify(dto.Password,user.PasswordHash);

            if (!validPassword)
            {
                await _userRepository.IncreaseFailedAttempts(user.UserID);
                throw new Exception("Invalid Password");
            }
            await _userRepository.UpdateLoginSuccess(user.UserID);
            var token = _jwtTokenGenerator.GenerateToken(user);

            await _sessionRepository.DeactivateSessions(user.UserID);

            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var geo = await GetGeoLocationAsync(ip);

            var userAgent = _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString();

            var browser =  GetBrowser(userAgent);

            await _sessionRepository.InsertAsync(
                    new UserSession
                    {
                        UserID = user.UserID,

                        JwtToken = token,

                        IPAddress = ip,
                        City = geo.City,

                        BrowserInfo = browser,

                        ExpiryTime = DateTime.Now.AddHours(1),

                        IsActive = true
                    });

            return new LoginResponseDto
            {
                UserID = user.UserID,

                FullName = user.FullName,

                Username = user.Username,

                Token = token,

                Expiry =
                    DateTime.Now
                        .AddHours(8)
            };
        }
        public async Task<GeoLocationDto> GetGeoLocationAsync(string ip)
        {
            using var client = new HttpClient();

            var url = $"http://ip-api.com/json/{ip}";

            var response = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<GeoLocationDto>(response);
        }
        private string GetBrowser(string userAgent)
        {
            if (userAgent.Contains("Edg"))
                return "Microsoft Edge";

            if (userAgent.Contains("Chrome"))
                return "Google Chrome";

            if (userAgent.Contains("Firefox"))
                return "Mozilla Firefox";

            if (userAgent.Contains("Safari"))
                return "Safari";

            return "Unknown Browser";
        }
    }
}
