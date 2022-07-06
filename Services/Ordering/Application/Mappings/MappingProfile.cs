using Application.Dtos.Order;
using Application.Features.Orders.Commands.CheckoutOrder;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<CheckoutOrderCommand, Order>();
    }
}