using Microsoft.Extensions.DependencyInjection;
using Rex.BaseService.EntityFrameworkCore;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.BaseService.Systems.UserWeChats
{
    /// <summary>
    /// 微信用户仓储
    /// </summary>
    public class UserWeChatRepository : EfCoreRepository<BaseServiceDbContext, UserWeChat, Guid>, IUserWeChatRepository
    {
        public BaseServiceDbContext bServiceDbContext { get; set; }

        public UserWeChatRepository(IDbContextProvider<BaseServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}