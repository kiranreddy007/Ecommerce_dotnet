using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "User"; // Default role is "User"

        [Required]
        public string Email { get; set; } // New field for email

        public string PhoneNumber { get; set; } // Optional field for phone number
    }
}