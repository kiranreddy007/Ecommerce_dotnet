using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}