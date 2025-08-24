using Rex.BaseService.Systems.RoleMenus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.Menus
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface IMenuAppService : ICrudAppService<MenuDto, Guid, PagedAndSortedResultRequestDto, MenuCreateDto, MenuUpdateDto>
    {
        /// <summary>
        /// 获取(所有)菜单信息
        /// </summary>
        /// <param name="input">菜单查询条件</param>
        /// <returns></returns>
        public Task<List<MenuDto>> GetFilterAsync(GetMenuInput input);

        /// <summary>
        /// 赋予菜单权限
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <returns></returns>
        public Task AddGrantMenuPermissionsAsync(Guid tenantId);

        /// <summary>
        /// 获取树形菜单
        /// </summary>
        /// <returns></returns>
        public Task<List<MenuTreeDto>> GetTreeAsync();
    }
}