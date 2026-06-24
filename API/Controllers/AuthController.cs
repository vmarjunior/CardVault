using Microsoft.AspNetCore.Mvc;
using CardVault.Application.DTOs.Auth;
using CardVault.Application.Interfaces;

namespace CardVault.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] AuthRequestDTO loginRequest)
        {
            var token = await _authService.LoginAsync(loginRequest);

            if (token == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new { Token = token });
        }
    }
}
