using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger)
    {
        _discountRepository = discountRepository;
        _logger = logger;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var discount = await _discountRepository.Get(request.ProductName);
        if (discount == null) throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
        var response = new CouponModel
        {
            Id = discount.Id,
            ProductName = discount.ProductName,
            Description = discount.Description,
            Amount = discount.Amount
        };
        return response;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        await _discountRepository.Create(new Coupon()
        {
            ProductName = request.Coupon.ProductName,
            Amount = request.Coupon.Amount,
            Description = request.Coupon.Description,
            Id = request.Coupon.Id
        });
        return request.Coupon;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var result = await _discountRepository.Delete(request.ProductName);
        return new DeleteDiscountResponse()
        {
            Success = result
        };
    }


    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        await _discountRepository.Update(new Coupon()
        {
            ProductName = request.Coupon.ProductName,
            Amount = request.Coupon.Amount,
            Description = request.Coupon.Description,
            Id = request.Coupon.Id
        });
        return request.Coupon;
    }
}