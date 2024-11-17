using EcommerceBackend.Data;
using EcommerceBackend.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cart? GetCartByUserId(int userId)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserId == userId);
        }

        public void CreateCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void AddToCart(CartItem cartItem)
{
    // Validate Cart existence
    var cart = _context.Carts.FirstOrDefault(c => c.Id == cartItem.CartId);
    if (cart == null)
        throw new Exception($"Cart with ID {cartItem.CartId} does not exist.");

    // Validate Product existence
    var product = _context.Products.FirstOrDefault(p => p.Id == cartItem.ProductId);
    if (product == null)
        throw new Exception($"Product with ID {cartItem.ProductId} does not exist.");

    _context.CartItems.Add(cartItem);
    _context.SaveChanges();
}

        public void UpdateCartItem(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            _context.SaveChanges();
        }

        public void RemoveFromCart(int cartItemId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        public void ClearCart(int userId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                _context.SaveChanges();
            }
        }
    }
}