using Microsoft.AspNetCore.Authorization;
using Rex.BaseService.Menus;
using Rex.BaseService.Systems.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.RoleMenus
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class RoleMenuAppService : CrudAppService<RoleMenu, RoleMenuDto, Guid, PagedAndSortedResultRequestDto, RoleMenuCreateDto, RoleMenuUpdateDto>, IRoleMenuAppService
    {
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly IMenuRepository _menuRepository;

        public IRepository<IdentityRole, Guid> IdentityRoleRepository { get; set; }

        public RoleMenuAppService(IRoleMenuRepository roleMenuRepository, IMenuRepository menuRepository) : base(roleMenuRepository)
        {
            _roleMenuRepository = roleMenuRepository;
            _menuRepository = menuRepository;
        }

        /// <summary>
        /// 创建角色菜单
        /// </summary>
        /// <param name="input">创建Dto</param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Roles.ManagePermissions)]
        public override Task<RoleMenuDto> CreateAsync(RoleMenuCreateDto input)
        {
            return base.CreateAsync(input);
        }

        /// <summary>
        /// 修改角色菜单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="input">修改Dto</param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Roles.ManagePermissions)]
        public override Task<RoleMenuDto> UpdateAsync(Guid id, RoleMenuUpdateDto input)
        {
            return base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除角色菜单
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Roles.ManagePermissions)]
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        /// <summary>
        /// 批量修改角色菜单
        /// </summary>
        /// <param name="input">批量修改Dto</param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Roles.ManagePermissions)]
        public async Task UpdateManyAsync(ManyRoleMenuUpdateDto input)
        {
            var roleMenus = new List<RoleMenu>();
            foreach (var menuId in input.MenuIds)
            {
                RoleMenu roleMenu = new RoleMenu();
                roleMenu.RoleId = input.RoleId;
                roleMenu.MenuId = menuId;
                roleMenus.Add(roleMenu);
            }
            await _roleMenuRepository.DeleteAsync(r => r.RoleId == input.RoleId);
            if (roleMenus.Count > 0)
            {
                await _roleMenuRepository.InsertManyAsync(roleMenus);
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 获取当前(角色)树形菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuTreeDto>> GetTreeAsync()
        {
            if (CurrentUser.Roles.Length < 1)
            {
                return new List<MenuTreeDto>();
            }

            // 先得到用户角色
            List<Guid> roleIds = (await IdentityRoleRepository.GetQueryableAsync()).Where(p => CurrentUser.Roles.Contains(p.Name)).Select(r => r.Id).ToList();

            // 获取(树形)菜单信息
            List<Guid> menuIds = (await _roleMenuRepository.GetQueryableAsync()).Where(p => roleIds.Contains(p.RoleId)).Select(m => m.MenuId).ToList();
            List<Menu> menuList = (await _menuRepository.GetListAsync(p => p.MenuType == MenuType.Menu && menuIds.Contains(p.Id))).OrderBy(p => p.MenuSort).ToList();
            List<Menu> menuRoot = menuList.Where(p => p.PId == null).OrderBy(p => p.MenuSort).ToList();
            return LoadRoleMenusTree(menuRoot, menuList);
        }

        /// <summary>
        /// 根据角色ID获取角色菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public async Task<List<RoleMenuDto>> GetRoleIdAsync(Guid roleId)
        {
            List<RoleMenu> roleMenus = await _roleMenuRepository.GetListAsync(p => p.RoleId == roleId);
            return ObjectMapper.Map<List<RoleMenu>, List<RoleMenuDto>>(roleMenus);
        }

        /// <summary>
        /// 加载树形菜单
        /// </summary>
        /// <param name="roots">(根)菜单</param>
        /// <param name="menus">菜单</param>
        /// <returns></returns>
        private List<MenuTreeDto> LoadRoleMenusTree(List<Menu> menuRoot, List<Menu> menuList)
        {
            List<MenuTreeDto> resultMenu = new List<MenuTreeDto>();
            foreach (var mRoot in menuRoot)
            {
                MenuTreeDto menuTree = ObjectMapper.Map<Menu, MenuTreeDto>(mRoot);
                //if (!string.IsNullOrEmpty(mRoot.MetaInfo)) menuTree.Meta = JsonSerializer.Deserialize<MenuMeta>(mRoot.MetaInfo);
                if (menuList.Where(p => p.PId == mRoot.Id).Any())
                {
                    menuTree.Children = LoadRoleMenusTree(menuList.Where(m => m.PId == mRoot.Id).OrderBy(o => o.MenuSort).ToList(), menuList);
                }
                resultMenu.Add(menuTree);
            }
            return resultMenu;
        }
    }
}