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

        public IEnumerable<Product> GetFilteredProducts(string category, string search, string sortBy, string order, bool? hasDiscount)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category.ToLower() == category.ToLower());
            }
            Console.WriteLine("Search: " + search);
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }

            if (hasDiscount.HasValue && hasDiscount.Value)
            {
                query = query.Where(p => p.Discount.HasValue && p.Discount > 0);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "price" => order.ToLower() == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                    "discount" => order.ToLower() == "desc" ? query.OrderByDescending(p => p.Discount) : query.OrderBy(p => p.Discount),
                    _ => query
                };
            }



            return query.ToList();
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