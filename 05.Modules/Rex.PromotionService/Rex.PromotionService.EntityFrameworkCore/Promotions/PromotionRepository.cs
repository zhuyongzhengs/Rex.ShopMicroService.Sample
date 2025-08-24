using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.PromotionService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 促销表仓储
    /// </summary>
    public class PromotionRepository : EfCoreRepository<PromotionServiceDbContext, Promotion, Guid>, IPromotionRepository
    {
        public PromotionServiceDbContext sServiceDbContext { get; set; }

        public PromotionRepository(IDbContextProvider<PromotionServiceDbContext> dbContextProvider) : base(dbContextProvider)
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
        /// <param name="status">状态</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(int[] types, string? name = "", bool? isEnable = null, bool? isExclusive = null, DateTime? startTime = null, DateTime? endTime = null, int? status = null, CancellationToken cancellationToken = default)
        {
            DateTime nowDate = DateTime.Now;
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(types.Any(), p => types.Contains(p.Type))
                .WhereIf(isEnable.HasValue, p => p.IsEnable == isEnable)
                .WhereIf(isExclusive.HasValue, p => p.IsExclusive == isExclusive)
                .WhereIf(startTime.HasValue && endTime.HasValue, p => p.StartTime >= startTime.Value && p.EndTime <= endTime.Value)
                .WhereIf(status.HasValue && status.Value == (int)PinTuanRuleStatus.NotBegun, p => p.StartTime > nowDate)
                .WhereIf(status.HasValue && status.Value == (int)PinTuanRuleStatus.Begin, p => p.StartTime <= nowDate && p.EndTime > nowDate)
                .WhereIf(status.HasValue && status.Value == (int)PinTuanRuleStatus.HaveExpired, p => p.EndTime < nowDate)
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
        /// <param name="status">状态</param>
        /// <returns></returns>
        public async Task<List<Promotion>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, int[] types, string? name = "", bool? isEnable = null, bool? isExclusive = null, DateTime? startTime = null, DateTime? endTime = null, int? status = null, CancellationToken cancellationToken = default)
        {
            DateTime nowDate = DateTime.Now;
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(types.Any(), p => types.Contains(p.Type))
                .WhereIf(isEnable.HasValue, p => p.IsEnable == isEnable)
                .WhereIf(isExclusive.HasValue, p => p.IsExclusive == isExclusive)
                .WhereIf(startTime.HasValue && endTime.HasValue, p => p.StartTime >= startTime.Value && p.EndTime <= endTime.Value)
                .WhereIf(status.HasValue && status.Value == (int)PinTuanRuleStatus.NotBegun, p => p.StartTime > nowDate)
                .WhereIf(status.HasValue && status.Value == (int)PinTuanRuleStatus.Begin, p => p.StartTime <= nowDate && p.EndTime > nowDate)
                .WhereIf(status.HasValue && status.Value == (int)PinTuanRuleStatus.HaveExpired, p => p.EndTime < nowDate)
                .Include(p => p.PromotionConditions)
                .Include(p => p.PromotionResults)
                .OrderByIf<Promotion, IQueryable<Promotion>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取促销详情
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Promotion>> GetPromotionByIdsAsync(Guid[] ids, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).Where(x => ids.Contains(x.Id))
                .IncludeIf(includeDetails, x => x.PromotionConditions)
                .IncludeIf(includeDetails, x => x.PromotionResults)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取促销详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        public async Task<Promotion> GetPromotionByIdAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).Where(x => x.Id == id)
                .IncludeIf(includeDetails, x => x.PromotionConditions)
                .IncludeIf(includeDetails, x => x.PromotionResults)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }
    }
}