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

    public async Task<Product> AddProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
        return product;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var result = await _context.Products.ReplaceOneAsync(x => x.Id == product.Id, product);
        if (result.IsAcknowledged && result.ModifiedCount > 0)
            return product;
        return null;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var result = await _context.Products.DeleteOneAsync(x => x.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}