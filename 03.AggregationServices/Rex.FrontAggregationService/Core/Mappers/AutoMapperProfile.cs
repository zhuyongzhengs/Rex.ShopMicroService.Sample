using AutoMapper;
using Rex.FrontAggregationService.Goods;
using Rex.FrontAggregationService.Orders;
using Rex.GoodService.Goods;
using Rex.GoodService.Products;
using Rex.OrderService.Bills;
using Rex.OrderService.Orders;
using Rex.PaymentService.Payments;

namespace Rex.FrontAggregationService.Core.Mappers
{
    /// <summary>
    /// 自动映射配置类
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // 定义映射关系
            CreateMap<BillPaymentDto, BillPaymentDetailDto>();
            CreateMap<OrderDto, OrderUpdateDto>();
            CreateMap<BillAftersalesDto, BillAftersalesDetailDto>();
            CreateMap<GoodDto, SeckillGoodDto>();
            CreateMap<ProductDto, SeckillProductDto>();
            CreateMap<GoodCommentDto, UserGoodCommentDto>();
        }
    }
}