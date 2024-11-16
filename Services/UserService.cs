using EcommerceBackend.DTOs;
using EcommerceBackend.Models;
using EcommerceBackend.Repositories;
using EcommerceBackend.Utils;

namespace EcommerceBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public (bool Success, string Message) RegisterUser(RegisterRequest request)
        {
            // Log the registration attempt
            Console.WriteLine($"Attempting to register user: {request.Username}");

            // Validate the input
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                Console.WriteLine("Validation failed: Username or password is empty.");
                return (false, "Username and password are required");
            }

            // Check if username already exists
            if (_userRepository.GetUserByUsername(request.Username) != null)
            {
                Console.WriteLine($"Validation failed: Username {request.Username} is already taken.");
                return (false, "Username is already taken");
            }

            // Hash the password
            var hashedPassword = PasswordHasher.HashPassword(request.Password);

            // Create and save the user
            var user = new User
            {
                Username = request.Username,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            _userRepository.AddUser(user);
            Console.WriteLine($"User {request.Username} registered successfully.");

            return (true, "User registered successfully");
        }

        public string LoginUser(LoginRequest request)
        {
            // Log the login attempt
            Console.WriteLine($"Attempting login for user: {request.Username}");

            // Fetch the user from the repository
            var user = _userRepository.GetUserByUsername(request.Username);
            if (user == null)
            {
                Console.WriteLine("Login failed: User not found.");
                return null;
            }

            // Validate the password
            if (!PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                Console.WriteLine("Login failed: Invalid password.");
                return null;
            }

            // Generate a JWT token for the user
            Console.WriteLine("Login successful: Generating JWT token.");
            var token = JwtHelper.GenerateJwtToken(user.Username, user.Role);

            Console.WriteLine("JWT token generated successfully.");
            return token;
        }
    }
}