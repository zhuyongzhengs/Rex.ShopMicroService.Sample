using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计仓储接口
    /// </summary>
    public interface IPageDesignRepository : IRepository<PageDesign, Guid>
    {
    }
}