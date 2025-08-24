using DotNetCore.CAP;
using Microsoft.Extensions.Caching.Memory;
using Rex.OrderService.Orders;
using Rex.PromotionService.Promotions;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events.Orders;
using StackExchange.Redis;
using System.Diagnostics;
using System.Text.Json;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using static Rex.Service.Core.Configurations.GlobalEnums;
using IOrderCommonAppService = Rex.OrderService.Commons.ICommonAppService;
using IPromotionCommonAppService = Rex.PromotionService.Commons.ICommonAppService;

namespace Rex.FrontAggregationService.Events
{
    /// <summary>
    /// 下单(取消)回滚事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class PlaceOrderCallbackEventHandler : ICapSubscribe
    {
        private readonly IOrderCommonAppService _orderCommonAppService;
        private readonly IPromotionCommonAppService _promotionCommonAppService;
        private readonly IMemoryCache _memoryCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public PlaceOrderCallbackEventHandler(IOrderCommonAppService orderCommonAppService, IPromotionCommonAppService promotionCommonAppService, IMemoryCache memoryCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _orderCommonAppService = orderCommonAppService;
            _promotionCommonAppService = promotionCommonAppService;
            _memoryCache = memoryCache;
            _connectionMultiplexer = connectionMultiplexer;
        }

        /// <summary>
        /// 事件执行
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        [CapSubscribe(PlaceOrderCallbackEto.EventName)]
        public async Task PlaceOrderCallbackAsync(PlaceOrderCallbackEto eventData)
        {
            OrderDto order = await _orderCommonAppService.GetOrderByIdAsync(eventData.Id);
            if (order == null) return;
            if (order.OrderType == (int)OrderType.Skill)
            {
                await SeckillOrderCallbackAsync(order);
            }
            else
            {
                await PlaceOrderCallbackAsync(order);
            }
            Task.CompletedTask.Wait();
        }

        /// <summary>
        /// 秒杀订单库存缓存回滚
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private async Task SeckillOrderCallbackAsync(OrderDto order)
        {
            PromotionDto promotion = await _promotionCommonAppService.GetPromotionByIdAsync(order.ObjectId.Value);
            if (promotion == null || promotion.Type != (int)PromotionType.Seckill) return;

            OrderItemDto orderItem = order.OrderItems.First();
            //string seckillLimitKey = $"Rex.GoodService:{{{orderItem.GoodId}}}:SeckillLimit:{{{order.No}}}";
            string orderGoodKey = $"Rex.GoodService:{{{orderItem.GoodId}}}:Order:{{{order.No}}}";
            string userBuyLimitKey = $"Rex.GoodService:{{{orderItem.GoodId}}}:UserBuyLimit:{{{order.UserId}}}";
            string totalBuyLimitKey = $"Rex.GoodService:{{{orderItem.GoodId}}}:TotalBuyLimit";
            string productStockKey = $"Rex.GoodService:{{{orderItem.GoodId}}}:Product:{{{orderItem.ProductId}}}"; // 商品库存 Key 前缀

            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            var pStockKeys = new RedisKey[] { orderGoodKey, userBuyLimitKey, totalBuyLimitKey, productStockKey };
            var pOrderValues = new RedisValue[] {
                promotion.MaxNums, // ARGV[1]
                promotion.MaxGoodNums, // ARGV[2]
                orderItem.Nums // ARGV[3]
            };
            string sOrderScript = _memoryCache.Get<string>(FrontAggregationHostedService.SeckillOrderCallbackLuaKey);
            RedisResult seckillResult = await redisDatabase.ScriptEvaluateAsync(
                sOrderScript,
                pStockKeys,
                pOrderValues);
            string rResult = seckillResult?.ToString();
            if (rResult.IsNullOrWhiteSpace() || !rResult.Equals("1"))
                throw new UserFriendlyException($"{rResult ?? "系统繁忙，请稍后再试"}！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
        }

        /// <summary>
        /// 订单有效性缓存回滚
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private async Task PlaceOrderCallbackAsync(OrderDto order)
        {
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            var goodOrderItems = order.OrderItems.GroupBy(x => x.GoodId).ToList();
            foreach (var goodOrderItem in goodOrderItems)
            {
                string orderGoodKey = $"Rex.GoodService:{{{goodOrderItem.Key}}}:Order:{{{order.No}}}";
                string productStockKeyPrefix = $"Rex.GoodService:{{{goodOrderItem.Key}}}"; // 商品库存 Key 前缀

                List<OrderItemDto> orderItems = goodOrderItem.ToList();
                List<RedisValue> redisValues = new List<RedisValue>();
                foreach (var orderItem in orderItems)
                {
                    string pName = orderItem.Name.Replace(":", "");
                    string rValue = $"{orderItem.ProductId}:{orderItem.Nums}:{pName}";
                    redisValues.Add(rValue);
                }

                string placeOrderScript = _memoryCache.Get<string>(FrontAggregationHostedService.PlaceOrderCallbackKey);
                RedisResult seckillResult = await redisDatabase.ScriptEvaluateAsync(
                    placeOrderScript,
                    new RedisKey[] { orderGoodKey, productStockKeyPrefix },
                    redisValues.ToArray());
                string rResult = seckillResult?.ToString();
                if (rResult.IsNullOrWhiteSpace() || !rResult.Equals("1"))
                    throw new UserFriendlyException($"{rResult ?? "系统繁忙，请稍后再试"}！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            }
        }
    }
}