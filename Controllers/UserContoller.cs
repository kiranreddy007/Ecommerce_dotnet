using Microsoft.AspNetCore.Mvc;
using EcommerceBackend.DTOs;
using EcommerceBackend.Services;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var result = _userService.RegisterUser(request);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = _userService.LoginUser(request);
            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new { token });
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new { message = "API is working" });
        }
    }
}