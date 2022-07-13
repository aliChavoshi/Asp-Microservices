using Application.Contracts.Persistence;
using Application.Dtos.Order;
using Application.Features.Orders.Commands.CheckoutOrder;
using Application.Features.Orders.Commands.DeleteOrder;
using Application.Features.Orders.Commands.UpdateOrder;
using Application.Features.Orders.Queries.GetOrderList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
internal class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([FromRoute] string username,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetOrderListQuery(username), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        return Ok(await _mediator.Send(new DeleteOrderCommand(id)));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}