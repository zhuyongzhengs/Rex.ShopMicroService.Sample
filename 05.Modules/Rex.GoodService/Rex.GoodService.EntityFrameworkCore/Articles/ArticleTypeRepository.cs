using Rex.GoodService.Articles;
using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.ArticleTypes
{
    /// <summary>
    /// 文章分类仓储
    /// </summary>
    public class ArticleTypeRepository : EfCoreRepository<GoodServiceDbContext, ArticleType, Guid>, IArticleTypeRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public ArticleTypeRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}