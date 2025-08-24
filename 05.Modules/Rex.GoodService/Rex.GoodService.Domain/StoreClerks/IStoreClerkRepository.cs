using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 店铺店员关联仓储接口
    /// </summary>
    public interface IStoreClerkRepository : IRepository<StoreClerk, Guid>
    {
        /// <summary>
        /// 获取(分页) 店铺店员关联列表总数
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        /// <param name="userIds">用户ID(多个用“,”隔开)</param>
        /// <returns></returns>
        Task<long> GetPageCountAsync(
            Guid? storeId,
            string userIds,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取(分页) 店铺店员关联列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="storeId">店铺ID</param>
        /// <param name="userIds">用户ID(多个用“,”隔开)</param>
        /// <returns></returns>
        Task<List<StoreClerk>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            Guid? storeId,
            string userIds,
            CancellationToken cancellationToken = default
            );
    }
}