using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.RoleMenus
{
    /// <summary>
    /// 菜单角色服务接口
    /// </summary>
    public interface IRoleMenuAppService : ICrudAppService<RoleMenuDto, Guid, PagedAndSortedResultRequestDto, RoleMenuCreateDto, RoleMenuUpdateDto>
    {
        /// <summary>
        /// 获取当前(角色)树形菜单
        /// </summary>
        /// <returns></returns>
        public Task<List<MenuTreeDto>> GetTreeAsync();

        /// <summary>
        /// 批量修改角色菜单
        /// </summary>
        /// <param name="input">批量修改Dto</param>
        /// <returns></returns>
        public Task UpdateManyAsync(ManyRoleMenuUpdateDto input);

        /// <summary>
        /// 根据角色ID获取角色菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public Task<List<RoleMenuDto>> GetRoleIdAsync(Guid roleId);
    }
}