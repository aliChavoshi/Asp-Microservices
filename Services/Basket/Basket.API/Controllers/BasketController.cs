using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcService;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService, IMapper mapper,
        IPublishEndpoint publishEndpoint)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
    {
        var basket = await _basketRepository.GetBasket(userName);
        return Ok(basket ?? new ShoppingCart(userName));
    }

    [HttpPut]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
        //TODO : Communicate with discount grpc and calculate latest of product into shopping cart
        //TODO : consume discount grpc
        foreach (var item in basket.Items)
        {
            var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            item.Price -= coupon.Amount;
        }

        return Ok(await _basketRepository.UpdateBasket(basket));
    }

    [HttpDelete("{userName}")]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        await _basketRepository.DeleteBasket(userName);
        return Ok();
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<ShoppingCart>> Checkout([FromBody] BasketCheckout basketCheckout,
        CancellationToken cancellationToken)
    {
        //get existing basket with total price
        var basket = await _basketRepository.GetBasket(basketCheckout.UserName);
        if (basket == null) throw new BadHttpRequestException("value is null");
        //Create basketCheckoutEvent -- set totalPrice on basketCheckout eventMessage
        var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;
        //send checkout event to rabbitMQ => Producer  
        await _publishEndpoint.Publish(eventMessage, cancellationToken);
        //remove the basket
        await _basketRepository.DeleteBasket(basket.UserName);
        return Ok(eventMessage);
    }
}