using Catalog.API.Context;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using MongoDB.Driver;

namespace Catalog.API.Services;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.Find(x => true).ToListAsync();
    }

    public async Task<Product> GetProduct(string id)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
        return await _context.Products.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.Name, name);
        return await _context.Products.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.Category, category);
        return await _context.Products.Find(filter).ToListAsync();
    }

    public async Task AddProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var result = await _context.Products.ReplaceOneAsync(x => x.Id == product.Id, product);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var result = await _context.Products.DeleteOneAsync(x => x.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}