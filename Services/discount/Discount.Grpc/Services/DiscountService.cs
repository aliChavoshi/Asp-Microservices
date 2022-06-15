using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.Get(request.ProductName);
        _logger.LogInformation($"Getting discount for product {request.ProductName}");
        // if (coupon == null) throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
        return _mapper.Map<CouponModel>(coupon);
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);
        await _discountRepository.Create(coupon);
        _logger.LogInformation($"Created discount for product {request.Coupon.ProductName}");
        return request.Coupon;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation($"Deleting discount for product {request.ProductName}");
        return new DeleteDiscountResponse()
        {
            Success = await _discountRepository.Delete(request.ProductName)
        };
    }


    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);
        await _discountRepository.Update(coupon);
        _logger.LogInformation($"Updated discount for product {request.Coupon.ProductName}");
        return request.Coupon;
    }
}