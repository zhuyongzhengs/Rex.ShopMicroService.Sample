using AutoMapper;
using Rex.BackendAggregationService.Goods;
using Rex.BackendAggregationService.Orders;
using Rex.BackendAggregationService.Payments;
using Rex.BackendAggregationService.Promotions;
using Rex.GoodService.Goods;
using Rex.GoodService.Products;
using Rex.OrderService.Bills;
using Rex.OrderService.Orders;
using Rex.PaymentService.Payments;
using Rex.PromotionService.Promotions;

namespace Rex.BackendAggregationService.Core.Mappers
{
    /// <summary>
    /// 自动映射配置类
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // 定义映射关系
            CreateMap<OrderDto, OrderDetailDto>();
            CreateMap<OrderDto, OrderDeliveryDto>();
            CreateMap<BillPaymentDto, BillPaymentDetailDto>();
            CreateMap<OrderDto, OrderUpdateDto>();
            CreateMap<BillAftersalesDto, BillAftersalesManyDto>();
            CreateMap<BillAftersalesDto, BillAftersalesDtlDto>();
            CreateMap<BillReshipDto, BillReshipManyDto>();
            CreateMap<BillRefundDto, BillRefundManyDto>();
            CreateMap<GoodCommentDto, GoodCommentManyDto>();
            CreateMap<CouponDto, CouponManyDto>();
            CreateMap<ProductDto, ProductDetailDto>();
        }
    }
}