using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.ServiceGoods
{
    /// <summary>
    /// 服务商品仓储接口
    /// </summary>
    public interface IServiceGoodRepository : IRepository<ServiceGood, Guid>
    {
    }
}