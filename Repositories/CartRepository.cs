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
            var carts = _context.Carts;
            if (carts == null)
            {
                throw new InvalidOperationException("Carts collection is null.");
            }

            var cart = carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                 cart = new Cart
        {
            UserId = userId,
            CartItems = new List<CartItem>()
        };
        _context.Carts.Add(cart);
        _context.SaveChanges();
            }

            return cart;
        }

        public void CreateCart(Cart cart)
        {
            if (_context.Carts == null)
            {
                throw new InvalidOperationException("Carts collection is null.");
            }

            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void AddToCart(CartItem cartItem)
        {
            if (_context.CartItems == null)
            {
                throw new InvalidOperationException("CartItems collection is null.");
            }

            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            if (_context.CartItems == null)
            {
                throw new InvalidOperationException("CartItems collection is null.");
            }

            _context.CartItems.Update(cartItem);
            _context.SaveChanges();
        }

        public void RemoveFromCart(int cartItemId)
        {
            if (_context.CartItems == null)
            {
                throw new InvalidOperationException("CartItems collection is null.");
            }

            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        public void ClearCart(int userId)
        {
            var carts = _context.Carts;
            if (carts == null)
            {
                throw new InvalidOperationException("Carts collection is null.");
            }

            var cart = carts.Include(c => c.CartItems)
                            .FirstOrDefault(c => c.UserId == userId);
            if (cart != null && cart.CartItems.Any())
            {
                if (cart.CartItems != null)
                {
                    if (cart.CartItems != null)
                    {
                        _context.CartItems.RemoveRange(cart.CartItems);
                    }
                }
                _context.SaveChanges();
            }
        }

        public IEnumerable<CartItem> GetCartItemsByIds(List<int> cartItemIds)
{
    if (_context.CartItems == null)
    {
        throw new InvalidOperationException("CartItems collection is null.");
    }

    return _context.CartItems
        .Include(ci => ci.Product)
        .Where(ci => ci != null && cartItemIds.Contains(ci.Id))
        .ToList();
}

        public void RemoveCartItems(List<int> cartItemIds)
        {
            if (_context.CartItems == null)
            {
                throw new InvalidOperationException("CartItems collection is null.");
            }

            var cartItems = _context.CartItems.Where(ci => cartItemIds.Contains(ci.Id)).ToList();
            if (cartItems.Any())
            {
                _context.CartItems.RemoveRange(cartItems);
                _context.SaveChanges();
            }
        }

        public CartItem GetCartItemById(int cartItemId)
{
    return _context.CartItems
        .Include(ci => ci.Product)  // Ensure Product is loaded
        .FirstOrDefault(ci => ci.Id == cartItemId);
}
    }
}