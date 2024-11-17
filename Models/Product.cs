using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }  // Added for filtering

        public int Stock { get; set; }

        public decimal? Discount { get; set; } // Added for filtering by discount

         public string? ImagePath { get; set; } 
    }
}