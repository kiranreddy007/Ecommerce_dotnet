using EcommerceBackend.Models;
using EcommerceBackend.Repositories;

namespace EcommerceBackend.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public Cart GetCartByUserId(int userId)
        {
            return _cartRepository.GetCartByUserId(userId);
        }

        public void AddToCart(int userId, int productId, int quantity)
{
    if (quantity <= 0)
        throw new ArgumentException("Quantity must be greater than 0.", nameof(quantity));

    // Ensure the product exists
    var product = _productRepository.GetProductById(productId);
    if (product == null)
        throw new Exception($"Product with ID {productId} does not exist.");

    // Retrieve or create the user's cart
    var cart = _cartRepository.GetCartByUserId(userId);
    if (cart == null)
    {
        // Create a new cart for the user
        cart = new Cart { UserId = userId };
        _cartRepository.CreateCart(cart);
    }

    // Check if the product is already in the cart
    var cartItem = cart.CartItems?.FirstOrDefault(ci => ci.ProductId == productId);
    if (cartItem != null)
    {
        // Update quantity and price if item already exists
        cartItem.Quantity += quantity;
        cartItem.Price = cartItem.Quantity * product.Price;
        _cartRepository.UpdateCartItem(cartItem);
    }
    else
    {
        // Add a new cart item
        var newCartItem = new CartItem
        {
            CartId = cart.Id,
            ProductId = productId,
            Quantity = quantity,
            Price = quantity * product.Price
        };
        _cartRepository.AddToCart(newCartItem);
    }
}

        public void UpdateCartItem(int cartItemId, int quantity)
        {
            var cartItem = _cartRepository.GetCartByUserId(cartItemId)?.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem != null && quantity > 0)
            {
                cartItem.Quantity = quantity;
                cartItem.Price = quantity * cartItem.Product.Price;
                _cartRepository.UpdateCartItem(cartItem);
            }
        }

        public void RemoveFromCart(int cartItemId)
        {
            _cartRepository.RemoveFromCart(cartItemId);
        }

        public void ClearCart(int userId)
        {
            _cartRepository.ClearCart(userId);
        }
    }
}