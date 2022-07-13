using Application.Features.Orders.Commands.CheckoutOrder;
using AutoMapper;
using EventBus.Messages.Events;

namespace API.Mapping;

public class OrderingProfile : Profile
{
    public OrderingProfile()
    {
        CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
    }
}