using Application.Dtos.Order;
using Application.Features.Orders.Commands.CheckoutOrder;
using Application.Features.Orders.Commands.UpdateOrder;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<CheckoutOrderCommand, Order>().ReverseMap();
        CreateMap<UpdateOrderCommand, Order>().ReverseMap();
    }
}