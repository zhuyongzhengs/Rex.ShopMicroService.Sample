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
    public interface IPromotionRepository : IRepository<Promotion, Guid>
    {
        /// <summary>
        /// 获取(分页) 常用促销列表总数
        /// </summary>
        /// <param name="types">类型</param>
        /// <param name="name">促销名称</param>
        /// <param name="isEnable">是否开启</param>
        /// <param name="isExclusive">是否排他</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        Task<long> GetPageCountAsync(
            int[] types,
            string? name = "",
            bool? isEnable = null,
            bool? isExclusive = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? status = null,
            CancellationToken cancellationToken = default
            );

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
        Task<List<Promotion>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            int[] types,
            string? name = "",
            bool? isEnable = null,
            bool? isExclusive = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? status = null,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取促销详情
        /// </summary>
        /// <param name="ids">ID</param>
        /// <returns></returns>
        Task<List<Promotion>> GetPromotionByIdsAsync(Guid[] ids, bool includeDetails = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取促销详情
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<Promotion> GetPromotionByIdAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default);
    }
}