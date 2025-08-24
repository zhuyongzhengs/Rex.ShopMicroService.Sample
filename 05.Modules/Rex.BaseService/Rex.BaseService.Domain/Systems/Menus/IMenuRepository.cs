using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.BaseService.Systems.Menus
{
    /// <summary>
    /// 菜单仓储接口
    /// </summary>
    public interface IMenuRepository : IRepository<Menu, Guid>
    {
    }
}