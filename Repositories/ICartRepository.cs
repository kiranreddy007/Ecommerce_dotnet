using EcommerceBackend.Models;
using System.Collections.Generic;

namespace EcommerceBackend.Repositories
{
    public interface ICartRepository
    {
        Cart GetCartByUserId(int userId);
        void CreateCart(Cart cart);
        void AddToCart(CartItem cartItem);
        void UpdateCartItem(CartItem cartItem);
        void RemoveFromCart(int cartItemId);
        void ClearCart(int userId);
    }
}