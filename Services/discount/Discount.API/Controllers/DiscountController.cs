using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository _discountRepository;

    public DiscountController(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Coupon>> Post([FromBody] Coupon coupon)
    {
        return Ok(await _discountRepository.Create(coupon));
    }

    [HttpGet("{productName}")]
    public async Task<ActionResult<Coupon>> Get(string productName)
    {
        return Ok(await _discountRepository.Get(productName));
    }

    [HttpGet]
    public async Task<ActionResult<List<Coupon>>> Get()
    {
        return Ok(await _discountRepository.Get());
    }

    [HttpPut]
    public async Task<ActionResult<Coupon>> Put([FromBody] Coupon coupon)
    {
        return Ok(await _discountRepository.Update(coupon));
    }

    [HttpDelete("{productName}")]
    public async Task<ActionResult<bool>> Delete(string productName)
    {
        return Ok(await _discountRepository.Delete(productName));
    }
}