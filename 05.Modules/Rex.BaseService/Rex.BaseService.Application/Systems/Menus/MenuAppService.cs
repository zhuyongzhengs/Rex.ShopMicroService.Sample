using Microsoft.AspNetCore.Authorization;
using Rex.BaseService.Menus;
using Rex.BaseService.Systems.RoleMenus;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Permission.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Rex.BaseService.Systems.Menus
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    [RemoteService]
    [Authorize(BaseServicePermissions.Menus.Default)]
    public class MenuAppService : CrudAppService<Menu, MenuDto, Guid, PagedAndSortedResultRequestDto, MenuCreateDto, MenuUpdateDto>, IMenuAppService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IDataFilter _dataFilter;
        public IRepository<PermissionGrant, Guid> PermissionGrantRepository { get; set; }
        public IRepository<IdentityRole, Guid> IdentityRoleRepository { get; set; }
        public IRepository<RoleMenu, Guid> RoleMenuRepository { get; set; }

        public MenuAppService(IMenuRepository repository, IDataFilter dataFilter) : base(repository)
        {
            _menuRepository = repository;
            _dataFilter = dataFilter;
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input">创建Dto</param>
        /// <returns></returns>
        [Authorize(BaseServicePermissions.Menus.Create)]
        public override async Task<MenuDto> CreateAsync(MenuCreateDto input)
        {
            return await base.CreateAsync(input);
        }

        /// <summary>
        /// 赋予菜单权限
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <returns></returns>
        [Authorize(BaseServicePermissions.Menus.Create)]
        public async Task AddGrantMenuPermissionsAsync(Guid tenantId)
        {
            // List<string> permissionGrantNames = new List<string>(); // 该租户管理员(角色)权限
            Guid roleId = Guid.Empty; // 该租户管理员角色ID
            using (_dataFilter.Disable<IMultiTenant>())
            {
                // permissionGrantNames = (await PermissionGrantRepository.GetListAsync(p => p.TenantId == tenantId && p.ProviderName.Equals(RolePermissionValueProvider.ProviderName) && p.ProviderKey.Equals("admin"))).Select(p => p.Name).ToList();
                IdentityRole identityRole = await IdentityRoleRepository.GetAsync(p => p.TenantId == tenantId && p.IsStatic && p.Name.Equals("admin"));
                if (identityRole != null)
                {
                    roleId = identityRole.Id;
                }
            }

            #region 保存菜单信息

            // 获取菜单信息（不包含租户|菜单）
            List<Menu> menuList = await _menuRepository.GetListAsync(p => p.TenantId != tenantId && !p.PermissionIdentifying.Contains(TenantManagementPermissions.Tenants.Default) && !p.PermissionIdentifying.Contains(BaseServicePermissions.Menus.Default) && !p.Component.Equals("tenant/index") && !p.Component.Equals("system/menu/index"));
            List<MenuDto> menuDtoList = ObjectMapper.Map<List<Menu>, List<MenuDto>>(menuList);

            // 创建新的菜单
            List<Guid> menuIds = menuList.Select(p => p.Id).ToList();
            if (menuDtoList.Count > 0)
            {
                string menuJson = JsonSerializer.Serialize(menuDtoList, options: AspNetCoreExtension.GetJsonSerializerOptions());
                foreach (var menuId in menuIds)
                {
                    string id = GuidGenerator.Create().ToString();
                    string mId = menuId.ToString();
                    menuJson = Regex.Replace(menuJson, mId, id);
                }
                List<MenuDto> newMenuList = JsonSerializer.Deserialize<List<MenuDto>>(menuJson);

                List<Menu> addMenuList = ObjectMapper.Map<List<MenuDto>, List<Menu>>(newMenuList);
                List<RoleMenu> createRoleMenus = new List<RoleMenu>();
                foreach (var aMenu in addMenuList)
                {
                    aMenu.TenantId = tenantId;
                    createRoleMenus.Add(new RoleMenu()
                    {
                        TenantId = tenantId,
                        RoleId = roleId,
                        MenuId = aMenu.Id
                    });
                }

                using (_dataFilter.Disable<IMultiTenant>()) // ISoftDelete
                {
                    await _menuRepository.DeleteAsync(p => p.TenantId == tenantId);
                    await RoleMenuRepository.DeleteAsync(p => p.TenantId == tenantId);
                }

                // 保存菜单信息
                await _menuRepository.InsertManyAsync(addMenuList);

                // 关联角色菜单
                await RoleMenuRepository.InsertManyAsync(createRoleMenus);

                await CurrentUnitOfWork.SaveChangesAsync();
            }

            #endregion 保存菜单信息
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="input">修改Dto</param>
        /// <returns></returns>
        [Authorize(BaseServicePermissions.Menus.Update)]
        public override async Task<MenuDto> UpdateAsync(Guid id, MenuUpdateDto input)
        {
            return await base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Authorize(BaseServicePermissions.Menus.Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        /// <summary>
        /// 获取(所有)菜单信息
        /// </summary>
        /// <param name="input">菜单查询条件</param>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetFilterAsync(GetMenuInput input)
        {
            var queryMenu = (await _menuRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), p => p.Name.Contains(input.Filter));
            List<Menu> menuList = queryMenu.OrderBy(input.Sorting ?? "MenuSort").ToList();
            List<MenuDto> menuDtoList = ObjectMapper.Map<List<Menu>, List<MenuDto>>(menuList);
            return menuDtoList;
        }

        /// <summary>
        /// 获取树形菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuTreeDto>> GetTreeAsync()
        {
            if (CurrentUser.Roles.Length < 1)
            {
                return new List<MenuTreeDto>();
            }

            List<Menu> menuList = (await _menuRepository.GetListAsync(p => p.MenuType == MenuType.Menu)).OrderBy(p => p.MenuSort).ToList();
            List<Menu> menuRoot = menuList.Where(p => p.PId == null).OrderBy(p => p.MenuSort).ToList();
            return LoadRoleMenusTree(menuRoot, menuList);
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