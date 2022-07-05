using Application.Contracts.Persistence;
using Application.Dtos.Order;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrderList;

public record GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.GetOrdersByUserName(request.UserName);
        return _mapper.Map<IEnumerable<OrderDto>>(result);
    }
}