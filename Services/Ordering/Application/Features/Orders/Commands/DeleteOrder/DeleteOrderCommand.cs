using MediatR;

namespace Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommand : IRequest
{
    public int Id { get; set; }

    public DeleteOrderCommand(int id)
    {
        Id = id;
    }
}