using Microsoft.Extensions.DependencyInjection;
using Rex.BaseService.EntityFrameworkCore;
using Rex.BaseService.Systems.RoleMenus;
using Rex.BaseService.Systems.SysUsers;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.BaseService.Systems.Menus
{
    /// <summary>
    /// 菜单角色仓储
    /// </summary>
    public class RoleMenuRepository : EfCoreRepository<BaseServiceDbContext, RoleMenu, Guid>, IRoleMenuRepository
    {
        public RoleMenuRepository(IDbContextProvider<BaseServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}