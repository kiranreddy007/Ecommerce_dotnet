using System.Collections.Generic;
using System.Linq;
using EcommerceBackend.Models;
using EcommerceBackend.Data;

namespace EcommerceBackend.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       public IEnumerable<Product> GetFilteredProducts(List<string> categories, string search, string sortBy, string order, bool? hasDiscount)
{
    var products = _context.Products.AsQueryable();

   

    if (hasDiscount.HasValue)
    {
        products = products.Where(p => (p.Discount ?? 0) > 0 == hasDiscount.Value);
    }

    // Switch to client-side evaluation for unsupported operations
    var productList = products.ToList();

    // Filter by categories (in memory)
    if (categories != null && categories.Any())
    {
        var normalizedCategories = categories.Select(category => category.Trim().ToLower()).ToList();

        productList = productList.Where(p =>
            p.Category.Split(',')
                .Select(c => c.Trim().ToLower())
                .Any(c => normalizedCategories.Contains(c)))
            .ToList();
    }
    if (!string.IsNullOrEmpty(search))
    {
        productList = productList.Where(p =>
            p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
            p.Description.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
    }


    // Apply sorting
    if (!string.IsNullOrEmpty(sortBy))
    {
        productList = sortBy.ToLower() switch
        {
            "price" => order == "desc" ? productList.OrderByDescending(p => p.Price).ToList() : productList.OrderBy(p => p.Price).ToList(),
            "name" => order == "desc" ? productList.OrderByDescending(p => p.Name).ToList() : productList.OrderBy(p => p.Name).ToList(),
            _ => productList
        };
    }

    return productList;
}
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
    if (existingProduct != null)
    {
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Description = product.Description;
        existingProduct.Category = product.Category;
        existingProduct.Stock = product.Stock;
        existingProduct.Discount = product.Discount;
        existingProduct.ImagePath = product.ImagePath; // Update the image path
        _context.SaveChanges();
    }
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}