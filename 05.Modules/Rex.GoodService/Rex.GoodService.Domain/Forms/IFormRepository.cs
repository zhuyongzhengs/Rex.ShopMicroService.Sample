using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Forms
{
    /// <summary>
    /// 表单仓储接口
    /// </summary>
    public interface IFormRepository : IRepository<Form, Guid>
    {
        /// <summary>
        /// 获取(分页) 表单列表总数
        /// </summary>
        /// <param name="name">表单名称</param>
        /// <param name="type">表单类型</param>
        /// <returns></returns>
        Task<long> GetPageCountAsync(
            string? name,
            int? type,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取(分页) 表单列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="name">表单名称</param>
        /// <param name="type">表单类型</param>
        /// <returns></returns>
        Task<List<Form>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? name,
            int? type,
            CancellationToken cancellationToken = default
            );
    }
}