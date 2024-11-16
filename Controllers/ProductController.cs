using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            // TODO: Fetch all products
            return Ok("List of products");
        }

        [HttpPost]
        public IActionResult Create()
        {
            // TODO: Add a new product (Admin only)
            return Ok("Product created");
        }
    }
}