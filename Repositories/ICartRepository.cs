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
        
        CartItem GetCartItemById(int cartItemId);
        
        // Retrieve cart items by their IDs
        IEnumerable<CartItem> GetCartItemsByIds(List<int> cartItemIds);

        // Remove specific cart items from the cart
        void RemoveCartItems(List<int> cartItemIds);

    }
}