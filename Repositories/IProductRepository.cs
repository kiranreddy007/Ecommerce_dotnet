using System.Collections.Generic;
using EcommerceBackend.Models;

namespace EcommerceBackend.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetFilteredProducts(List<string> categories, string search, string sortBy, string order, bool? hasDiscount);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}