using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品参数仓储接口
    /// </summary>
    public interface IGoodParamRepository : IRepository<GoodParam, Guid>
    {
    }
}