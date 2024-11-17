using System.Collections.Generic;

namespace EcommerceBackend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Linked to OrderItem
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Shipped, Completed, Cancelled
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } // Navigation property
        public int ProductId { get; set; }
        public Product Product { get; set; } // Navigation property
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Price at the time of purchase
    }
}