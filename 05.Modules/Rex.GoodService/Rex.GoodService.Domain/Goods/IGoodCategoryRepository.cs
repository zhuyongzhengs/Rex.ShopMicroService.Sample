using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品分类仓储接口
    /// </summary>
    public interface IGoodCategoryRepository : IRepository<GoodCategory, Guid>
    {
    }
}