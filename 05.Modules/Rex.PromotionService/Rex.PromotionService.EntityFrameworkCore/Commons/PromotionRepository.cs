using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.PromotionService.EntityFrameworkCore;
using Rex.PromotionService.PinTuans;
using Rex.PromotionService.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.PromotionService.Commons
{
    /// <summary>
    /// 公共仓储
    /// </summary>
    public class CommonRepository : EfCoreRepository<PromotionServiceDbContext, Promotion, Guid>, ICommonRepository
    {
        public PromotionServiceDbContext sServiceDbContext { get; set; }

        public CommonRepository(IDbContextProvider<PromotionServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取(分页) 促销列表总数
        /// </summary>
        /// <param name="types">类型</param>
        /// <param name="name">促销名称</param>
        /// <param name="isEnable">是否开启</param>
        /// <param name="isExclusive">是否排他</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(int[] types, string? name = "", bool? isEnable = null, bool? isExclusive = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(types.Any(), p => types.Contains(p.Type))
                .WhereIf(isEnable.HasValue, p => p.IsEnable == isEnable)
                .WhereIf(isExclusive.HasValue, p => p.IsExclusive == isExclusive)
                .WhereIf(startTime.HasValue && endTime.HasValue, p => p.StartTime >= startTime.Value && p.EndTime <= endTime.Value)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 促销列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="types">类型</param>
        /// <param name="name">促销名称</param>
        /// <param name="isEnable">是否开启</param>
        /// <param name="isExclusive">是否排他</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public async Task<List<Promotion>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, int[] types, string? name = "", bool? isEnable = null, bool? isExclusive = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(types.Any(), p => types.Contains(p.Type))
                .WhereIf(isEnable.HasValue, p => p.IsEnable == isEnable)
                .WhereIf(isExclusive.HasValue, p => p.IsExclusive == isExclusive)
                .WhereIf(startTime.HasValue && endTime.HasValue, p => p.StartTime >= startTime.Value && p.EndTime <= endTime.Value)
                .Include(p => p.PromotionConditions)
                .Include(p => p.PromotionResults)
                .OrderByIf<Promotion, IQueryable<Promotion>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取可领取的优惠劵
        /// </summary>
        /// <param name="limit">优惠劵数量，默认：3</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public async Task<List<Promotion>> GetReceiveCouponAsync(int limit = 3, int type = 2, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.IsEnable && p.EndTime > DateTime.Now && p.Type == type && p.IsAutoReceive)
                .Include(p => p.PromotionConditions)
                .Include(p => p.PromotionResults)
                .PageBy(0, limit)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取团购秒杀数据
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <param name="nowDate">当前日期</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        public async Task<Promotion> GetGroupPurchaseAsync(Guid promotionId, DateTime nowDate, bool isEnable = true, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.Id == promotionId && p.IsEnable && p.EndTime > nowDate)
                .Include(p => p.PromotionConditions)
                .Include(p => p.PromotionResults)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 根据商品ID获取拼团信息
        /// </summary>
        /// <param name="goodsId">商品ID</param>
        /// <returns></returns>
        public async Task<PinTuanGood> GetPinTuanGoodsAsync(Guid goodsId, CancellationToken cancellationToken = default)
        {
            DateTime nowDate = DateTime.Now;
            return await sServiceDbContext.PinTuanGoods
                .Where(p => p.GoodId == goodsId)
                .Include(p => p.PinTuanRule)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}