using Microsoft.Extensions.DependencyInjection;
using Rex.BaseService.EntityFrameworkCore;
using Rex.BaseService.Systems.UserPointLogs;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 用户积分记录仓储
    /// </summary>
    public class UserPointLogRepository : EfCoreRepository<BaseServiceDbContext, UserPointLog, Guid>, IUserPointLogRepository
    {
        public BaseServiceDbContext bServiceDbContext { get; set; }

        public UserPointLogRepository(IDbContextProvider<BaseServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}