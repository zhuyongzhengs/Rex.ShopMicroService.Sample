using Rex.PromotionService.PinTuans;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.PinTuanGoodService.PinTuans
{
    /// <summary>
    /// 拼团规则仓储接口
    /// </summary>
    public interface IPinTuanRuleRepository : IRepository<PinTuanRule, Guid>
    {
        /// <summary>
        /// 获取(分页) 拼团规则列表总数
        /// </summary>
        /// <param name="name">活动名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isStatusOpen">是否开启</param>
        /// <returns></returns>
        Task<long> GetPageCountAsync(
            string? name = "",
            DateTime? startTime = null,
            DateTime? endTime = null,
            bool? isStatusOpen = null,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取(分页) 拼团规则列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="name">活动名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isStatusOpen">是否开启</param>
        /// <returns></returns>
        Task<List<PinTuanRule>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? name = "",
            DateTime? startTime = null,
            DateTime? endTime = null,
            bool? isStatusOpen = null,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 根据拼团规则ID获取拼团商品
        /// </summary>
        /// <param name="ruleIds">拼团规则ID</param>
        /// <returns></returns>
        Task<List<PinTuanGood>> GetPinTuanGoodByRuleIdsAsync(Guid[] ruleIds);
    }
}