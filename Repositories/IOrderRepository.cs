using System.Collections.Generic;
using EcommerceBackend.Models;

namespace EcommerceBackend.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetOrdersByUserId(int userId);
        Order GetOrderById(int id);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);

        
    }
}