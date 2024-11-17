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

        private readonly IProductRepository _productRepository; // Add this field
        private readonly ApplicationDbContext _context; // Add ApplicationDbContext

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository,IProductRepository productRepository, ApplicationDbContext context)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository; // Inject the product repository
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

        

        public void PlaceOrder(int userId, List<int> cartItemIds)
{
    // Retrieve and validate the cart items
    var cartItems = _cartRepository.GetCartItemsByIds(cartItemIds).ToList();

    if (cartItems == null || !cartItems.Any())
    {
        throw new Exception("No valid cart items found for the order.");
    }

    // Calculate total amount
    var totalAmount = cartItems.Sum(ci => ci.Price);

    // Create the order
    var order = new Order
    {
        UserId = userId,
        TotalAmount = totalAmount,
        OrderItems = cartItems.Select(ci => new OrderItem
        {
            ProductId = ci.ProductId,
            Quantity = ci.Quantity,
            Price = ci.Price
        }).ToList()
    };

    // Use a transaction for atomic operations
    using var transaction = _context.Database.BeginTransaction();
    try
    {
        // Create the order
        _orderRepository.CreateOrder(order);

        // Update stock for each product
        foreach (var cartItem in cartItems)
        {
            var product = _productRepository.GetProductById(cartItem.ProductId);
            if (product == null)
            {
                throw new Exception($"Product with ID {cartItem.ProductId} not found.");
            }

            if (product.Stock < cartItem.Quantity)
            {
                throw new Exception($"Insufficient stock for product {product.Name}.");
            }

            product.Stock -= cartItem.Quantity; // Reduce stock
            _productRepository.UpdateProduct(product); // Update product in database
        }

        // Remove items from the cart
        _cartRepository.RemoveCartItems(cartItemIds);

        // Commit transaction
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