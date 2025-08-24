using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Ships
{
    /// <summary>
    /// 配送方式仓储接口
    /// </summary>
    public interface IShipRepository : IRepository<Ship, Guid>
    {
        /// <summary>
        /// 获取(分页) 配送方式列表总数
        /// </summary>
        /// <param name="name">配送方式名称</param>
        /// <param name="logiCode">物料公司名称</param>
        /// <param name="logiName">物料公司名称</param>
        /// <returns></returns>
        Task<long> GetPageCountAsync(
            string name,
            string logiCode,
            string logiName,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取(分页) 配送方式列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="name">配送方式名称</param>
        /// <param name="logiCode">物料公司名称</param>
        /// <param name="logiName">物料公司名称</param>
        /// <returns></returns>
        Task<List<Ship>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string name,
            string logiCode,
            string logiName,
            CancellationToken cancellationToken = default
            );
    }
}