using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.BaseService.Systems.RoleMenus
{
    /// <summary>
    /// 菜单角色仓储接口
    /// </summary>
    public interface IRoleMenuRepository : IRepository<RoleMenu, Guid>
    {
    }
}