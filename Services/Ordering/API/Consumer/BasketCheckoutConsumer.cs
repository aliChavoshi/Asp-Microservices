using Application.Features.Orders.Commands.CheckoutOrder;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;

namespace API.Consumer;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public BasketCheckoutConsumer(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
        return _mediator.Send(command, context.CancellationToken);
    }
}