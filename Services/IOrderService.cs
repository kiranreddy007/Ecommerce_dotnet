using System.Collections.Generic;
using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetOrdersByUserId(int userId);
        Order GetOrderById(int id);
        
        void PlaceOrder(int userId, List<int> cartItemIds,string ShippingFirstName,string ShippingLastName,string ShippingAddress,string ShippingCity,string ShippingPostalCode);

        void UpdateOrderStatus(int orderId, string status);
    }
}