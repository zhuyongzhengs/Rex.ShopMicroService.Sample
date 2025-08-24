using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Brands
{
    /// <summary>
    /// 品牌仓储接口
    /// </summary>
    public interface IBrandRepository : IRepository<Brand, Guid>
    {
    }
}