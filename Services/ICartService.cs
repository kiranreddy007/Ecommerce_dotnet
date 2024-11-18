using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface ICartService
    {
        Cart GetCartByUserId(int userId);

        
    // Other methods...
        void AddToCart(int userId, int productId, int quantity);
        void UpdateCartItem(int cartItemId, int quantity);
        void RemoveFromCart(int cartItemId);
        void ClearCart(int userId);
    }
}