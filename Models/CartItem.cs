using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; } // Foreign key linking to the Cart

        public Cart? Cart { get; set; } // Navigation property

        [Required]
        public int ProductId { get; set; } // Foreign key linking to the Product

        public Product? Product { get; set; } // Navigation property

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; } // Price at the time of adding to the cart
    }
}