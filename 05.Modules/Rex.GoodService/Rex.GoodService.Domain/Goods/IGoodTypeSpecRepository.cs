using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品类型规格仓储接口
    /// </summary>
    public interface IGoodTypeSpecRepository : IRepository<GoodTypeSpec, Guid>
    {
        /// <summary>
        /// 获取(分页) 商品类型规格列表总数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns></returns>
        Task<long> GetPageCountAsync(
            string name = "",
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取(分页) 商品类型规格列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="name">参数名称</param>
        /// <returns></returns>
        Task<List<GoodTypeSpec>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string name = "",
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 根据ID获取商品类型规格信息
        /// </summary>
        /// <param name="ids">商品类型规格ID</param>
        /// <returns></returns>
        Task<List<GoodTypeSpec>> GetListByIdAsync(
            List<Guid> ids,
            CancellationToken cancellationToken = default
            );
    }
}