using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.PromotionService.EntityFrameworkCore;
using Rex.Service.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 常用促销仓储
    /// </summary>
    public class PromotionCommonRepository : EfCoreRepository<PromotionServiceDbContext, Promotion, Guid>, IPromotionCommonRepository
    {
        public PromotionServiceDbContext sServiceDbContext { get; set; }

        public PromotionCommonRepository(IDbContextProvider<PromotionServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取(分页) 常用促销列表总数
        /// </summary>
        /// <param name="nowDate">当前时间</param>
        /// <param name="name">促销名称</param>
        /// <param name="isEnable">是否开启</param>
        /// <param name="isExclusive">是否排他</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public async Task<long> GetTagPromotionCountAsync(DateTime nowDate, string? name = "", bool? isExclusive = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.IsEnable == true && p.StartTime < nowDate && p.EndTime > nowDate && p.Type == 4)
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(isExclusive.HasValue, p => p.IsExclusive == isExclusive)
                .WhereIf(startTime.HasValue && endTime.HasValue, p => p.StartTime >= startTime.Value && p.EndTime <= endTime.Value)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 常用促销列表
        /// </summary>
        /// <param name="nowDate">当前时间</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="name">促销名称</param>
        /// <param name="isExclusive">是否排他</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public async Task<List<Promotion>> GetTagPromotionAsync(DateTime nowDate, int skipCount, int maxResultCount, string sorting, string? name = "", bool? isExclusive = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.IsEnable == true && p.StartTime < nowDate && p.EndTime > nowDate && p.Type == 4)
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(isExclusive.HasValue, p => p.IsExclusive == isExclusive)
                .WhereIf(startTime.HasValue && endTime.HasValue, p => p.StartTime >= startTime.Value && p.EndTime <= endTime.Value)
                .Include(p => p.PromotionConditions)
                .Include(p => p.PromotionResults)
                .OrderByIf<Promotion, IQueryable<Promotion>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取启用的全局促销
        /// </summary>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        public async Task<List<Promotion>> GetPromotionGlobalIsEnableAsync(bool isEnable = true, bool includeDetails = true)
        {
            int pType = (int)GlobalEnums.PromotionType.Promotion;
            return await (await GetDbSetAsync())
                .Where(p => p.Type == pType && p.IsEnable == isEnable)
                .IncludeIf(includeDetails, p => p.PromotionConditions)
                .IncludeIf(includeDetails, p => p.PromotionResults)
                .AsNoTracking()
                .OrderBy(p => p.Weight)
                .ToListAsync();
        }
    }
}