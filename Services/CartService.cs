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
        // Create a new cart for the user if it doesn't exist
        cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
        _cartRepository.CreateCart(cart);
    }

    // Ensure cart.CartItems is initialized
    cart.CartItems ??= new List<CartItem>();

    // Check if the product is already in the cart
    var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

    if (cartItem != null)
    {
        // Update quantity and price if item already exists
        cartItem.Quantity = cartItem.Quantity+quantity;
        cartItem.Price = cartItem.Quantity * product.Price;

        // Persist changes to the cart item
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

        // Add the new cart item to the cart's in-memory list to keep it consistent
        cart.CartItems.Add(newCartItem);
    }
}


       public void UpdateCartItem(int cartItemId, int quantity)
{
    if (quantity <= 0)
        throw new ArgumentException("Quantity must be greater than 0.", nameof(quantity));

    // Retrieve the CartItem by its ID
    var cartItem = _cartRepository.GetCartItemById(cartItemId);
    if (cartItem == null)
        throw new Exception($"CartItem with ID {cartItemId} does not exist.");

    // Ensure the associated Product exists
    if (cartItem.Product == null)
        throw new Exception($"Product associated with CartItem ID {cartItemId} does not exist.");

    // Update the CartItem's quantity and price
    cartItem.Quantity = quantity;
    cartItem.Price = quantity * cartItem.Product.Price;

    // Persist the changes
    _cartRepository.UpdateCartItem(cartItem);
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