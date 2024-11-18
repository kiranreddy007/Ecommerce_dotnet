using System.Collections.Generic;
using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetFilteredProducts(List<string> categories, string search, string sortBy, string order, bool? hasDiscount);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}