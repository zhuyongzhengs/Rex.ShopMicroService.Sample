using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 常用促销仓储接口
    /// </summary>
    public interface IPromotionCommonRepository : IRepository<Promotion, Guid>
    {
        /// <summary>
        /// 获取(分页) 常用促销列表总数
        /// </summary>
        /// <param name="nowDate">当前时间</param>
        /// <param name="name">促销名称</param>
        /// <param name="isExclusive">是否排他</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        Task<long> GetTagPromotionCountAsync(
            DateTime nowDate,
            string? name = "",
            bool? isExclusive = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken cancellationToken = default
            );

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
        Task<List<Promotion>> GetTagPromotionAsync(
            DateTime nowDate,
            int skipCount,
            int maxResultCount,
            string sorting,
            string? name = "",
            bool? isExclusive = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取启用的全局促销
        /// </summary>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        Task<List<Promotion>> GetPromotionGlobalIsEnableAsync(bool isEnable = true, bool includeDetails = true);
    }
}