using Microsoft.AspNetCore.Mvc;
using EcommerceBackend.Models;
using EcommerceBackend.Services;
using Microsoft.AspNetCore.Authorization;
using EcommerceBackend.Utils;
using EcommerceBackend.DTOs;

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

       [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }




        [HttpGet("{id}")]
        [Authorize] // Only authenticated users can access
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (_userService.GetUserByUsername(user.Username) != null)
            {
                return BadRequest(new { message = "Username already exists" });
            }

            _userService.RegisterUser(user);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var existingUser = _userService.GetUserByUsername(request.Username);
            if (existingUser == null || !PasswordHasher.VerifyPassword(request.Password, existingUser.Password))
            {
                return BadRequest(new { message = "Invalid username or password" });
            }

            var role = existingUser.Role;
            var token = JwtHelper.GenerateJwtToken(existingUser.Username,role,existingUser.Id);

            return Ok(new { token });
        }

        [HttpPut("{id}")]
        [Authorize] // Only authenticated users can update their profile
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound(new { message = "User not found" });
            }

            if (existingUser.Username != User.Identity.Name)
            {
                return Unauthorized(new { message = "You can only update your own profile" });
            }

            user.Id = id; // Ensure the ID is correctly set
            _userService.UpdateUser(user);
            return Ok(new { message = "User updated successfully" });
        }
    }
}