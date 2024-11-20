using System;
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

        // Get filtered products with soft delete exclusion
        public IEnumerable<Product> GetFilteredProducts(List<string> categories, string search, string sortBy, string order, bool? hasDiscount)
        {
            var products = _context.Products.Where(p => !p.IsDeleted).AsQueryable(); // Exclude deleted products

            if (hasDiscount.HasValue)
            {
                products = products.Where(p => (p.Discount ?? 0) > 0 == hasDiscount.Value);
            }

            if (categories != null && categories.Any())
            {
                var normalizedCategories = categories.Select(category => category.Trim().ToLower()).ToList();
                products = products.Where(p =>
                    p.Category.Split(new[] { ',' }, StringSplitOptions.None)
                        .Select(c => c.Trim().ToLower())
                        .Any(c => normalizedCategories.Contains(c)));
            }

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p =>
                    p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                products = sortBy.ToLower() switch
                {
                    "price" => order == "desc" ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price),
                    "name" => order == "desc" ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name),
                    _ => products
                };
            }

            return products.ToList();
        }

        // Get all non-deleted products
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Where(p => !p.IsDeleted).ToList(); // Exclude deleted products
        }

        // Get a product by ID (also excludes deleted ones)
        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
        }

        // Add a new product
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        // Update an existing product
        public void UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id && !p.IsDeleted);
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

        // Soft delete a product
        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.IsDeleted = true; // Mark as deleted
                _context.SaveChanges();
            }
        }
    }
}