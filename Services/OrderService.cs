using System.Linq;
using System.Collections.Generic;
using EcommerceBackend.Models;
using EcommerceBackend.Repositories;
using EcommerceBackend.Data; // Add this namespace
using Microsoft.EntityFrameworkCore.Storage; // For transactions

namespace EcommerceBackend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ApplicationDbContext _context; // Add ApplicationDbContext

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, ApplicationDbContext context)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _context = context; // Inject the database context
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _orderRepository.GetOrdersByUserId(userId);
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetOrderById(id);
        }

        // public void PlaceOrder(int userId, List<int> cartItemIds)
        // {
        //     // Retrieve and validate the user's cart items
        //     var cartItems = _cartRepository.GetCartItemsByIds(cartItemIds)
        //         .Where(ci => ci.Cart.UserId == userId)
        //         .ToList();

        //     if (cartItems == null || !cartItems.Any())
        //     {
        //         throw new Exception("No valid cart items found for the order or the items do not belong to the user.");
        //     }

        //     // Calculate the total amount for the order
        //     var totalAmount = cartItems.Sum(ci => ci.Price);

        //     // Create the order
        //     var order = new Order
        //     {
        //         UserId = userId,
        //         TotalAmount = totalAmount,
        //         OrderItems = cartItems.Select(ci => new OrderItem
        //         {
        //             ProductId = ci.ProductId,
        //             Quantity = ci.Quantity,
        //             Price = ci.Price
        //         }).ToList()
        //     };

        //     // Use a transaction to ensure atomicity
        //     using var transaction = _context.Database.BeginTransaction(); // Use ApplicationDbContext
        //     try
        //     {
        //         _orderRepository.CreateOrder(order);
        //         _cartRepository.RemoveCartItems(cartItemIds);
        //         transaction.Commit();
        //     }
        //     catch
        //     {
        //         transaction.Rollback();
        //         throw;
        //     }
        // }

        public void PlaceOrder(int userId, List<int> cartItemIds)
{
    // Retrieve and validate the cart items
    var cartItems = _cartRepository.GetCartItemsByIds(cartItemIds)
        .ToList();

    if (cartItems == null || !cartItems.Any())
    {
        throw new Exception("No valid cart items found for the order.");
    }

    // Check for null items or invalid data
    var validItems = cartItems
        .Where(ci => ci != null && cartItemIds.Contains(ci.Id))
        .ToList();

    if (!validItems.Any())
    {
        throw new Exception("No valid cart items available for processing.");
    }

    // Calculate total amount
    var totalAmount = validItems.Sum(ci => ci.Price);

    // Create the order
    var order = new Order
    {
        UserId = userId,
        TotalAmount = totalAmount,
        OrderItems = validItems.Select(ci => new OrderItem
        {
            ProductId = ci.ProductId,
            Quantity = ci.Quantity,
            Price = ci.Price
        }).ToList()
    };

    // Save order and update cart
    using var transaction = _context.Database.BeginTransaction();
    try
    {
        _orderRepository.CreateOrder(order);
        _cartRepository.RemoveCartItems(cartItemIds);
        transaction.Commit();
    }
    catch
    {
        transaction.Rollback();
        throw;
    }
}

        public void UpdateOrderStatus(int orderId, string status)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
                throw new Exception("Order not found.");

            order.Status = status;
            _orderRepository.UpdateOrder(order);
        }
    }
}