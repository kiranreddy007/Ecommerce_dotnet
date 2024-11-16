using System.Collections.Generic;

namespace EcommerceBackend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>();
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
    }
}