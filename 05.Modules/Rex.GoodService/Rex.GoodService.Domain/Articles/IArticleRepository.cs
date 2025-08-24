using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 文章仓储接口
    /// </summary>
    public interface IArticleRepository : IRepository<Article, Guid>
    {
        /// <summary>
        /// 获取(分页) 文章列表总数
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="typeId">文章分类ID</param>
        /// <param name="isPub">是否发布</param>
        /// <returns></returns>
        Task<long> GetPageCountAsync(
            string? title,
            Guid? typeId,
            bool? isPub,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取(分页) 文章列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="title">标题</param>
        /// <param name="typeId">文章分类ID</param>
        /// <param name="isPub">是否发布</param>
        /// <returns></returns>
        Task<List<Article>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? title,
            Guid? typeId,
            bool? isPub,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 根据ID查询文章信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        Task<Article> GetArticleByIdAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default);
    }
}