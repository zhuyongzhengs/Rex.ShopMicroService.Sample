using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.GoodService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 文章仓储
    /// </summary>
    public class ArticleRepository : EfCoreRepository<GoodServiceDbContext, Article, Guid>, IArticleRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public ArticleRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取(分页) 区域列表总数
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="typeId">文章分类ID</param>
        /// <param name="isPub">是否发布</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(string? title, Guid? typeId, bool? isPub, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!title.IsNullOrWhiteSpace(), p => p.Title.Contains(title))
                .WhereIf(typeId.HasValue, p => p.TypeId == typeId.Value)
                .WhereIf(isPub.HasValue, p => p.IsPub == isPub.Value)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 区域列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="title">标题</param>
        /// <param name="typeId">文章分类ID</param>
        /// <param name="isPub">是否发布</param>
        /// <returns></returns>
        public async Task<List<Article>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, string? title, Guid? typeId, bool? isPub, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!title.IsNullOrWhiteSpace(), p => p.Title.Contains(title))
                .WhereIf(typeId.HasValue, p => p.TypeId == typeId.Value)
                .WhereIf(isPub.HasValue, p => p.IsPub == isPub.Value)
                .Include(p => p.ArticleType)
                .OrderByIf<Article, IQueryable<Article>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 查询文章信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<Article> GetArticleByIdAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(x => x.Id == id)
                .IncludeIf(includeDetails, x => x.ArticleType)
                .FirstAsync(GetCancellationToken(cancellationToken));
        }
    }
}