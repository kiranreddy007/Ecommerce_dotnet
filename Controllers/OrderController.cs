using Microsoft.AspNetCore.Mvc;
using EcommerceBackend.Services;
using Microsoft.AspNetCore.Authorization;

using EcommerceBackend.DTOs;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult GetUserOrders()
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
            return Ok(_orderService.GetOrdersByUserId(userId));
        }

        [HttpPost("Create")]
[Authorize]
public IActionResult PlaceOrder([FromBody] PlaceOrderRequest request )
{
    if (request == null || request.CartItemIds == null || !request.CartItemIds.Any())
    {
        return BadRequest(new { message = "The cartItemIds field is required and cannot be empty." });
    }

    var userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
    _orderService.PlaceOrder(userId, request.CartItemIds);

    return Ok(new { message = "Order placed successfully." });
}

        [HttpPatch("{orderId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateOrderStatus(int orderId, [FromBody] string status)
        {
            _orderService.UpdateOrderStatus(orderId, status);
            return Ok(new { message = "Order status updated successfully." });
        }
    }
}