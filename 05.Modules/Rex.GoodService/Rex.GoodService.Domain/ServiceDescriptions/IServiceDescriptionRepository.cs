using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.ServiceDescriptions
{
    /// <summary>
    /// 商城服务描述仓储接口
    /// </summary>
    public interface IServiceDescriptionRepository : IRepository<ServiceDescription, Guid>
    {
    }
}