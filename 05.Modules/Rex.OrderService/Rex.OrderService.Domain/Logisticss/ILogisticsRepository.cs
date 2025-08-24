using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Logisticss
{
    /// <summary>
    /// 物流仓储接口
    /// </summary>
    public interface ILogisticsRepository : IRepository<Logistics, Guid>
    {
        /// <summary>
        /// 获取(分页) 物流列表总数
        /// </summary>
        /// <param name="logiCode">物流公司编码</param>
        /// <param name="logiName">物流公司名称</param>
        /// <param name="phone">物流电话</param>
        /// <returns></returns>
        Task<long> GetPageCountAsync(
            string logiCode,
            string logiName,
            string phone,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取(分页) 物流列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="logiCode">物流公司编码</param>
        /// <param name="logiName">物流公司名称</param>
        /// <param name="phone">物流电话</param>
        /// <returns></returns>
        Task<List<Logistics>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string logiCode,
            string logiName,
            string phone,
            CancellationToken cancellationToken = default
            );
    }
}