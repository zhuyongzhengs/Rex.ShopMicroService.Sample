using Rex.GoodService.Areas;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.AreaService.Areas
{
    /// <summary>
    /// 区域仓储接口
    /// </summary>
    public interface IAreaRepository : IRepository<Area, long>
    {
        /// <summary>
        /// 获取(分页) 区域列表总数
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <param name="name">区域名称</param>
        /// <returns></returns>
        Task<long> GetPageCountAsync(
            long? parentId,
            string name,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取(分页) 区域列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="parentId">父级ID</param>
        /// <param name="name">区域名称</param>
        /// <returns></returns>
        Task<List<Area>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            long? parentId,
            string name,
            CancellationToken cancellationToken = default
            );
    }
}