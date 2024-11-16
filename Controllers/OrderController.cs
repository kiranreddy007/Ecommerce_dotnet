using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateOrder()
        {
            // TODO: Create a new order
            return Ok("Order created");
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            // TODO: Fetch orders for the logged-in user
            return Ok("List of orders");
        }
    }
}