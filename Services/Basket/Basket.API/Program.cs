using System.Reflection;
using Basket.API.GrpcService;
using Basket.API.Mapper;
using Basket.API.Repositories;
using Basket.API.Services;
using Discount.Grpc.Protos;
using MassTransit;

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
        //rabbitMQ
        builder.Services.AddMassTransit(options =>
        {
            options.UsingRabbitMq((context, configurator) =>
            {
                //username:password@localhost:port
                configurator.Host(builder.Configuration["EventBusSettings:HostAddress"]);
            });
        });
        // builder.Services.AddMassTransitHostedService();
        //mapper
        builder.Services.AddAutoMapper(typeof(BasketProfile));
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