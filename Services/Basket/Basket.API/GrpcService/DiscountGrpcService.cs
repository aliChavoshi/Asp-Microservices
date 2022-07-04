using Discount.Grpc.Protos;

namespace Basket.API.GrpcService;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    {
        _discountProtoService = discountProtoService;
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountReq = new GetDiscountRequest {ProductName = productName};
        return await _discountProtoService.GetDiscountAsync(discountReq);
    }

    public async Task<CouponModel> CreateDiscount(CreateDiscountRequest request)
    {
        return await _discountProtoService.CreateDiscountAsync(request);
    }
}