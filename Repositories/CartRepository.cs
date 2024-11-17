using EcommerceBackend.Data;
using EcommerceBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceBackend.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cart GetCartByUserId(int userId)
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
            var cart = _context.Carts.Include(c => c.CartItems)
                                      .FirstOrDefault(c => c.UserId == userId);
            if (cart != null && cart.CartItems.Any())
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                _context.SaveChanges();
            }
        }

        public IEnumerable<CartItem> GetCartItemsByIds(List<int> cartItemIds)
{
    return _context.CartItems
        .Include(ci => ci.Product)
        .Where(ci => ci != null && cartItemIds.Contains(ci.Id))
        .ToList();
}

        public void RemoveCartItems(List<int> cartItemIds)
        {
            var cartItems = _context.CartItems.Where(ci => cartItemIds.Contains(ci.Id)).ToList();
            if (cartItems.Any())
            {
                _context.CartItems.RemoveRange(cartItems);
                _context.SaveChanges();
            }
        }
    }
}