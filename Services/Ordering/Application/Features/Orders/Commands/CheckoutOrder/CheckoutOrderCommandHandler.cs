using System.ComponentModel.DataAnnotations;
using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        // 1. Create Order
        // 2. Send Email To user
        // 3. Return order Id
        var order = _mapper.Map<Order>(request);
        var newOrder = await _orderRepository.AddAsync(order).ConfigureAwait(false);
        await SendEmail(newOrder);
        return newOrder.Id;
    }

    private async Task SendEmail(Order order)
    {
        var email = new Email("alichavoshii1372@gmail.com", "test", "test");
        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}