using Microsoft.Extensions.DependencyInjection;
using Rex.BaseService.EntityFrameworkCore;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.BaseService.Systems.Menus
{
    /// <summary>
    /// 菜单仓储
    /// </summary>
    public class MenuRepository : EfCoreRepository<BaseServiceDbContext, Menu, Guid>, IMenuRepository
    {
        public BaseServiceDbContext bServiceDbContext { get; set; }

        public MenuRepository(IDbContextProvider<BaseServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}