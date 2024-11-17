using EcommerceBackend.Models;
using EcommerceBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EcommerceBackend.Repositories;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly IProductRepository _productRepository;



        public ProductsController(IProductService productService, IProductRepository productRepository)
        {
            _productService = productService;
            _productRepository = productRepository;
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
public IActionResult AddProduct([FromForm] Product product, IFormFile imageFile)
{
    if (product == null)
    {
        return BadRequest("Invalid product data.");
    }

    if (imageFile != null)
    {
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            imageFile.CopyTo(stream);
        }

        product.ImagePath = Path.Combine("images", fileName); // Save relative path
    }

    _productRepository.AddProduct(product);
    return Ok(new { message = "Product added successfully." });
}

        [Authorize(Roles = "Admin")]
[HttpPut("{id}")]
public IActionResult UpdateProduct(int id, [FromForm] Product product, IFormFile? imageFile)
{
    // Ensure the product exists
    var existingProduct = _productRepository.GetProductById(id);
    if (existingProduct == null)
    {
        return NotFound(new { message = "Product not found." });
    }

    // Update only the provided fields
    if (!string.IsNullOrEmpty(product.Name))
    {
        existingProduct.Name = product.Name;
    }
    if (!string.IsNullOrEmpty(product.Category))
    {
        existingProduct.Category = product.Category;
    }
    if (!string.IsNullOrEmpty(product.Description))
    {
        existingProduct.Description = product.Description;
    }
    if (product.Price > 0)
    {
        existingProduct.Price = product.Price;
    }
    if (product.Stock > 0)
    {
        existingProduct.Stock = product.Stock;
    }
    if (product.Discount != null)
    {
        existingProduct.Discount = product.Discount;
    }

    // Handle image upload if provided
    if (imageFile != null)
    {
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            imageFile.CopyTo(stream);
        }

        existingProduct.ImagePath = Path.Combine("images", fileName); // Update image path
    }
    Console.WriteLine("product" + existingProduct);

    // Save the updated product
    _productRepository.UpdateProduct(existingProduct);

    return Ok(new { message = "Product updated successfully." });
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


        [HttpPost("upload-image")]
public async Task<IActionResult> UploadImage(IFormFile file)
{
    if (file == null || file.Length == 0)
    {
        return BadRequest("No file uploaded.");
    }

    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
    if (!Directory.Exists(uploadPath))
    {
        Directory.CreateDirectory(uploadPath);
    }

    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
    var filePath = Path.Combine(uploadPath, fileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    var relativePath = Path.Combine("images", fileName);
    return Ok(new { imagePath = relativePath });
}
    }
}