using EcommerceBackend.Models;
using EcommerceBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCart()
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
            var cart = _cartService.GetCartByUserId(userId);
            return Ok(cart);
        }

        [Authorize]
        [HttpPost("{productId}")]
        public IActionResult AddToCart(int productId, [FromQuery] int quantity)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);

            Console.WriteLine("userId: " + userId);
            Console.WriteLine("productId: " + productId);
            Console.WriteLine("quantity: " + quantity);


            _cartService.AddToCart(userId, productId, quantity);
            return Ok(new { message = "Product added to cart" });
        }

        [Authorize]
        [HttpPut("{cartItemId}")]
        public IActionResult UpdateCartItem(int cartItemId, [FromQuery] int quantity)
        {
            _cartService.UpdateCartItem(cartItemId, quantity);
            return Ok(new { message = "Cart item updated" });
        }

        [Authorize]
        [HttpDelete("{cartItemId}")]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            _cartService.RemoveFromCart(cartItemId);
            return Ok(new { message = "Product removed from cart" });
        }

        [Authorize]
        [HttpDelete("clear")]
        public IActionResult ClearCart()
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
            _cartService.ClearCart(userId);
            return Ok(new { message = "Cart cleared" });
        }
    }
}