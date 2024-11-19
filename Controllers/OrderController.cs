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

        [HttpGet("all")]
        [Authorize (Roles = "Admin")]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }

        [HttpPost]
[Authorize]
public IActionResult PlaceOrder([FromBody] PlaceOrderRequest request )
{
    if (request == null || request.CartItemIds == null || !request.CartItemIds.Any())
    {
        return BadRequest(new { message = "The cartItemIds field is required and cannot be empty." });
    }

    //shipping info and payment info add here

    var ShippingFirstName = request.ShippingFirstName;
    var ShippingLastName = request.ShippingLastName;
    var ShippingAddress = request.ShippingAddress;


    var userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
    _orderService.PlaceOrder(userId, request.CartItemIds, ShippingFirstName, ShippingLastName, ShippingAddress, request.ShippingCity, request.ShippingPostalCode);

    return Ok(new { message = "Order placed successfully." });
}

        [HttpPatch("{orderId}")]
[Authorize(Roles = "Admin")]
public IActionResult UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusRequest request)
{
    if (request == null || string.IsNullOrWhiteSpace(request.Status))
    {
        return BadRequest(new { message = "The status field is required and cannot be empty." });
    }

    try
    {
        _orderService.UpdateOrderStatus(orderId, request.Status);
        return Ok(new { message = "Order status updated successfully." });
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "An error occurred while updating order status.", details = ex.Message });
    }
}
    }
}