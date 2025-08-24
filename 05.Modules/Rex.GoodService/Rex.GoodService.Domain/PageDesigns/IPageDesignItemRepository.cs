using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计项仓储接口
    /// </summary>
    public interface IPageDesignItemRepository : IRepository<PageDesignItem, Guid>
    {
    }
}