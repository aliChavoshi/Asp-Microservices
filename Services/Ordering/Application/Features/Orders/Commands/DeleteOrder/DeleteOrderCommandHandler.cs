using Application.Contracts.Persistence;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _orderRepository.GetByIdAsync(request.Id);
        if (entity == null) throw new NotFoundException(nameof(Order), request.Id);
        await _orderRepository.DeleteAsync(entity).ConfigureAwait(false);
        return Unit.Value;
    }
}