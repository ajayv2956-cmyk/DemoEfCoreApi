using DemoEfCoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoEfCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            if (!_authService.ValidateUser(model.Username, model.Password))
                return Unauthorized();

            var token = _authService.GenerateJwtToken(model.Username);
            return Ok(new { token });
        }

        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}
