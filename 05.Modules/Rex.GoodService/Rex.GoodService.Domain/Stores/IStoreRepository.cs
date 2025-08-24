using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories;
using System.Data;

namespace Rex.GoodService.Stores
{
    /// <summary>
    /// 门店仓储接口
    /// </summary>
    public interface IStoreRepository : IRepository<Store, Guid>
    {
        /// <summary>
        /// 根据坐标获取门店数量
        /// </summary>
        /// <param name="storeName">门店名称</param>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">精度</param>
        /// <returns></returns>
        public Task<long> GetStoreByCoordinateCountAsync(string? storeName, decimal latitude = 0, decimal longitude = 0, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据坐标获取门店
        /// </summary>
        /// <param name="storeName">门店名称</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">精度</param>
        /// <returns></returns>
        Task<List<Store>> GetStoreByCoordinateListAsync(string? storeName, int skipCount, int maxResultCount, string sorting, decimal latitude = 0, decimal longitude = 0, CancellationToken cancellationToken = default);
    }
}