using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 文章分类仓储接口
    /// </summary>
    public interface IArticleTypeRepository : IRepository<ArticleType, Guid>
    {
    }
}