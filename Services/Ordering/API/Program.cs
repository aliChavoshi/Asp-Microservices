using API.Consumer;
using API.Mapping;
using Application;
using EventBus.Messages.Common;
using EventBus.Messages.Events;
using Infrastructure;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//RabbitMQ
builder.Services.AddMassTransit(options =>
{
    //add consumer in rabbitMQ
    options.AddConsumer<BasketCheckoutConsumer>();
    options.UsingRabbitMq((context, configurator) =>
    {
        //username:password@localhost:port
        configurator.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        configurator.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue,
            c => { c.ConfigureConsumer<BasketCheckoutConsumer>(context); }
        );
    });
});
// builder.Services.AddMassTransitHostedService();
//add auto mapper
builder.Services.AddAutoMapper(typeof(OrderingProfile));
builder.Services.AddScoped<BasketCheckoutConsumer>();


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