using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.PinTuanGoodService.PinTuans;
using Rex.PromotionService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团规则仓储
    /// </summary>
    public class PinTuanRuleRepository : EfCoreRepository<PromotionServiceDbContext, PinTuanRule, Guid>, IPinTuanRuleRepository
    {
        public PromotionServiceDbContext gServiceDbContext { get; set; }

        public PinTuanRuleRepository(IDbContextProvider<PromotionServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 根据拼团规则ID获取拼团商品
        /// </summary>
        /// <param name="ruleIds">拼团规则ID</param>
        /// <returns></returns>
        public async Task<List<PinTuanGood>> GetPinTuanGoodByRuleIdsAsync(Guid[] ruleIds)
        {
            var list = await gServiceDbContext.PinTuanGoods.Where(p => ruleIds.Contains(p.RuleId)).Include(p => p.PinTuanRule).ToListAsync();
            return list;
        }

        /// <summary>
        /// 获取(分页) 常用促销列表总数
        /// </summary>
        /// <param name="name">促销名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isStatusOpen">是否开启</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(string? name = "", DateTime? startTime = null, DateTime? endTime = null, bool? isStatusOpen = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(startTime.HasValue && endTime.HasValue, p => p.StartTime >= startTime.Value && p.EndTime <= endTime.Value)
                .WhereIf(isStatusOpen.HasValue, p => p.IsStatusOpen == isStatusOpen)
                .LongCountAsync();
        }

        /// <summary>
        /// 获取(分页) 促销列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="name">促销名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isStatusOpen">是否开启</param>
        /// <returns></returns>
        public async Task<List<PinTuanRule>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, string? name, DateTime? startTime, DateTime? endTime, bool? isStatusOpen, CancellationToken cancellationToken)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(startTime.HasValue && endTime.HasValue, p => p.StartTime >= startTime.Value && p.EndTime <= endTime.Value)
                .WhereIf(isStatusOpen.HasValue, p => p.IsStatusOpen == isStatusOpen)
                .Include(p => p.PinTuanGoods)
                .OrderByIf<PinTuanRule, IQueryable<PinTuanRule>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}