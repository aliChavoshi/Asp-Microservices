using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;

    public BasketController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
    {
        var basket = await _basketRepository.GetBasket(userName);
        return Ok(basket ?? new ShoppingCart(userName));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
    {
        return Ok(await _basketRepository.UpdateBasket(basket));
    }

    [HttpDelete("{userName}")]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        await _basketRepository.DeleteBasket(userName);
        return Ok();
    }
}