using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } // Foreign key linking to the User

        public User? User { get; set; } // Navigation property

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); // List of items in the cart
    }
}