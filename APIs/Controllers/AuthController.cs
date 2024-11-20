using APIs.Model;
using APIs.Repositories;
using APIs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//  alias  LoginRequest
using CustomLoginRequest = APIs.Model.LoginRequest;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        //private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CustomLoginRequest request)
        {
            var user = await _userRepository.AuthenticateUserAsync(request.UserName, request.Password);
            if (user == null) return Unauthorized();

            var accessToken = _jwtService.GenerateJwtToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            await _userRepository.SaveRefreshTokenAsync(user.Id, refreshToken);
            return Ok(new { accessToken, refreshToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null || !await _userRepository.ValidateRefreshTokenAsync(userId, tokenRequest.RefreshToken))
            {
                return Unauthorized();
            }

            var user = await _userRepository.GetUserByIdAsync(int.Parse(userId));
            var newAccessToken = _jwtService.GenerateJwtToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            await _userRepository.SaveRefreshTokenAsync(user.Id, newRefreshToken); 

            return Ok(new { accessToken = newAccessToken, refreshToken = newRefreshToken });
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId != null)
            {
                await _userRepository.RemoveRefreshTokenAsync(userId);
            }

            return Ok(new { message = "Logout successful" });
        }

    }
}
