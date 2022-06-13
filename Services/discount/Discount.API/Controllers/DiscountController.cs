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
    public async Task<IActionResult> Post([FromBody] Coupon coupon)
    {
        return Ok(await _discountRepository.Create(coupon));
    }

    [HttpGet("{productName}")]
    public async Task<IActionResult> Get(string productName)
    {
        return Ok(await _discountRepository.Get(productName));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _discountRepository.Get());
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Coupon coupon)
    {
        return Ok(await _discountRepository.Update(coupon));
    }

    [HttpDelete("{productName}")]
    public async Task<IActionResult> Delete(string productName)
    {
        return Ok(await _discountRepository.Delete(productName));
    }
}