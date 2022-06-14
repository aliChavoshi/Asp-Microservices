using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mapping;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
    }
}