using Application.Dtos.Order;
using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrderList;

public record GetOrderListQuery : IRequest<IEnumerable<OrderDto>>
{
    public string UserName { get; }

    public GetOrderListQuery(string userName)
    {
        UserName = userName;
    }
}