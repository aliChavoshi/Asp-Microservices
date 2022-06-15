using Basket.API.GrpcService;
using Basket.API.Repositories;
using Basket.API.Services;
using Discount.Grpc.Protos;

namespace Basket.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //Configuration for redis
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration["ConfigurationRedis:ConnectionString"];
        });
        //DI
        builder.Services.AddScoped<IBasketRepository, BasketRepository>();
        //register grpc
        builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
        {
            options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
        });
        //register grpc service
        builder.Services.AddScoped<DiscountGrpcService>();
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}