using Catalog.API.Context;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using MongoDB.Driver;

namespace Catalog.API.Extensions;

public static class HostExtensions
{
    public static async Task<IHost> MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var productRepository = services.GetRequiredService<IProductRepository>();
        // var context = services.GetRequiredService<CatalogContext>();
        //exist product ?
        var products = new List<Product>
        {
            new()
            {
                Name = "IPhone X",
                Price = 150,
                Description = "IPhone Discount",
                Category = "Phone",
                ImageFile = "",
                Summary = "IPhone Discount"
            },
            new()
            {
                Name = "Samsung 10",
                Description = "Samsung Discount",
                Price = 100,
                Summary = "Samsung Discount",
                Category = "Phone",
                ImageFile = ""
            }
        };
        foreach (var product in products)
            await productRepository.AddProduct(product);
        return host;
    }
}