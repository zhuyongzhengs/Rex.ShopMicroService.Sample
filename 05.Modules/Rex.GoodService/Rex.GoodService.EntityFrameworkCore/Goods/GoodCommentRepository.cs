using DotNetCore.CAP;
using Rex.GoodService.EntityFrameworkCore;
using Rex.Service.Core.Events.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品评价仓储
    /// </summary>
    public class GoodCommentRepository : EfCoreRepository<GoodServiceDbContext, GoodComment, Guid>, IGoodCommentRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }
        private readonly IGoodRepository _goodRepository;
        private readonly ICapPublisher _capEventBus;

        public GoodCommentRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider, IGoodRepository goodRepository, ICapPublisher capEventBus) : base(dbContextProvider)
        {
            _goodRepository = goodRepository;
            _capEventBus = capEventBus;
        }

        /// <summary>
        /// 用户评价
        /// </summary>
        /// <param name="input">评价信息</param>
        /// <returns></returns>
        public async Task UserCommentAsync(List<GoodComment> input, int type = (int)OrderLogType.LogTypeEvaluation)
        {
            // 保存评价信息
            await (await GetDbSetAsync()).AddRangeAsync(input);

            // 更新商品评价数量
            List<Guid> goodIds = input.Select(x => x.GoodId).Distinct().ToList();
            List<Good> goods = await _goodRepository.GetGoodByIdsAsync(goodIds, false);
            foreach (var good in goods) good.CommentsCount++;

            // 更改订单评价状态
            List<Guid> orderIds = input.Select(x => x.OrderId).Distinct().ToList();
            foreach (var orderId in orderIds)
            {
                await _capEventBus.PublishAsync(OrderCommentEto.EventName, new OrderCommentEto()
                {
                    OrderId = orderId,
                    Type = type,
                    IsComment = true
                });
            }
        }
    }
}