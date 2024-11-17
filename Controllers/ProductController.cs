using EcommerceBackend.Models;
using EcommerceBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetFilteredProducts([FromQuery] string? category, [FromQuery] string? search,
                                                 [FromQuery] string? sortBy, [FromQuery] string? order = "asc", 
                                                 [FromQuery] bool? hasDiscount = null)
        {
            var products = _productService.GetFilteredProducts(category, search, sortBy, order, hasDiscount);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _productService.AddProduct(product);
            return Ok(new { message = "Product added successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (_productService.GetProductById(id) == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            product.Id = id;
            _productService.UpdateProduct(product);
            return Ok(new { message = "Product updated successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (_productService.GetProductById(id) == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            _productService.DeleteProduct(id);
            return Ok(new { message = "Product deleted successfully" });
        }
    }
}