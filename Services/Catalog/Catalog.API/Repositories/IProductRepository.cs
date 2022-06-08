using Catalog.API.Entities;

namespace Catalog.API.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(string id);
    Task<IEnumerable<Product>> GetProductsByName(string name);
    Task<IEnumerable<Product>> GetProductsByCategory(string category);

    Task<Product> AddProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
}