using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Rex.BaseService.Menus;
using Rex.BaseService.MultiTenancy;
using Rex.BaseService.Systems.Menus;
using Rex.BaseService.Systems.RoleMenus;
using Rex.BaseService.Systems.UserGrades;
using Rex.Service.Core.Models;
using Rex.Service.Permission.BaseServices;
using Rex.Service.Permission.GoodServices;
using Rex.Service.Permission.OrderServices;
using Rex.Service.Permission.PaymentServices;
using Rex.Service.Permission.PromotionServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Rex.BaseService.Data;

public class BaseServiceDbMigrationService : ITransientDependency
{
    public ILogger<BaseServiceDbMigrationService> Logger { get; set; }
    public IPermissionManager PermissionManager { get; set; }
    public IRepository<RoleMenu, Guid> RoleMenuRepository { get; set; }
    public IRepository<Menu, Guid> MenuRepository { get; set; }
    public IRepository<IdentityRole, Guid> RoleRepository { get; set; }
    public IRepository<UserGrade, Guid> UserGradeRepository { get; set; }

    private readonly IDataSeeder _dataSeeder;
    private readonly IConfiguration _configuration;
    private readonly IEnumerable<IBaseServiceDbSchemaMigrator> _dbSchemaMigrators;
    private readonly ITenantRepository _tenantRepository;
    private readonly ICurrentTenant _currentTenant;
    private readonly IGuidGenerator _guidGenerator;

    public BaseServiceDbMigrationService(
        IDataSeeder dataSeeder,
        IConfiguration configuration,
        IEnumerable<IBaseServiceDbSchemaMigrator> dbSchemaMigrators,
        ITenantRepository tenantRepository,
        ICurrentTenant currentTenant,
        IGuidGenerator guidGenerator)
    {
        _dataSeeder = dataSeeder;
        _configuration = configuration;
        _dbSchemaMigrators = dbSchemaMigrators;
        _tenantRepository = tenantRepository;
        _currentTenant = currentTenant;
        _guidGenerator = guidGenerator;
        Logger = NullLogger<BaseServiceDbMigrationService>.Instance;
    }

    public async Task MigrateAsync()
    {
        var initialMigrationAdded = AddInitialMigrationIfNotExist();

        if (initialMigrationAdded)
        {
            return;
        }

        Logger.LogInformation("开始进行数据库迁移操作...");

        await MigrateDatabaseSchemaAsync();
        await SeedDataAsync();
        await InitializationAdminPermissionAsync();
        await InitializationMenuAsync();
        await InitializationUserGradeAsync();

        Logger.LogInformation($"成功完成了数据库迁移工作。");

        if (MultiTenancyConsts.IsEnabled)
        {
            var tenants = await _tenantRepository.GetListAsync(includeDetails: true);

            var migratedDatabaseSchemas = new HashSet<string>();
            foreach (var tenant in tenants)
            {
                using (_currentTenant.Change(tenant.Id))
                {
                    if (tenant.ConnectionStrings.Any())
                    {
                        var tenantConnectionStrings = tenant.ConnectionStrings
                            .Select(x => x.Value)
                            .ToList();

                        if (!migratedDatabaseSchemas.IsSupersetOf(tenantConnectionStrings))
                        {
                            await MigrateDatabaseSchemaAsync(tenant);

                            migratedDatabaseSchemas.AddIfNotContains(tenantConnectionStrings);
                        }
                    }

                    await SeedDataAsync(tenant);
                }
                Logger.LogInformation($"成功完成了 {tenant.Name} 租户数据库迁移工作。");
            }
        }

        Logger.LogInformation("成功完成了所有数据库迁移工作。");
        Logger.LogInformation("您可以放心地结束这个程序了...");
    }

    private async Task MigrateDatabaseSchemaAsync(Tenant? tenant = null)
    {
        Logger.LogInformation(
            $"针对租户【{(tenant == null ? "host" : tenant.Name)}】的数据库迁移方案...");

        foreach (var migrator in _dbSchemaMigrators)
        {
            await migrator.MigrateAsync();
        }
    }

    private async Task SeedDataAsync(Tenant? tenant = null)
    {
        Logger.LogInformation($"正在初始化租户【{(tenant == null ? "host" : tenant.Name)}】管理员账号...");

        await _dataSeeder.SeedAsync(new DataSeedContext(tenant?.Id)
            .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName, IdentityDataSeedContributor.AdminEmailDefaultValue)
            .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName, IdentityDataSeedContributor.AdminPasswordDefaultValue)
        );

        Logger.LogInformation($"租户【{(tenant == null ? "host" : tenant.Name)}】管理员账号初始化完成，登录账号：{IdentityDataSeedContributor.AdminUserNameDefaultValue}、密码：{IdentityDataSeedContributor.AdminPasswordDefaultValue}。");
    }

    /// <summary>
    /// 初始化管理员角色权限
    /// </summary>
    /// <returns></returns>
    private async Task InitializationAdminPermissionAsync()
    {
        Logger.LogInformation($"正在初始化[管理员]角色权限...");

        List<string> adminPermissions = new List<string>();
        adminPermissions.AddRange(GetLeafPermissionValues(typeof(BaseServicePermissions)));
        adminPermissions.AddRange(GetLeafPermissionValues(typeof(GoodServicePermissions)));
        adminPermissions.AddRange(GetLeafPermissionValues(typeof(OrderServicePermissions)));
        adminPermissions.AddRange(GetLeafPermissionValues(typeof(PaymentServicePermissions)));
        adminPermissions.AddRange(GetLeafPermissionValues(typeof(PromotionServicePermissions)));
        foreach (var pName in adminPermissions)
        {
            await PermissionManager.SetAsync(pName, RolePermissionValueProvider.ProviderName, IdentityDataSeedContributor.AdminUserNameDefaultValue, true);
        }

        Logger.LogInformation($"[管理员]角色权限初始化完成。");
    }

    /// <summary>
    /// 递归获取权限类中的所有常量值
    /// </summary>
    /// <param name="type">权限类的 Type</param>
    /// <returns>权限字符串列表</returns>
    private List<string> GetLeafPermissionValues(Type type)
    {
        List<string> values = new List<string>();

        // 1. 获取当前类中定义的所有嵌套类
        var nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.Static);

        if (nestedTypes.Length > 0)
        {
            // 如果存在嵌套类，根据你的逻辑：不获取当前类的属性，继续往子类找
            foreach (var nestedType in nestedTypes)
            {
                values.AddRange(GetLeafPermissionValues(nestedType));
            }
        }
        else
        {
            // 2. 如果没有嵌套类了（到达了最底层），获取该类中的所有常量（Fields）
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                             .Where(f => f.IsLiteral && !f.IsInitOnly); // 过滤出 const 常量

            foreach (var field in fields)
            {
                var value = field.GetValue(null)?.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    values.Add(value);
                }
            }
        }

        return values;
    }

    /// <summary>
    /// 初始化用户等级
    /// </summary>
    /// <param name="tenant"></param>
    /// <returns></returns>
    private async Task InitializationUserGradeAsync(Tenant? tenant = null)
    {
        long userGradeRepositoryCount = await UserGradeRepository.GetCountAsync();
        if (userGradeRepositoryCount > 0) return; // 可根据自己的需求调整

        Logger.LogInformation($"正在初始化租户【{(tenant == null ? "host" : tenant.Name)}】用户等级...");
        List<UserGrade> userGradeList = new List<UserGrade>();
        UserGrade sVipGrade = new UserGrade(_guidGenerator.Create());
        sVipGrade.Title = "豪华SVIP";
        sVipGrade.IsDefault = false;
        userGradeList.Add(sVipGrade);

        UserGrade vipGrade = new UserGrade(_guidGenerator.Create());
        vipGrade.Title = "尊享VIP";
        vipGrade.IsDefault = false;
        userGradeList.Add(vipGrade);

        UserGrade normalGrade = new UserGrade(_guidGenerator.Create());
        normalGrade.Title = "普通用户";
        normalGrade.IsDefault = true;
        userGradeList.Add(normalGrade);

        await UserGradeRepository.InsertManyAsync(userGradeList);
        Logger.LogInformation($"租户【{(tenant == null ? "host" : tenant.Name)}】用户等级初始化完成。");
    }

    /// <summary>
    /// 初始化菜单
    /// </summary>
    /// <param name="tenant"></param>
    /// <returns></returns>
    private async Task InitializationMenuAsync(Tenant? tenant = null)
    {
        long menuCount = await MenuRepository.GetCountAsync();
        if (menuCount > 0) return; // 可根据自己的需求调整

        // 关联管理员菜单角色
        IdentityRole identityRole = await RoleRepository.FindAsync(r => r.Name == IdentityDataSeedContributor.AdminUserNameDefaultValue);
        if (identityRole == null) return;

        Logger.LogInformation($"正在初始化租户【{(tenant == null ? "host" : tenant.Name)}】功能菜单...");

        List<Menu> menuList = new List<Menu>();

        #region 首页

        Menu homeMenu = new Menu(_guidGenerator.Create());
        homeMenu.TenantId = tenant?.Id;
        homeMenu.MenuType = MenuType.Menu;
        homeMenu.Name = "home";
        homeMenu.Component = "home/index";
        homeMenu.ComponentAlias = string.Empty;
        homeMenu.IsLink = false;
        homeMenu.MenuSort = 0;
        homeMenu.Path = "/home";
        homeMenu.Redirect = string.Empty;
        homeMenu.PermissionIdentifying = BaseServicePermissions.Homes.Default;
        homeMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.home",
            Icon = "ele-House",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = true,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(homeMenu);

        #endregion 首页

        #region 系统管理

        Menu systemMenu = new Menu(_guidGenerator.Create());
        systemMenu.TenantId = tenant?.Id;
        systemMenu.MenuType = MenuType.Menu;
        systemMenu.Name = "system";
        systemMenu.Component = "layout/routerView/parent";
        systemMenu.ComponentAlias = string.Empty;
        systemMenu.IsLink = false;
        systemMenu.MenuSort = 1;
        systemMenu.Path = "/system";
        systemMenu.Redirect = "/system/user";
        systemMenu.PermissionIdentifying = string.Empty;
        systemMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.system",
            Icon = "ele-Setting",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(systemMenu);

        #region 组织单元

        Menu souMenu = new Menu(_guidGenerator.Create());
        souMenu.TenantId = tenant?.Id;
        souMenu.PId = systemMenu.Id;
        souMenu.MenuType = MenuType.Menu;
        souMenu.Name = "systemOrganizationUnit";
        souMenu.Component = "system/organizationUnit/index";
        souMenu.ComponentAlias = string.Empty;
        souMenu.IsLink = false;
        souMenu.MenuSort = 1;
        souMenu.Path = "/system/organizationUnit";
        souMenu.Redirect = string.Empty;
        souMenu.PermissionIdentifying = string.Empty;
        souMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemOrganizationUnit",
            Icon = "iconfont icon-zuzhijigou",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(souMenu);

        #region 查询

        Menu souSearchMenu = new Menu(_guidGenerator.Create());
        souSearchMenu.TenantId = tenant?.Id;
        souSearchMenu.PId = souMenu.Id;
        souSearchMenu.MenuType = MenuType.Button;
        souSearchMenu.Name = string.Empty;
        souSearchMenu.Component = string.Empty;
        souSearchMenu.ComponentAlias = string.Empty;
        souSearchMenu.IsLink = false;
        souSearchMenu.MenuSort = 1;
        souSearchMenu.Path = string.Empty;
        souSearchMenu.Redirect = string.Empty;
        souSearchMenu.PermissionIdentifying = BaseServicePermissions.OrganizationUnits.Default;
        souSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(souSearchMenu);

        #endregion 查询

        #region 新增

        Menu souCreateMenu = new Menu(_guidGenerator.Create());
        souCreateMenu.TenantId = tenant?.Id;
        souCreateMenu.PId = souMenu.Id;
        souCreateMenu.MenuType = MenuType.Button;
        souCreateMenu.Name = string.Empty;
        souCreateMenu.Component = string.Empty;
        souCreateMenu.ComponentAlias = string.Empty;
        souCreateMenu.IsLink = false;
        souCreateMenu.MenuSort = 2;
        souCreateMenu.Path = string.Empty;
        souCreateMenu.Redirect = string.Empty;
        souCreateMenu.PermissionIdentifying = BaseServicePermissions.OrganizationUnits.Create;
        souCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(souCreateMenu);

        #endregion 新增

        #region 修改

        Menu souUpdateMenu = new Menu(_guidGenerator.Create());
        souUpdateMenu.TenantId = tenant?.Id;
        souUpdateMenu.PId = souMenu.Id;
        souUpdateMenu.MenuType = MenuType.Button;
        souUpdateMenu.Name = string.Empty;
        souUpdateMenu.Component = string.Empty;
        souUpdateMenu.ComponentAlias = string.Empty;
        souUpdateMenu.IsLink = false;
        souUpdateMenu.MenuSort = 3;
        souUpdateMenu.Path = string.Empty;
        souUpdateMenu.Redirect = string.Empty;
        souUpdateMenu.PermissionIdentifying = BaseServicePermissions.OrganizationUnits.Update;
        souUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(souUpdateMenu);

        #endregion 修改

        #region 删除

        Menu souDeleteMenu = new Menu(_guidGenerator.Create());
        souDeleteMenu.TenantId = tenant?.Id;
        souDeleteMenu.PId = souMenu.Id;
        souDeleteMenu.MenuType = MenuType.Button;
        souDeleteMenu.Name = string.Empty;
        souDeleteMenu.Component = string.Empty;
        souDeleteMenu.ComponentAlias = string.Empty;
        souDeleteMenu.IsLink = false;
        souDeleteMenu.MenuSort = 4;
        souDeleteMenu.Path = string.Empty;
        souDeleteMenu.Redirect = string.Empty;
        souDeleteMenu.PermissionIdentifying = BaseServicePermissions.OrganizationUnits.Delete;
        souDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(souDeleteMenu);

        #endregion 删除

        #region 管理组织结构用户

        Menu souMuMenu = new Menu(_guidGenerator.Create());
        souMuMenu.TenantId = tenant?.Id;
        souMuMenu.PId = souMenu.Id;
        souMuMenu.MenuType = MenuType.Button;
        souMuMenu.Name = string.Empty;
        souMuMenu.Component = string.Empty;
        souMuMenu.ComponentAlias = string.Empty;
        souMuMenu.IsLink = false;
        souMuMenu.MenuSort = 5;
        souMuMenu.Path = string.Empty;
        souMuMenu.Redirect = string.Empty;
        souMuMenu.PermissionIdentifying = BaseServicePermissions.OrganizationUnits.ManagingUser;
        souMuMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemOrganizationUnitManagingUser",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(souMuMenu);

        #endregion 管理组织结构用户

        #region 管理组织结构角色

        Menu souMrMenu = new Menu(_guidGenerator.Create());
        souMrMenu.TenantId = tenant?.Id;
        souMrMenu.PId = souMenu.Id;
        souMrMenu.MenuType = MenuType.Button;
        souMrMenu.Name = string.Empty;
        souMrMenu.Component = string.Empty;
        souMrMenu.ComponentAlias = string.Empty;
        souMrMenu.IsLink = false;
        souMrMenu.MenuSort = 6;
        souMrMenu.Path = string.Empty;
        souMrMenu.Redirect = string.Empty;
        souMrMenu.PermissionIdentifying = BaseServicePermissions.OrganizationUnits.ManagingRole;
        souMrMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemOrganizationUnitManagingRole",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(souMrMenu);

        #endregion 管理组织结构角色

        #endregion 组织单元

        #region 用户管理

        Menu userMenu = new Menu(_guidGenerator.Create());
        userMenu.TenantId = tenant?.Id;
        userMenu.PId = systemMenu.Id;
        userMenu.MenuType = MenuType.Menu;
        userMenu.Name = "systemUser";
        userMenu.Component = "system/user/index";
        userMenu.ComponentAlias = string.Empty;
        userMenu.IsLink = false;
        userMenu.MenuSort = 2;
        userMenu.Path = "/system/user";
        userMenu.Redirect = string.Empty;
        userMenu.PermissionIdentifying = string.Empty;
        userMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUser",
            Icon = "iconfont icon-yonghu",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(userMenu);

        #region 管理权限

        Menu umMenu = new Menu(_guidGenerator.Create());
        umMenu.TenantId = tenant?.Id;
        umMenu.PId = userMenu.Id;
        umMenu.MenuType = MenuType.Button;
        umMenu.Name = string.Empty;
        umMenu.Component = string.Empty;
        umMenu.ComponentAlias = string.Empty;
        umMenu.IsLink = false;
        umMenu.MenuSort = 0;
        umMenu.Path = string.Empty;
        umMenu.Redirect = string.Empty;
        umMenu.PermissionIdentifying = IdentityPermissions.Users.ManagePermissions;
        umMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemManagePermissions",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(umMenu);

        #endregion 管理权限

        #region 查询

        Menu uSearchMenu = new Menu(_guidGenerator.Create());
        uSearchMenu.TenantId = tenant?.Id;
        uSearchMenu.PId = userMenu.Id;
        uSearchMenu.MenuType = MenuType.Button;
        uSearchMenu.Name = string.Empty;
        uSearchMenu.Component = string.Empty;
        uSearchMenu.ComponentAlias = string.Empty;
        uSearchMenu.IsLink = false;
        uSearchMenu.MenuSort = 1;
        uSearchMenu.Path = string.Empty;
        uSearchMenu.Redirect = string.Empty;
        uSearchMenu.PermissionIdentifying = IdentityPermissions.Users.Default;
        uSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(uSearchMenu);

        #endregion 查询

        #region 新增

        Menu uCreateMenu = new Menu(_guidGenerator.Create());
        uCreateMenu.TenantId = tenant?.Id;
        uCreateMenu.PId = userMenu.Id;
        uCreateMenu.MenuType = MenuType.Button;
        uCreateMenu.Name = string.Empty;
        uCreateMenu.Component = string.Empty;
        uCreateMenu.ComponentAlias = string.Empty;
        uCreateMenu.IsLink = false;
        uCreateMenu.MenuSort = 2;
        uCreateMenu.Path = string.Empty;
        uCreateMenu.Redirect = string.Empty;
        uCreateMenu.PermissionIdentifying = IdentityPermissions.Users.Create;
        uCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(uCreateMenu);

        #endregion 新增

        #region 修改

        Menu uUpdateMenu = new Menu(_guidGenerator.Create());
        uUpdateMenu.TenantId = tenant?.Id;
        uUpdateMenu.PId = userMenu.Id;
        uUpdateMenu.MenuType = MenuType.Button;
        uUpdateMenu.Name = string.Empty;
        uUpdateMenu.Component = string.Empty;
        uUpdateMenu.ComponentAlias = string.Empty;
        uUpdateMenu.IsLink = false;
        uUpdateMenu.MenuSort = 3;
        uUpdateMenu.Path = string.Empty;
        uUpdateMenu.Redirect = string.Empty;
        uUpdateMenu.PermissionIdentifying = IdentityPermissions.Users.Update;
        uUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(uUpdateMenu);

        #endregion 修改

        #region 删除

        Menu uDeleteMenu = new Menu(_guidGenerator.Create());
        uDeleteMenu.TenantId = tenant?.Id;
        uDeleteMenu.PId = userMenu.Id;
        uDeleteMenu.MenuType = MenuType.Button;
        uDeleteMenu.Name = string.Empty;
        uDeleteMenu.Component = string.Empty;
        uDeleteMenu.ComponentAlias = string.Empty;
        uDeleteMenu.IsLink = false;
        uDeleteMenu.MenuSort = 4;
        uDeleteMenu.Path = string.Empty;
        uDeleteMenu.Redirect = string.Empty;
        uDeleteMenu.PermissionIdentifying = IdentityPermissions.Users.Delete;
        uDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(uDeleteMenu);

        #endregion 删除

        #endregion 用户管理

        #region 角色管理

        Menu roleMenu = new Menu(_guidGenerator.Create());
        roleMenu.TenantId = tenant?.Id;
        roleMenu.PId = systemMenu.Id;
        roleMenu.MenuType = MenuType.Menu;
        roleMenu.Name = "systemRole";
        roleMenu.Component = "system/role/index";
        roleMenu.ComponentAlias = string.Empty;
        roleMenu.IsLink = false;
        roleMenu.MenuSort = 3;
        roleMenu.Path = "/system/role";
        roleMenu.Redirect = string.Empty;
        roleMenu.PermissionIdentifying = string.Empty;
        roleMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemRole",
            Icon = "iconfont icon-quanxianziyuan",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(roleMenu);

        #region 管理权限

        Menu rmMenu = new Menu(_guidGenerator.Create());
        rmMenu.TenantId = tenant?.Id;
        rmMenu.PId = roleMenu.Id;
        rmMenu.MenuType = MenuType.Button;
        rmMenu.Name = string.Empty;
        rmMenu.Component = string.Empty;
        rmMenu.ComponentAlias = string.Empty;
        rmMenu.IsLink = false;
        rmMenu.MenuSort = 0;
        rmMenu.Path = string.Empty;
        rmMenu.Redirect = string.Empty;
        rmMenu.PermissionIdentifying = IdentityPermissions.Roles.ManagePermissions;
        rmMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemManagePermissions",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(rmMenu);

        #endregion 管理权限

        #region 查询

        Menu rSearchMenu = new Menu(_guidGenerator.Create());
        rSearchMenu.TenantId = tenant?.Id;
        rSearchMenu.PId = roleMenu.Id;
        rSearchMenu.MenuType = MenuType.Button;
        rSearchMenu.Name = string.Empty;
        rSearchMenu.Component = string.Empty;
        rSearchMenu.ComponentAlias = string.Empty;
        rSearchMenu.IsLink = false;
        rSearchMenu.MenuSort = 1;
        rSearchMenu.Path = string.Empty;
        rSearchMenu.Redirect = string.Empty;
        rSearchMenu.PermissionIdentifying = IdentityPermissions.Roles.Default;
        rSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(rSearchMenu);

        #endregion 查询

        #region 新增

        Menu rCreateMenu = new Menu(_guidGenerator.Create());
        rCreateMenu.TenantId = tenant?.Id;
        rCreateMenu.PId = roleMenu.Id;
        rCreateMenu.MenuType = MenuType.Button;
        rCreateMenu.Name = string.Empty;
        rCreateMenu.Component = string.Empty;
        rCreateMenu.ComponentAlias = string.Empty;
        rCreateMenu.IsLink = false;
        rCreateMenu.MenuSort = 2;
        rCreateMenu.Path = string.Empty;
        rCreateMenu.Redirect = string.Empty;
        rCreateMenu.PermissionIdentifying = IdentityPermissions.Roles.Create;
        rCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(rCreateMenu);

        #endregion 新增

        #region 修改

        Menu rUpdateMenu = new Menu(_guidGenerator.Create());
        rUpdateMenu.TenantId = tenant?.Id;
        rUpdateMenu.PId = roleMenu.Id;
        rUpdateMenu.MenuType = MenuType.Button;
        rUpdateMenu.Name = string.Empty;
        rUpdateMenu.Component = string.Empty;
        rUpdateMenu.ComponentAlias = string.Empty;
        rUpdateMenu.IsLink = false;
        rUpdateMenu.MenuSort = 3;
        rUpdateMenu.Path = string.Empty;
        rUpdateMenu.Redirect = string.Empty;
        rUpdateMenu.PermissionIdentifying = IdentityPermissions.Roles.Update;
        rUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(rUpdateMenu);

        #endregion 修改

        #region 删除

        Menu rDeleteMenu = new Menu(_guidGenerator.Create());
        rDeleteMenu.TenantId = tenant?.Id;
        rDeleteMenu.PId = roleMenu.Id;
        rDeleteMenu.MenuType = MenuType.Button;
        rDeleteMenu.Name = string.Empty;
        rDeleteMenu.Component = string.Empty;
        rDeleteMenu.ComponentAlias = string.Empty;
        rDeleteMenu.IsLink = false;
        rDeleteMenu.MenuSort = 4;
        rDeleteMenu.Path = string.Empty;
        rDeleteMenu.Redirect = string.Empty;
        rDeleteMenu.PermissionIdentifying = IdentityPermissions.Roles.Delete;
        rDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(rDeleteMenu);

        #endregion 删除

        #endregion 角色管理

        #region 菜单管理

        Menu menu = new Menu(_guidGenerator.Create());
        menu.TenantId = tenant?.Id;
        menu.PId = systemMenu.Id;
        menu.MenuType = MenuType.Menu;
        menu.Name = "systemMenu";
        menu.Component = "system/menu/index";
        menu.ComponentAlias = string.Empty;
        menu.IsLink = false;
        menu.MenuSort = 4;
        menu.Path = "/system/menu";
        menu.Redirect = string.Empty;
        menu.PermissionIdentifying = string.Empty;
        menu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemMenu",
            Icon = "iconfont icon-caidan",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(menu);

        #region 查询

        Menu searchMenu = new Menu(_guidGenerator.Create());
        searchMenu.TenantId = tenant?.Id;
        searchMenu.PId = menu.Id;
        searchMenu.MenuType = MenuType.Button;
        searchMenu.Name = string.Empty;
        searchMenu.Component = string.Empty;
        searchMenu.ComponentAlias = string.Empty;
        searchMenu.IsLink = false;
        searchMenu.MenuSort = 1;
        searchMenu.Path = string.Empty;
        searchMenu.Redirect = string.Empty;
        searchMenu.PermissionIdentifying = BaseServicePermissions.Menus.Default;
        searchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(searchMenu);

        #endregion 查询

        #region 新增

        Menu createMenu = new Menu(_guidGenerator.Create());
        createMenu.TenantId = tenant?.Id;
        createMenu.PId = menu.Id;
        createMenu.MenuType = MenuType.Button;
        createMenu.Name = string.Empty;
        createMenu.Component = string.Empty;
        createMenu.ComponentAlias = string.Empty;
        createMenu.IsLink = false;
        createMenu.MenuSort = 2;
        createMenu.Path = string.Empty;
        createMenu.Redirect = string.Empty;
        createMenu.PermissionIdentifying = BaseServicePermissions.Menus.Create;
        createMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(createMenu);

        #endregion 新增

        #region 修改

        Menu updateMenu = new Menu(_guidGenerator.Create());
        updateMenu.TenantId = tenant?.Id;
        updateMenu.PId = menu.Id;
        updateMenu.MenuType = MenuType.Button;
        updateMenu.Name = string.Empty;
        updateMenu.Component = string.Empty;
        updateMenu.ComponentAlias = string.Empty;
        updateMenu.IsLink = false;
        updateMenu.MenuSort = 3;
        updateMenu.Path = string.Empty;
        updateMenu.Redirect = string.Empty;
        updateMenu.PermissionIdentifying = BaseServicePermissions.Menus.Update;
        updateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(updateMenu);

        #endregion 修改

        #region 删除

        Menu deleteMenu = new Menu(_guidGenerator.Create());
        deleteMenu.TenantId = tenant?.Id;
        deleteMenu.PId = menu.Id;
        deleteMenu.MenuType = MenuType.Button;
        deleteMenu.Name = string.Empty;
        deleteMenu.Component = string.Empty;
        deleteMenu.ComponentAlias = string.Empty;
        deleteMenu.IsLink = false;
        deleteMenu.MenuSort = 4;
        deleteMenu.Path = string.Empty;
        deleteMenu.Redirect = string.Empty;
        deleteMenu.PermissionIdentifying = BaseServicePermissions.Menus.Delete;
        deleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(deleteMenu);

        #endregion 删除

        #endregion 菜单管理

        #endregion 系统管理

        #region 会员管理

        Menu memberMenu = new Menu(_guidGenerator.Create());
        memberMenu.TenantId = tenant?.Id;
        memberMenu.MenuType = MenuType.Menu;
        memberMenu.Name = "member";
        memberMenu.Component = "layout/routerView/parent";
        memberMenu.ComponentAlias = string.Empty;
        memberMenu.IsLink = false;
        memberMenu.MenuSort = 2;
        memberMenu.Path = "/member";
        memberMenu.Redirect = "/member/userGrade";
        memberMenu.PermissionIdentifying = string.Empty;
        memberMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.member",
            Icon = "iconfont icon-Member",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(memberMenu);

        #region 用户等级

        Menu mugMenu = new Menu(_guidGenerator.Create());
        mugMenu.TenantId = tenant?.Id;
        mugMenu.PId = memberMenu.Id;
        mugMenu.MenuType = MenuType.Menu;
        mugMenu.Name = "memberUserGrade";
        mugMenu.Component = "member/userGrade/index";
        mugMenu.ComponentAlias = string.Empty;
        mugMenu.IsLink = false;
        mugMenu.MenuSort = 1;
        mugMenu.Path = "/member/userGrade";
        mugMenu.Redirect = string.Empty;
        mugMenu.PermissionIdentifying = string.Empty;
        mugMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.memberUserGrade",
            Icon = "iconfont icon-huiyuan",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(mugMenu);

        #region 查询

        Menu mugSearchMenu = new Menu(_guidGenerator.Create());
        mugSearchMenu.TenantId = tenant?.Id;
        mugSearchMenu.PId = mugMenu.Id;
        mugSearchMenu.MenuType = MenuType.Button;
        mugSearchMenu.Name = string.Empty;
        mugSearchMenu.Component = string.Empty;
        mugSearchMenu.ComponentAlias = string.Empty;
        mugSearchMenu.IsLink = false;
        mugSearchMenu.MenuSort = 1;
        mugSearchMenu.Path = string.Empty;
        mugSearchMenu.Redirect = string.Empty;
        mugSearchMenu.PermissionIdentifying = BaseServicePermissions.UserGrades.Default;
        mugSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(mugSearchMenu);

        #endregion 查询

        #region 新增

        Menu mugCreateMenu = new Menu(_guidGenerator.Create());
        mugCreateMenu.TenantId = tenant?.Id;
        mugCreateMenu.PId = mugMenu.Id;
        mugCreateMenu.MenuType = MenuType.Button;
        mugCreateMenu.Name = string.Empty;
        mugCreateMenu.Component = string.Empty;
        mugCreateMenu.ComponentAlias = string.Empty;
        mugCreateMenu.IsLink = false;
        mugCreateMenu.MenuSort = 2;
        mugCreateMenu.Path = string.Empty;
        mugCreateMenu.Redirect = string.Empty;
        mugCreateMenu.PermissionIdentifying = BaseServicePermissions.UserGrades.Create;
        mugCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(mugCreateMenu);

        #endregion 新增

        #region 修改

        Menu mugUpdateMenu = new Menu(_guidGenerator.Create());
        mugUpdateMenu.TenantId = tenant?.Id;
        mugUpdateMenu.PId = mugMenu.Id;
        mugUpdateMenu.MenuType = MenuType.Button;
        mugUpdateMenu.Name = string.Empty;
        mugUpdateMenu.Component = string.Empty;
        mugUpdateMenu.ComponentAlias = string.Empty;
        mugUpdateMenu.IsLink = false;
        mugUpdateMenu.MenuSort = 3;
        mugUpdateMenu.Path = string.Empty;
        mugUpdateMenu.Redirect = string.Empty;
        mugUpdateMenu.PermissionIdentifying = BaseServicePermissions.UserGrades.Update;
        mugUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(mugUpdateMenu);

        #endregion 修改

        #region 删除

        Menu mugDeleteMenu = new Menu(_guidGenerator.Create());
        mugDeleteMenu.TenantId = tenant?.Id;
        mugDeleteMenu.PId = mugMenu.Id;
        mugDeleteMenu.MenuType = MenuType.Button;
        mugDeleteMenu.Name = string.Empty;
        mugDeleteMenu.Component = string.Empty;
        mugDeleteMenu.ComponentAlias = string.Empty;
        mugDeleteMenu.IsLink = false;
        mugDeleteMenu.MenuSort = 4;
        mugDeleteMenu.Path = string.Empty;
        mugDeleteMenu.Redirect = string.Empty;
        mugDeleteMenu.PermissionIdentifying = BaseServicePermissions.UserGrades.Delete;
        mugDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(mugDeleteMenu);

        #endregion 删除

        #endregion 用户等级

        #region 微信用户管理

        Menu weChatMenu = new Menu(_guidGenerator.Create());
        weChatMenu.TenantId = tenant?.Id;
        weChatMenu.PId = memberMenu.Id;
        weChatMenu.MenuType = MenuType.Menu;
        weChatMenu.Name = "memberUserWeChat";
        weChatMenu.Component = "member/userWeChat/index";
        weChatMenu.ComponentAlias = string.Empty;
        weChatMenu.IsLink = false;
        weChatMenu.MenuSort = 2;
        weChatMenu.Path = "/member/userWeChat";
        weChatMenu.Redirect = string.Empty;
        weChatMenu.PermissionIdentifying = string.Empty;
        weChatMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.memberUserWeChat",
            Icon = "iconfont icon-weixin",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(weChatMenu);

        #region 查询

        Menu wcSearchMenu = new Menu(_guidGenerator.Create());
        wcSearchMenu.TenantId = tenant?.Id;
        wcSearchMenu.PId = weChatMenu.Id;
        wcSearchMenu.MenuType = MenuType.Button;
        wcSearchMenu.Name = string.Empty;
        wcSearchMenu.Component = string.Empty;
        wcSearchMenu.ComponentAlias = string.Empty;
        wcSearchMenu.IsLink = false;
        wcSearchMenu.MenuSort = 1;
        wcSearchMenu.Path = string.Empty;
        wcSearchMenu.Redirect = string.Empty;
        wcSearchMenu.PermissionIdentifying = BaseServicePermissions.UserWeChats.Default;
        wcSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(wcSearchMenu);

        #endregion 查询

        #endregion 微信用户管理

        #region 注册用户管理

        Menu msuMenu = new Menu(_guidGenerator.Create());
        msuMenu.TenantId = tenant?.Id;
        msuMenu.PId = memberMenu.Id;
        msuMenu.MenuType = MenuType.Menu;
        msuMenu.Name = "memberSysUser";
        msuMenu.Component = "member/sysUser/index";
        msuMenu.ComponentAlias = string.Empty;
        msuMenu.IsLink = false;
        msuMenu.MenuSort = 3;
        msuMenu.Path = "/member/sysUser";
        msuMenu.Redirect = string.Empty;
        msuMenu.PermissionIdentifying = string.Empty;
        msuMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.memberSysUser",
            Icon = "iconfont icon-24gl-userGroupPlus",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(msuMenu);

        #region 查询

        Menu msuSearchMenu = new Menu(_guidGenerator.Create());
        msuSearchMenu.TenantId = tenant?.Id;
        msuSearchMenu.PId = msuMenu.Id;
        msuSearchMenu.MenuType = MenuType.Button;
        msuSearchMenu.Name = string.Empty;
        msuSearchMenu.Component = string.Empty;
        msuSearchMenu.ComponentAlias = string.Empty;
        msuSearchMenu.IsLink = false;
        msuSearchMenu.MenuSort = 1;
        msuSearchMenu.Path = string.Empty;
        msuSearchMenu.Redirect = string.Empty;
        msuSearchMenu.PermissionIdentifying = BaseServicePermissions.SysUsers.Default;
        msuSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(msuSearchMenu);

        #endregion 查询

        #region 新增

        Menu msuCreateMenu = new Menu(_guidGenerator.Create());
        msuCreateMenu.TenantId = tenant?.Id;
        msuCreateMenu.PId = msuMenu.Id;
        msuCreateMenu.MenuType = MenuType.Button;
        msuCreateMenu.Name = string.Empty;
        msuCreateMenu.Component = string.Empty;
        msuCreateMenu.ComponentAlias = string.Empty;
        msuCreateMenu.IsLink = false;
        msuCreateMenu.MenuSort = 2;
        msuCreateMenu.Path = string.Empty;
        msuCreateMenu.Redirect = string.Empty;
        msuCreateMenu.PermissionIdentifying = BaseServicePermissions.SysUsers.Create;
        msuCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(msuCreateMenu);

        #endregion 新增

        #region 修改

        Menu msuUpdateMenu = new Menu(_guidGenerator.Create());
        msuUpdateMenu.TenantId = tenant?.Id;
        msuUpdateMenu.PId = msuMenu.Id;
        msuUpdateMenu.MenuType = MenuType.Button;
        msuUpdateMenu.Name = string.Empty;
        msuUpdateMenu.Component = string.Empty;
        msuUpdateMenu.ComponentAlias = string.Empty;
        msuUpdateMenu.IsLink = false;
        msuUpdateMenu.MenuSort = 3;
        msuUpdateMenu.Path = string.Empty;
        msuUpdateMenu.Redirect = string.Empty;
        msuUpdateMenu.PermissionIdentifying = BaseServicePermissions.SysUsers.Update;
        msuUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(msuUpdateMenu);

        #endregion 修改

        #region 删除

        Menu msuDeleteMenu = new Menu(_guidGenerator.Create());
        msuDeleteMenu.TenantId = tenant?.Id;
        msuDeleteMenu.PId = msuMenu.Id;
        msuDeleteMenu.MenuType = MenuType.Button;
        msuDeleteMenu.Name = string.Empty;
        msuDeleteMenu.Component = string.Empty;
        msuDeleteMenu.ComponentAlias = string.Empty;
        msuDeleteMenu.IsLink = false;
        msuDeleteMenu.MenuSort = 4;
        msuDeleteMenu.Path = string.Empty;
        msuDeleteMenu.Redirect = string.Empty;
        msuDeleteMenu.PermissionIdentifying = BaseServicePermissions.SysUsers.Delete;
        msuDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(msuDeleteMenu);

        #endregion 删除

        #endregion 注册用户管理

        #endregion 会员管理

        #region 商品管理

        Menu goodMenu = new Menu(_guidGenerator.Create());
        goodMenu.TenantId = tenant?.Id;
        goodMenu.MenuType = MenuType.Menu;
        goodMenu.Name = "good";
        goodMenu.Component = "layout/routerView/parent";
        goodMenu.ComponentAlias = string.Empty;
        goodMenu.IsLink = false;
        goodMenu.MenuSort = 3;
        goodMenu.Path = "/good";
        goodMenu.Redirect = "/good/goods";
        goodMenu.PermissionIdentifying = string.Empty;
        goodMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.good",
            Icon = "iconfont icon-shangpin",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(goodMenu);

        #region 商品列表

        Menu goodsMenu = new Menu(_guidGenerator.Create());
        goodsMenu.TenantId = tenant?.Id;
        goodsMenu.PId = goodMenu.Id;
        goodsMenu.MenuType = MenuType.Menu;
        goodsMenu.Name = "goods";
        goodsMenu.Component = "good/goods/index";
        goodsMenu.ComponentAlias = string.Empty;
        goodsMenu.IsLink = false;
        goodsMenu.MenuSort = 1;
        goodsMenu.Path = "/good/goods";
        goodsMenu.Redirect = string.Empty;
        goodsMenu.PermissionIdentifying = string.Empty;
        goodsMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.goods",
            Icon = "iconfont icon-shangpinguanli",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(goodsMenu);

        #region 查询

        Menu goodsSearchMenu = new Menu(_guidGenerator.Create());
        goodsSearchMenu.TenantId = tenant?.Id;
        goodsSearchMenu.PId = goodsMenu.Id;
        goodsSearchMenu.MenuType = MenuType.Button;
        goodsSearchMenu.Name = string.Empty;
        goodsSearchMenu.Component = string.Empty;
        goodsSearchMenu.ComponentAlias = string.Empty;
        goodsSearchMenu.IsLink = false;
        goodsSearchMenu.MenuSort = 1;
        goodsSearchMenu.Path = string.Empty;
        goodsSearchMenu.Redirect = string.Empty;
        goodsSearchMenu.PermissionIdentifying = GoodServicePermissions.Goods.Default;
        goodsSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(goodsSearchMenu);

        #endregion 查询

        #region 新增

        Menu goodsCreateMenu = new Menu(_guidGenerator.Create());
        goodsCreateMenu.TenantId = tenant?.Id;
        goodsCreateMenu.PId = goodsMenu.Id;
        goodsCreateMenu.MenuType = MenuType.Menu;
        goodsCreateMenu.Name = "goodsCreate";
        goodsCreateMenu.Component = "good/goods/create";
        goodsCreateMenu.ComponentAlias = string.Empty;
        goodsCreateMenu.IsLink = false;
        goodsCreateMenu.MenuSort = 2;
        goodsCreateMenu.Path = "/good/goods/create";
        goodsCreateMenu.Redirect = string.Empty;
        goodsCreateMenu.PermissionIdentifying = GoodServicePermissions.Goods.Create;
        goodsCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.goodsCreate",
            Icon = string.Empty,
            IsHide = true,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(goodsCreateMenu);

        #endregion 新增

        #region 修改

        Menu goodsUpdateMenu = new Menu(_guidGenerator.Create());
        goodsUpdateMenu.TenantId = tenant?.Id;
        goodsUpdateMenu.PId = goodsMenu.Id;
        goodsUpdateMenu.MenuType = MenuType.Menu;
        goodsUpdateMenu.Name = "goodsUpdate";
        goodsUpdateMenu.Component = "good/goods/update";
        goodsUpdateMenu.ComponentAlias = string.Empty;
        goodsUpdateMenu.IsLink = false;
        goodsUpdateMenu.MenuSort = 3;
        goodsUpdateMenu.Path = "/good/goods/update";
        goodsUpdateMenu.Redirect = string.Empty;
        goodsUpdateMenu.PermissionIdentifying = GoodServicePermissions.Goods.Update;
        goodsUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.goodsUpdate",
            Icon = string.Empty,
            IsHide = true,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(goodsUpdateMenu);

        #endregion 修改

        #region 删除

        Menu goodsDeleteMenu = new Menu(_guidGenerator.Create());
        goodsDeleteMenu.TenantId = tenant?.Id;
        goodsDeleteMenu.PId = goodsMenu.Id;
        goodsDeleteMenu.MenuType = MenuType.Button;
        goodsDeleteMenu.Name = string.Empty;
        goodsDeleteMenu.Component = string.Empty;
        goodsDeleteMenu.ComponentAlias = string.Empty;
        goodsDeleteMenu.IsLink = false;
        goodsDeleteMenu.MenuSort = 4;
        goodsDeleteMenu.Path = string.Empty;
        goodsDeleteMenu.Redirect = string.Empty;
        goodsDeleteMenu.PermissionIdentifying = GoodServicePermissions.Goods.Delete;
        goodsDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(goodsDeleteMenu);

        #endregion 删除

        #region 查看

        Menu goodsLookMenu = new Menu(_guidGenerator.Create());
        goodsLookMenu.TenantId = tenant?.Id;
        goodsLookMenu.PId = goodsMenu.Id;
        goodsLookMenu.MenuType = MenuType.Menu;
        goodsLookMenu.Name = "goodsLook";
        goodsLookMenu.Component = "good/goods/look";
        goodsLookMenu.ComponentAlias = string.Empty;
        goodsLookMenu.IsLink = false;
        goodsLookMenu.MenuSort = 3;
        goodsLookMenu.Path = "/good/goods/look";
        goodsLookMenu.Redirect = string.Empty;
        goodsLookMenu.PermissionIdentifying = GoodServicePermissions.Goods.Look;
        goodsLookMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.goodsLook",
            Icon = string.Empty,
            IsHide = true,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(goodsLookMenu);

        #endregion 查看

        #endregion 商品列表

        #region 品牌

        Menu brandMenu = new Menu(_guidGenerator.Create());
        brandMenu.TenantId = tenant?.Id;
        brandMenu.PId = goodMenu.Id;
        brandMenu.MenuType = MenuType.Menu;
        brandMenu.Name = "brand";
        brandMenu.Component = "good/brand/index";
        brandMenu.ComponentAlias = string.Empty;
        brandMenu.IsLink = false;
        brandMenu.MenuSort = 3;
        brandMenu.Path = "/good/brand";
        brandMenu.Redirect = string.Empty;
        brandMenu.PermissionIdentifying = string.Empty;
        brandMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.brand",
            Icon = "iconfont icon-pinpai",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(brandMenu);

        #region 查询

        Menu brandSearchMenu = new Menu(_guidGenerator.Create());
        brandSearchMenu.TenantId = tenant?.Id;
        brandSearchMenu.PId = brandMenu.Id;
        brandSearchMenu.MenuType = MenuType.Button;
        brandSearchMenu.Name = string.Empty;
        brandSearchMenu.Component = string.Empty;
        brandSearchMenu.ComponentAlias = string.Empty;
        brandSearchMenu.IsLink = false;
        brandSearchMenu.MenuSort = 1;
        brandSearchMenu.Path = string.Empty;
        brandSearchMenu.Redirect = string.Empty;
        brandSearchMenu.PermissionIdentifying = GoodServicePermissions.GoodBrands.Default;
        brandSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(brandSearchMenu);

        #endregion 查询

        #region 新增

        Menu brandCreateMenu = new Menu(_guidGenerator.Create());
        brandCreateMenu.TenantId = tenant?.Id;
        brandCreateMenu.PId = brandMenu.Id;
        brandCreateMenu.MenuType = MenuType.Button;
        brandCreateMenu.Name = string.Empty;
        brandCreateMenu.Component = string.Empty;
        brandCreateMenu.ComponentAlias = string.Empty;
        brandCreateMenu.IsLink = false;
        brandCreateMenu.MenuSort = 2;
        brandCreateMenu.Path = string.Empty;
        brandCreateMenu.Redirect = string.Empty;
        brandCreateMenu.PermissionIdentifying = GoodServicePermissions.GoodBrands.Create;
        brandCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(brandCreateMenu);

        #endregion 新增

        #region 修改

        Menu brandUpdateMenu = new Menu(_guidGenerator.Create());
        brandUpdateMenu.TenantId = tenant?.Id;
        brandUpdateMenu.PId = brandMenu.Id;
        brandUpdateMenu.MenuType = MenuType.Button;
        brandUpdateMenu.Name = string.Empty;
        brandUpdateMenu.Component = string.Empty;
        brandUpdateMenu.ComponentAlias = string.Empty;
        brandUpdateMenu.IsLink = false;
        brandUpdateMenu.MenuSort = 3;
        brandUpdateMenu.Path = string.Empty;
        brandUpdateMenu.Redirect = string.Empty;
        brandUpdateMenu.PermissionIdentifying = GoodServicePermissions.GoodBrands.Update;
        brandUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(brandUpdateMenu);

        #endregion 修改

        #region 删除

        Menu brandDeleteMenu = new Menu(_guidGenerator.Create());
        brandDeleteMenu.TenantId = tenant?.Id;
        brandDeleteMenu.PId = brandMenu.Id;
        brandDeleteMenu.MenuType = MenuType.Button;
        brandDeleteMenu.Name = string.Empty;
        brandDeleteMenu.Component = string.Empty;
        brandDeleteMenu.ComponentAlias = string.Empty;
        brandDeleteMenu.IsLink = false;
        brandDeleteMenu.MenuSort = 4;
        brandDeleteMenu.Path = string.Empty;
        brandDeleteMenu.Redirect = string.Empty;
        brandDeleteMenu.PermissionIdentifying = GoodServicePermissions.GoodBrands.Delete;
        brandDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(brandDeleteMenu);

        #endregion 删除

        #endregion 品牌

        #region 商品分类

        Menu categoryMenu = new Menu(_guidGenerator.Create());
        categoryMenu.TenantId = tenant?.Id;
        categoryMenu.PId = goodMenu.Id;
        categoryMenu.MenuType = MenuType.Menu;
        categoryMenu.Name = "goodCategory";
        categoryMenu.Component = "good/category/index";
        categoryMenu.ComponentAlias = string.Empty;
        categoryMenu.IsLink = false;
        categoryMenu.MenuSort = 4;
        categoryMenu.Path = "/good/category";
        categoryMenu.Redirect = string.Empty;
        categoryMenu.PermissionIdentifying = string.Empty;
        categoryMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.goodCategory",
            Icon = "iconfont icon-shangpinfenlei1",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(categoryMenu);

        #region 查询

        Menu categorySearchMenu = new Menu(_guidGenerator.Create());
        categorySearchMenu.TenantId = tenant?.Id;
        categorySearchMenu.PId = categoryMenu.Id;
        categorySearchMenu.MenuType = MenuType.Button;
        categorySearchMenu.Name = string.Empty;
        categorySearchMenu.Component = string.Empty;
        categorySearchMenu.ComponentAlias = string.Empty;
        categorySearchMenu.IsLink = false;
        categorySearchMenu.MenuSort = 1;
        categorySearchMenu.Path = string.Empty;
        categorySearchMenu.Redirect = string.Empty;
        categorySearchMenu.PermissionIdentifying = GoodServicePermissions.GoodCategorys.Default;
        categorySearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(categorySearchMenu);

        #endregion 查询

        #region 新增

        Menu categoryCreateMenu = new Menu(_guidGenerator.Create());
        categoryCreateMenu.TenantId = tenant?.Id;
        categoryCreateMenu.PId = categoryMenu.Id;
        categoryCreateMenu.MenuType = MenuType.Button;
        categoryCreateMenu.Name = string.Empty;
        categoryCreateMenu.Component = string.Empty;
        categoryCreateMenu.ComponentAlias = string.Empty;
        categoryCreateMenu.IsLink = false;
        categoryCreateMenu.MenuSort = 2;
        categoryCreateMenu.Path = string.Empty;
        categoryCreateMenu.Redirect = string.Empty;
        categoryCreateMenu.PermissionIdentifying = GoodServicePermissions.GoodCategorys.Create;
        categoryCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(categoryCreateMenu);

        #endregion 新增

        #region 修改

        Menu categoryUpdateMenu = new Menu(_guidGenerator.Create());
        categoryUpdateMenu.TenantId = tenant?.Id;
        categoryUpdateMenu.PId = categoryMenu.Id;
        categoryUpdateMenu.MenuType = MenuType.Button;
        categoryUpdateMenu.Name = string.Empty;
        categoryUpdateMenu.Component = string.Empty;
        categoryUpdateMenu.ComponentAlias = string.Empty;
        categoryUpdateMenu.IsLink = false;
        categoryUpdateMenu.MenuSort = 3;
        categoryUpdateMenu.Path = string.Empty;
        categoryUpdateMenu.Redirect = string.Empty;
        categoryUpdateMenu.PermissionIdentifying = GoodServicePermissions.GoodCategorys.Update;
        categoryUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(categoryUpdateMenu);

        #endregion 修改

        #region 删除

        Menu categoryDeleteMenu = new Menu(_guidGenerator.Create());
        categoryDeleteMenu.TenantId = tenant?.Id;
        categoryDeleteMenu.PId = categoryMenu.Id;
        categoryDeleteMenu.MenuType = MenuType.Button;
        categoryDeleteMenu.Name = string.Empty;
        categoryDeleteMenu.Component = string.Empty;
        categoryDeleteMenu.ComponentAlias = string.Empty;
        categoryDeleteMenu.IsLink = false;
        categoryDeleteMenu.MenuSort = 4;
        categoryDeleteMenu.Path = string.Empty;
        categoryDeleteMenu.Redirect = string.Empty;
        categoryDeleteMenu.PermissionIdentifying = GoodServicePermissions.GoodCategorys.Delete;
        categoryDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(categoryDeleteMenu);

        #endregion 删除

        #endregion 商品分类

        #region 参数模型

        Menu paramMenu = new Menu(_guidGenerator.Create());
        paramMenu.TenantId = tenant?.Id;
        paramMenu.PId = goodMenu.Id;
        paramMenu.MenuType = MenuType.Menu;
        paramMenu.Name = "goodParam";
        paramMenu.Component = "good/param/index";
        paramMenu.ComponentAlias = string.Empty;
        paramMenu.IsLink = false;
        paramMenu.MenuSort = 4;
        paramMenu.Path = "/good/param";
        paramMenu.Redirect = string.Empty;
        paramMenu.PermissionIdentifying = string.Empty;
        paramMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.goodParam",
            Icon = "iconfont icon-canshuchaxun",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(paramMenu);

        #region 查询

        Menu paramSearchMenu = new Menu(_guidGenerator.Create());
        paramSearchMenu.TenantId = tenant?.Id;
        paramSearchMenu.PId = paramMenu.Id;
        paramSearchMenu.MenuType = MenuType.Button;
        paramSearchMenu.Name = string.Empty;
        paramSearchMenu.Component = string.Empty;
        paramSearchMenu.ComponentAlias = string.Empty;
        paramSearchMenu.IsLink = false;
        paramSearchMenu.MenuSort = 1;
        paramSearchMenu.Path = string.Empty;
        paramSearchMenu.Redirect = string.Empty;
        paramSearchMenu.PermissionIdentifying = GoodServicePermissions.GoodParams.Default;
        paramSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(paramSearchMenu);

        #endregion 查询

        #region 新增

        Menu paramCreateMenu = new Menu(_guidGenerator.Create());
        paramCreateMenu.TenantId = tenant?.Id;
        paramCreateMenu.PId = paramMenu.Id;
        paramCreateMenu.MenuType = MenuType.Button;
        paramCreateMenu.Name = string.Empty;
        paramCreateMenu.Component = string.Empty;
        paramCreateMenu.ComponentAlias = string.Empty;
        paramCreateMenu.IsLink = false;
        paramCreateMenu.MenuSort = 2;
        paramCreateMenu.Path = string.Empty;
        paramCreateMenu.Redirect = string.Empty;
        paramCreateMenu.PermissionIdentifying = GoodServicePermissions.GoodParams.Create;
        paramCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(paramCreateMenu);

        #endregion 新增

        #region 修改

        Menu paramUpdateMenu = new Menu(_guidGenerator.Create());
        paramUpdateMenu.TenantId = tenant?.Id;
        paramUpdateMenu.PId = paramMenu.Id;
        paramUpdateMenu.MenuType = MenuType.Button;
        paramUpdateMenu.Name = string.Empty;
        paramUpdateMenu.Component = string.Empty;
        paramUpdateMenu.ComponentAlias = string.Empty;
        paramUpdateMenu.IsLink = false;
        paramUpdateMenu.MenuSort = 3;
        paramUpdateMenu.Path = string.Empty;
        paramUpdateMenu.Redirect = string.Empty;
        paramUpdateMenu.PermissionIdentifying = GoodServicePermissions.GoodParams.Update;
        paramUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(paramUpdateMenu);

        #endregion 修改

        #region 删除

        Menu paramDeleteMenu = new Menu(_guidGenerator.Create());
        paramDeleteMenu.TenantId = tenant?.Id;
        paramDeleteMenu.PId = paramMenu.Id;
        paramDeleteMenu.MenuType = MenuType.Button;
        paramDeleteMenu.Name = string.Empty;
        paramDeleteMenu.Component = string.Empty;
        paramDeleteMenu.ComponentAlias = string.Empty;
        paramDeleteMenu.IsLink = false;
        paramDeleteMenu.MenuSort = 5;
        paramDeleteMenu.Path = string.Empty;
        paramDeleteMenu.Redirect = string.Empty;
        paramDeleteMenu.PermissionIdentifying = GoodServicePermissions.GoodParams.Delete;
        paramDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(paramDeleteMenu);

        #endregion 删除

        #endregion 参数模型

        #region SKU模型

        Menu typeSpecMenu = new Menu(_guidGenerator.Create());
        typeSpecMenu.TenantId = tenant?.Id;
        typeSpecMenu.PId = goodMenu.Id;
        typeSpecMenu.MenuType = MenuType.Menu;
        typeSpecMenu.Name = "goodTypeSpec";
        typeSpecMenu.Component = "good/typeSpec/index";
        typeSpecMenu.ComponentAlias = string.Empty;
        typeSpecMenu.IsLink = false;
        typeSpecMenu.MenuSort = 6;
        typeSpecMenu.Path = "/good/typeSpec";
        typeSpecMenu.Redirect = string.Empty;
        typeSpecMenu.PermissionIdentifying = string.Empty;
        typeSpecMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.goodTypeSpec",
            Icon = "ele-ScaleToOriginal",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(typeSpecMenu);

        #region 查询

        Menu typeSpecSearchMenu = new Menu(_guidGenerator.Create());
        typeSpecSearchMenu.TenantId = tenant?.Id;
        typeSpecSearchMenu.PId = typeSpecMenu.Id;
        typeSpecSearchMenu.MenuType = MenuType.Button;
        typeSpecSearchMenu.Name = string.Empty;
        typeSpecSearchMenu.Component = string.Empty;
        typeSpecSearchMenu.ComponentAlias = string.Empty;
        typeSpecSearchMenu.IsLink = false;
        typeSpecSearchMenu.MenuSort = 6;
        typeSpecSearchMenu.Path = string.Empty;
        typeSpecSearchMenu.Redirect = string.Empty;
        typeSpecSearchMenu.PermissionIdentifying = GoodServicePermissions.GoodTypeSpecs.Default;
        typeSpecSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(typeSpecSearchMenu);

        #endregion 查询

        #region 新增

        Menu typeSpecCreateMenu = new Menu(_guidGenerator.Create());
        typeSpecCreateMenu.TenantId = tenant?.Id;
        typeSpecCreateMenu.PId = typeSpecMenu.Id;
        typeSpecCreateMenu.MenuType = MenuType.Button;
        typeSpecCreateMenu.Name = string.Empty;
        typeSpecCreateMenu.Component = string.Empty;
        typeSpecCreateMenu.ComponentAlias = string.Empty;
        typeSpecCreateMenu.IsLink = false;
        typeSpecCreateMenu.MenuSort = 2;
        typeSpecCreateMenu.Path = string.Empty;
        typeSpecCreateMenu.Redirect = string.Empty;
        typeSpecCreateMenu.PermissionIdentifying = GoodServicePermissions.GoodTypeSpecs.Create;
        typeSpecCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(typeSpecCreateMenu);

        #endregion 新增

        #region 修改

        Menu typeSpecUpdateMenu = new Menu(_guidGenerator.Create());
        typeSpecUpdateMenu.TenantId = tenant?.Id;
        typeSpecUpdateMenu.PId = typeSpecMenu.Id;
        typeSpecUpdateMenu.MenuType = MenuType.Button;
        typeSpecUpdateMenu.Name = string.Empty;
        typeSpecUpdateMenu.Component = string.Empty;
        typeSpecUpdateMenu.ComponentAlias = string.Empty;
        typeSpecUpdateMenu.IsLink = false;
        typeSpecUpdateMenu.MenuSort = 3;
        typeSpecUpdateMenu.Path = string.Empty;
        typeSpecUpdateMenu.Redirect = string.Empty;
        typeSpecUpdateMenu.PermissionIdentifying = GoodServicePermissions.GoodTypeSpecs.Update;
        typeSpecUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(typeSpecUpdateMenu);

        #endregion 修改

        #region 删除

        Menu typeSpecDeleteMenu = new Menu(_guidGenerator.Create());
        typeSpecDeleteMenu.TenantId = tenant?.Id;
        typeSpecDeleteMenu.PId = typeSpecMenu.Id;
        typeSpecDeleteMenu.MenuType = MenuType.Button;
        typeSpecDeleteMenu.Name = string.Empty;
        typeSpecDeleteMenu.Component = string.Empty;
        typeSpecDeleteMenu.ComponentAlias = string.Empty;
        typeSpecDeleteMenu.IsLink = false;
        typeSpecDeleteMenu.MenuSort = 4;
        typeSpecDeleteMenu.Path = string.Empty;
        typeSpecDeleteMenu.Redirect = string.Empty;
        typeSpecDeleteMenu.PermissionIdentifying = GoodServicePermissions.GoodTypeSpecs.Delete;
        typeSpecDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(typeSpecDeleteMenu);

        #endregion 删除

        #endregion SKU模型

        #region 商品评价

        Menu commentMenu = new Menu(_guidGenerator.Create());
        commentMenu.TenantId = tenant?.Id;
        commentMenu.PId = goodMenu.Id;
        commentMenu.MenuType = MenuType.Menu;
        commentMenu.Name = "goodComment";
        commentMenu.Component = "good/comment/index";
        commentMenu.ComponentAlias = string.Empty;
        commentMenu.IsLink = false;
        commentMenu.MenuSort = 5;
        commentMenu.Path = "/good/comment";
        commentMenu.Redirect = string.Empty;
        commentMenu.PermissionIdentifying = string.Empty;
        commentMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.goodComment",
            Icon = "ele-Pointer",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(commentMenu);

        #region 查询

        Menu commentSearchMenu = new Menu(_guidGenerator.Create());
        commentSearchMenu.TenantId = tenant?.Id;
        commentSearchMenu.PId = commentMenu.Id;
        commentSearchMenu.MenuType = MenuType.Button;
        commentSearchMenu.Name = string.Empty;
        commentSearchMenu.Component = string.Empty;
        commentSearchMenu.ComponentAlias = string.Empty;
        commentSearchMenu.IsLink = false;
        commentSearchMenu.MenuSort = 1;
        commentSearchMenu.Path = string.Empty;
        commentSearchMenu.Redirect = string.Empty;
        commentSearchMenu.PermissionIdentifying = GoodServicePermissions.GoodComments.Default;
        commentSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(commentSearchMenu);

        #endregion 查询

        #region 卖家回复

        Menu sellerReplyMenu = new Menu(_guidGenerator.Create());
        sellerReplyMenu.TenantId = tenant?.Id;
        sellerReplyMenu.PId = commentMenu.Id;
        sellerReplyMenu.MenuType = MenuType.Button;
        sellerReplyMenu.Name = string.Empty;
        sellerReplyMenu.Component = string.Empty;
        sellerReplyMenu.ComponentAlias = string.Empty;
        sellerReplyMenu.IsLink = false;
        sellerReplyMenu.MenuSort = 3;
        sellerReplyMenu.Path = string.Empty;
        sellerReplyMenu.Redirect = string.Empty;
        sellerReplyMenu.PermissionIdentifying = GoodServicePermissions.GoodComments.SellerReply;
        sellerReplyMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.sellerReply",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(sellerReplyMenu);

        #endregion 卖家回复

        #region 删除

        Menu commentDeleteMenu = new Menu(_guidGenerator.Create());
        commentDeleteMenu.TenantId = tenant?.Id;
        commentDeleteMenu.PId = commentMenu.Id;
        commentDeleteMenu.MenuType = MenuType.Button;
        commentDeleteMenu.Name = string.Empty;
        commentDeleteMenu.Component = string.Empty;
        commentDeleteMenu.ComponentAlias = string.Empty;
        commentDeleteMenu.IsLink = false;
        commentDeleteMenu.MenuSort = 3;
        commentDeleteMenu.Path = string.Empty;
        commentDeleteMenu.Redirect = string.Empty;
        commentDeleteMenu.PermissionIdentifying = GoodServicePermissions.GoodComments.Delete;
        commentDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(commentDeleteMenu);

        #endregion 删除

        #endregion 商品评价

        #endregion 商品管理

        #region 订单管理

        Menu orderManageMenu = new Menu(_guidGenerator.Create());
        orderManageMenu.TenantId = tenant?.Id;
        orderManageMenu.MenuType = MenuType.Menu;
        orderManageMenu.Name = "orderManage";
        orderManageMenu.Component = "layout/routerView/parent";
        orderManageMenu.ComponentAlias = string.Empty;
        orderManageMenu.IsLink = false;
        orderManageMenu.MenuSort = 4;
        orderManageMenu.Path = "/orderManage";
        orderManageMenu.Redirect = "/orderManage/order";
        orderManageMenu.PermissionIdentifying = string.Empty;
        orderManageMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.orderManage",
            Icon = "iconfont icon-dingdanguanli",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderManageMenu);

        #region 订单列表

        Menu orderMenu = new Menu(_guidGenerator.Create());
        orderMenu.TenantId = tenant?.Id;
        orderMenu.PId = orderManageMenu.Id;
        orderMenu.MenuType = MenuType.Menu;
        orderMenu.Name = "order";
        orderMenu.Component = "orderManage/order/index";
        orderMenu.ComponentAlias = string.Empty;
        orderMenu.IsLink = false;
        orderMenu.MenuSort = 1;
        orderMenu.Path = "/orderManage/order";
        orderMenu.Redirect = string.Empty;
        orderMenu.PermissionIdentifying = string.Empty;
        orderMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.order",
            Icon = "iconfont icon-shangpindingdan",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderMenu);

        #region 查询

        Menu orderSearchMenu = new Menu(_guidGenerator.Create());
        orderSearchMenu.TenantId = tenant?.Id;
        orderSearchMenu.PId = orderMenu.Id;
        orderSearchMenu.MenuType = MenuType.Button;
        orderSearchMenu.Name = string.Empty;
        orderSearchMenu.Component = string.Empty;
        orderSearchMenu.ComponentAlias = string.Empty;
        orderSearchMenu.IsLink = false;
        orderSearchMenu.MenuSort = 1;
        orderSearchMenu.Path = string.Empty;
        orderSearchMenu.Redirect = string.Empty;
        orderSearchMenu.PermissionIdentifying = OrderServicePermissions.Orders.Default;
        orderSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderSearchMenu);

        #endregion 查询

        #region 修改

        Menu orderUpdateMenu = new Menu(_guidGenerator.Create());
        orderUpdateMenu.TenantId = tenant?.Id;
        orderUpdateMenu.PId = orderMenu.Id;
        orderUpdateMenu.MenuType = MenuType.Button;
        orderUpdateMenu.Name = string.Empty;
        orderUpdateMenu.Component = string.Empty;
        orderUpdateMenu.ComponentAlias = string.Empty;
        orderUpdateMenu.IsLink = false;
        orderUpdateMenu.MenuSort = 2;
        orderUpdateMenu.Path = string.Empty;
        orderUpdateMenu.Redirect = string.Empty;
        orderUpdateMenu.PermissionIdentifying = OrderServicePermissions.Orders.Update;
        orderUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderUpdateMenu);

        #endregion 修改

        #region 删除

        Menu orderDeleteMenu = new Menu(_guidGenerator.Create());
        orderDeleteMenu.TenantId = tenant?.Id;
        orderDeleteMenu.PId = orderMenu.Id;
        orderDeleteMenu.MenuType = MenuType.Button;
        orderDeleteMenu.Name = string.Empty;
        orderDeleteMenu.Component = string.Empty;
        orderDeleteMenu.ComponentAlias = string.Empty;
        orderDeleteMenu.IsLink = false;
        orderDeleteMenu.MenuSort = 3;
        orderDeleteMenu.Path = string.Empty;
        orderDeleteMenu.Redirect = string.Empty;
        orderDeleteMenu.PermissionIdentifying = OrderServicePermissions.Orders.Delete;
        orderDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderDeleteMenu);

        #endregion 删除

        #region 取消

        Menu cancelDeleteMenu = new Menu(_guidGenerator.Create());
        cancelDeleteMenu.TenantId = tenant?.Id;
        cancelDeleteMenu.PId = orderMenu.Id;
        cancelDeleteMenu.MenuType = MenuType.Button;
        cancelDeleteMenu.Name = string.Empty;
        cancelDeleteMenu.Component = string.Empty;
        cancelDeleteMenu.ComponentAlias = string.Empty;
        cancelDeleteMenu.IsLink = false;
        cancelDeleteMenu.MenuSort = 4;
        cancelDeleteMenu.Path = string.Empty;
        cancelDeleteMenu.Redirect = string.Empty;
        cancelDeleteMenu.PermissionIdentifying = OrderServicePermissions.Orders.Cancel;
        cancelDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.cancel",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(cancelDeleteMenu);

        #endregion 取消

        #region 支付

        Menu playDeleteMenu = new Menu(_guidGenerator.Create());
        playDeleteMenu.TenantId = tenant?.Id;
        playDeleteMenu.PId = orderMenu.Id;
        playDeleteMenu.MenuType = MenuType.Button;
        playDeleteMenu.Name = string.Empty;
        playDeleteMenu.Component = string.Empty;
        playDeleteMenu.ComponentAlias = string.Empty;
        playDeleteMenu.IsLink = false;
        playDeleteMenu.MenuSort = 5;
        playDeleteMenu.Path = string.Empty;
        playDeleteMenu.Redirect = string.Empty;
        playDeleteMenu.PermissionIdentifying = OrderServicePermissions.Orders.Pay;
        playDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.pay",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(playDeleteMenu);

        #endregion 支付

        #region 发货

        Menu deliveryMenu = new Menu(_guidGenerator.Create());
        deliveryMenu.TenantId = tenant?.Id;
        deliveryMenu.PId = orderMenu.Id;
        deliveryMenu.MenuType = MenuType.Button;
        deliveryMenu.Name = string.Empty;
        deliveryMenu.Component = string.Empty;
        deliveryMenu.ComponentAlias = string.Empty;
        deliveryMenu.IsLink = false;
        deliveryMenu.MenuSort = 6;
        deliveryMenu.Path = string.Empty;
        deliveryMenu.Redirect = string.Empty;
        deliveryMenu.PermissionIdentifying = OrderServicePermissions.Orders.Delivery;
        deliveryMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.delivery",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(deliveryMenu);

        #endregion 发货

        #region 打印

        Menu printDeleteMenu = new Menu(_guidGenerator.Create());
        printDeleteMenu.TenantId = tenant?.Id;
        printDeleteMenu.PId = orderMenu.Id;
        printDeleteMenu.MenuType = MenuType.Button;
        printDeleteMenu.Name = string.Empty;
        printDeleteMenu.Component = string.Empty;
        printDeleteMenu.ComponentAlias = string.Empty;
        printDeleteMenu.IsLink = false;
        printDeleteMenu.MenuSort = 7;
        printDeleteMenu.Path = string.Empty;
        printDeleteMenu.Redirect = string.Empty;
        printDeleteMenu.PermissionIdentifying = OrderServicePermissions.Orders.Print;
        printDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.print",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(printDeleteMenu);

        #endregion 打印

        #region 完成

        Menu completeDeleteMenu = new Menu(_guidGenerator.Create());
        completeDeleteMenu.TenantId = tenant?.Id;
        completeDeleteMenu.PId = orderMenu.Id;
        completeDeleteMenu.MenuType = MenuType.Button;
        completeDeleteMenu.Name = string.Empty;
        completeDeleteMenu.Component = string.Empty;
        completeDeleteMenu.ComponentAlias = string.Empty;
        completeDeleteMenu.IsLink = false;
        completeDeleteMenu.MenuSort = 8;
        completeDeleteMenu.Path = string.Empty;
        completeDeleteMenu.Redirect = string.Empty;
        completeDeleteMenu.PermissionIdentifying = OrderServicePermissions.Orders.Complete;
        completeDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.complete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(completeDeleteMenu);

        #endregion 完成

        #endregion 订单列表

        #region 发货单列表

        Menu billDeliveryMenu = new Menu(_guidGenerator.Create());
        billDeliveryMenu.TenantId = tenant?.Id;
        billDeliveryMenu.PId = orderManageMenu.Id;
        billDeliveryMenu.MenuType = MenuType.Menu;
        billDeliveryMenu.Name = "billDelivery";
        billDeliveryMenu.Component = "orderManage/billDelivery/index";
        billDeliveryMenu.ComponentAlias = string.Empty;
        billDeliveryMenu.IsLink = false;
        billDeliveryMenu.MenuSort = 2;
        billDeliveryMenu.Path = "/orderManage/billDelivery";
        billDeliveryMenu.Redirect = string.Empty;
        billDeliveryMenu.PermissionIdentifying = string.Empty;
        billDeliveryMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.billDelivery",
            Icon = "ele-Van",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(billDeliveryMenu);

        #region 查询

        Menu billDeliverySearchMenu = new Menu(_guidGenerator.Create());
        billDeliverySearchMenu.TenantId = tenant?.Id;
        billDeliverySearchMenu.PId = billDeliveryMenu.Id;
        billDeliverySearchMenu.MenuType = MenuType.Button;
        billDeliverySearchMenu.Name = string.Empty;
        billDeliverySearchMenu.Component = string.Empty;
        billDeliverySearchMenu.ComponentAlias = string.Empty;
        billDeliverySearchMenu.IsLink = false;
        billDeliverySearchMenu.MenuSort = 1;
        billDeliverySearchMenu.Path = string.Empty;
        billDeliverySearchMenu.Redirect = string.Empty;
        billDeliverySearchMenu.PermissionIdentifying = OrderServicePermissions.BillDeliverys.Default;
        billDeliverySearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(billDeliverySearchMenu);

        #endregion 查询

        #region 修改

        Menu billDeliveryUpdateMenu = new Menu(_guidGenerator.Create());
        billDeliveryUpdateMenu.TenantId = tenant?.Id;
        billDeliveryUpdateMenu.PId = billDeliveryMenu.Id;
        billDeliveryUpdateMenu.MenuType = MenuType.Button;
        billDeliveryUpdateMenu.Name = string.Empty;
        billDeliveryUpdateMenu.Component = string.Empty;
        billDeliveryUpdateMenu.ComponentAlias = string.Empty;
        billDeliveryUpdateMenu.IsLink = false;
        billDeliveryUpdateMenu.MenuSort = 2;
        billDeliveryUpdateMenu.Path = string.Empty;
        billDeliveryUpdateMenu.Redirect = string.Empty;
        billDeliveryUpdateMenu.PermissionIdentifying = OrderServicePermissions.BillDeliverys.Update;
        billDeliveryUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(billDeliveryUpdateMenu);

        #endregion 修改

        #endregion 发货单列表

        #region 售后单列表

        Menu billAftersalesMenu = new Menu(_guidGenerator.Create());
        billAftersalesMenu.TenantId = tenant?.Id;
        billAftersalesMenu.PId = orderManageMenu.Id;
        billAftersalesMenu.MenuType = MenuType.Menu;
        billAftersalesMenu.Name = "billAftersales";
        billAftersalesMenu.Component = "orderManage/billAftersales/index";
        billAftersalesMenu.ComponentAlias = string.Empty;
        billAftersalesMenu.IsLink = false;
        billAftersalesMenu.MenuSort = 3;
        billAftersalesMenu.Path = "/orderManage/billAftersales";
        billAftersalesMenu.Redirect = string.Empty;
        billAftersalesMenu.PermissionIdentifying = string.Empty;
        billAftersalesMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.billAftersales",
            Icon = "iconfont icon-fuwu2",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(billAftersalesMenu);

        #region 查询

        Menu billAftersalesSearchMenu = new Menu(_guidGenerator.Create());
        billAftersalesSearchMenu.TenantId = tenant?.Id;
        billAftersalesSearchMenu.PId = billAftersalesMenu.Id;
        billAftersalesSearchMenu.MenuType = MenuType.Button;
        billAftersalesSearchMenu.Name = string.Empty;
        billAftersalesSearchMenu.Component = string.Empty;
        billAftersalesSearchMenu.ComponentAlias = string.Empty;
        billAftersalesSearchMenu.IsLink = false;
        billAftersalesSearchMenu.MenuSort = 1;
        billAftersalesSearchMenu.Path = string.Empty;
        billAftersalesSearchMenu.Redirect = string.Empty;
        billAftersalesSearchMenu.PermissionIdentifying = OrderServicePermissions.BillAftersales.Default;
        billAftersalesSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(billAftersalesSearchMenu);

        #endregion 查询

        #region 操作

        Menu billDeliveryOperateMenu = new Menu(_guidGenerator.Create());
        billDeliveryOperateMenu.TenantId = tenant?.Id;
        billDeliveryOperateMenu.PId = billAftersalesMenu.Id;
        billDeliveryOperateMenu.MenuType = MenuType.Button;
        billDeliveryOperateMenu.Name = string.Empty;
        billDeliveryOperateMenu.Component = string.Empty;
        billDeliveryOperateMenu.ComponentAlias = string.Empty;
        billDeliveryOperateMenu.IsLink = false;
        billDeliveryOperateMenu.MenuSort = 2;
        billDeliveryOperateMenu.Path = string.Empty;
        billDeliveryOperateMenu.Redirect = string.Empty;
        billDeliveryOperateMenu.PermissionIdentifying = OrderServicePermissions.BillAftersales.Operate;
        billDeliveryOperateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.operate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(billDeliveryOperateMenu);

        #endregion 操作

        #endregion 售后单列表

        #region 退货单列表

        Menu billReshipMenu = new Menu(_guidGenerator.Create());
        billReshipMenu.TenantId = tenant?.Id;
        billReshipMenu.PId = orderManageMenu.Id;
        billReshipMenu.MenuType = MenuType.Menu;
        billReshipMenu.Name = "billReship";
        billReshipMenu.Component = "orderManage/billReship/index";
        billReshipMenu.ComponentAlias = string.Empty;
        billReshipMenu.IsLink = false;
        billReshipMenu.MenuSort = 4;
        billReshipMenu.Path = "/orderManage/billReship";
        billReshipMenu.Redirect = string.Empty;
        billReshipMenu.PermissionIdentifying = string.Empty;
        billReshipMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.billReship",
            Icon = "ele-SoldOut",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(billReshipMenu);

        #region 查询

        Menu billReshipSearchMenu = new Menu(_guidGenerator.Create());
        billReshipSearchMenu.TenantId = tenant?.Id;
        billReshipSearchMenu.PId = billReshipMenu.Id;
        billReshipSearchMenu.MenuType = MenuType.Button;
        billReshipSearchMenu.Name = string.Empty;
        billReshipSearchMenu.Component = string.Empty;
        billReshipSearchMenu.ComponentAlias = string.Empty;
        billReshipSearchMenu.IsLink = false;
        billReshipSearchMenu.MenuSort = 1;
        billReshipSearchMenu.Path = string.Empty;
        billReshipSearchMenu.Redirect = string.Empty;
        billReshipSearchMenu.PermissionIdentifying = OrderServicePermissions.BillReships.Default;
        billReshipSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(billReshipSearchMenu);

        #endregion 查询

        #region 确认收货

        Menu billReshipConfirmMenu = new Menu(_guidGenerator.Create());
        billReshipConfirmMenu.TenantId = tenant?.Id;
        billReshipConfirmMenu.PId = billReshipMenu.Id;
        billReshipConfirmMenu.MenuType = MenuType.Button;
        billReshipConfirmMenu.Name = string.Empty;
        billReshipConfirmMenu.Component = string.Empty;
        billReshipConfirmMenu.ComponentAlias = string.Empty;
        billReshipConfirmMenu.IsLink = false;
        billReshipConfirmMenu.MenuSort = 2;
        billReshipConfirmMenu.Path = string.Empty;
        billReshipConfirmMenu.Redirect = string.Empty;
        billReshipConfirmMenu.PermissionIdentifying = OrderServicePermissions.BillReships.ConfirmDelivery;
        billReshipConfirmMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.confirmDelivery",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(billReshipConfirmMenu);

        #endregion 确认收货

        #endregion 退货单列表

        #endregion 订单管理

        #region 运营管理

        Menu operationManageMenu = new Menu(_guidGenerator.Create());
        operationManageMenu.TenantId = tenant?.Id;
        operationManageMenu.MenuType = MenuType.Menu;
        operationManageMenu.Name = "operationManage";
        operationManageMenu.Component = "layout/routerView/parent";
        operationManageMenu.ComponentAlias = string.Empty;
        operationManageMenu.IsLink = false;
        operationManageMenu.MenuSort = 5;
        operationManageMenu.Path = "/operationManage";
        operationManageMenu.Redirect = "/operationManage/notices";
        operationManageMenu.PermissionIdentifying = string.Empty;
        operationManageMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.operationManage",
            Icon = "iconfont icon-yunyingguanli1",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(operationManageMenu);

        #region 公告列表

        Menu noticeMenu = new Menu(_guidGenerator.Create());
        noticeMenu.PId = operationManageMenu.Id;
        noticeMenu.TenantId = tenant?.Id;
        noticeMenu.MenuType = MenuType.Menu;
        noticeMenu.Name = "noticeList";
        noticeMenu.Component = "operationManage/notices/index";
        noticeMenu.ComponentAlias = string.Empty;
        noticeMenu.IsLink = false;
        noticeMenu.MenuSort = 1;
        noticeMenu.Path = "/operationManage/notices";
        noticeMenu.Redirect = string.Empty;
        noticeMenu.PermissionIdentifying = string.Empty;
        noticeMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.noticeList",
            Icon = "iconfont icon-gonggao",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(noticeMenu);

        #region 查询

        Menu noticeSearchMenu = new Menu(_guidGenerator.Create());
        noticeSearchMenu.TenantId = tenant?.Id;
        noticeSearchMenu.PId = noticeMenu.Id;
        noticeSearchMenu.MenuType = MenuType.Button;
        noticeSearchMenu.Name = string.Empty;
        noticeSearchMenu.Component = string.Empty;
        noticeSearchMenu.ComponentAlias = string.Empty;
        noticeSearchMenu.IsLink = false;
        noticeSearchMenu.MenuSort = 1;
        noticeSearchMenu.Path = string.Empty;
        noticeSearchMenu.Redirect = string.Empty;
        noticeSearchMenu.PermissionIdentifying = GoodServicePermissions.Notices.Default;
        noticeSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(noticeSearchMenu);

        #endregion 查询

        #region 新增

        Menu noticeCreateMenu = new Menu(_guidGenerator.Create());
        noticeCreateMenu.TenantId = tenant?.Id;
        noticeCreateMenu.PId = noticeMenu.Id;
        noticeCreateMenu.MenuType = MenuType.Button;
        noticeCreateMenu.Name = string.Empty;
        noticeCreateMenu.Component = string.Empty;
        noticeCreateMenu.ComponentAlias = string.Empty;
        noticeCreateMenu.IsLink = false;
        noticeCreateMenu.MenuSort = 2;
        noticeCreateMenu.Path = string.Empty;
        noticeCreateMenu.Redirect = string.Empty;
        noticeCreateMenu.PermissionIdentifying = GoodServicePermissions.Notices.Create;
        noticeCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(noticeCreateMenu);

        #endregion 新增

        #region 修改

        Menu noticeUpdateMenu = new Menu(_guidGenerator.Create());
        noticeUpdateMenu.TenantId = tenant?.Id;
        noticeUpdateMenu.PId = noticeMenu.Id;
        noticeUpdateMenu.MenuType = MenuType.Button;
        noticeUpdateMenu.Name = string.Empty;
        noticeUpdateMenu.Component = string.Empty;
        noticeUpdateMenu.ComponentAlias = string.Empty;
        noticeUpdateMenu.IsLink = false;
        noticeUpdateMenu.MenuSort = 3;
        noticeUpdateMenu.Path = string.Empty;
        noticeUpdateMenu.Redirect = string.Empty;
        noticeUpdateMenu.PermissionIdentifying = GoodServicePermissions.Notices.Update;
        noticeUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(noticeUpdateMenu);

        #endregion 修改

        #region 删除

        Menu noticeDeleteMenu = new Menu(_guidGenerator.Create());
        noticeDeleteMenu.TenantId = tenant?.Id;
        noticeDeleteMenu.PId = noticeMenu.Id;
        noticeDeleteMenu.MenuType = MenuType.Button;
        noticeDeleteMenu.Name = string.Empty;
        noticeDeleteMenu.Component = string.Empty;
        noticeDeleteMenu.ComponentAlias = string.Empty;
        noticeDeleteMenu.IsLink = false;
        noticeDeleteMenu.MenuSort = 4;
        noticeDeleteMenu.Path = string.Empty;
        noticeDeleteMenu.Redirect = string.Empty;
        noticeDeleteMenu.PermissionIdentifying = GoodServicePermissions.Notices.Delete;
        noticeDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(noticeDeleteMenu);

        #endregion 删除

        #endregion 公告列表

        #region 文章管理

        Menu articleManageMenu = new Menu(_guidGenerator.Create());
        articleManageMenu.PId = operationManageMenu.Id;
        articleManageMenu.TenantId = tenant?.Id;
        articleManageMenu.MenuType = MenuType.Menu;
        articleManageMenu.Name = "articleManage";
        articleManageMenu.Component = "layout/routerView/parent";
        articleManageMenu.ComponentAlias = string.Empty;
        articleManageMenu.IsLink = false;
        articleManageMenu.MenuSort = 2;
        articleManageMenu.Path = "/articleManage";
        articleManageMenu.Redirect = string.Empty;
        articleManageMenu.PermissionIdentifying = string.Empty;
        articleManageMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.articleManage",
            Icon = "iconfont icon-xitongrizhi",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(articleManageMenu);

        #region 文章分类

        Menu articleTypeMenu = new Menu(_guidGenerator.Create());
        articleTypeMenu.PId = articleManageMenu.Id;
        articleTypeMenu.TenantId = tenant?.Id;
        articleTypeMenu.MenuType = MenuType.Menu;
        articleTypeMenu.Name = "articleType";
        articleTypeMenu.Component = "operationManage/articles/type";
        articleTypeMenu.ComponentAlias = string.Empty;
        articleTypeMenu.IsLink = false;
        articleTypeMenu.MenuSort = 3;
        articleTypeMenu.Path = "/operationManage/articleManage/type";
        articleTypeMenu.Redirect = string.Empty;
        articleTypeMenu.PermissionIdentifying = string.Empty;
        articleTypeMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.articleType",
            Icon = "iconfont icon-wenzhangfenlei",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(articleTypeMenu);

        #region 查询

        Menu atSearchMenu = new Menu(_guidGenerator.Create());
        atSearchMenu.TenantId = tenant?.Id;
        atSearchMenu.PId = articleTypeMenu.Id;
        atSearchMenu.MenuType = MenuType.Button;
        atSearchMenu.Name = string.Empty;
        atSearchMenu.Component = string.Empty;
        atSearchMenu.ComponentAlias = string.Empty;
        atSearchMenu.IsLink = false;
        atSearchMenu.MenuSort = 1;
        atSearchMenu.Path = string.Empty;
        atSearchMenu.Redirect = string.Empty;
        atSearchMenu.PermissionIdentifying = GoodServicePermissions.ArticleTypes.Default;
        atSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(atSearchMenu);

        #endregion 查询

        #region 新增

        Menu atCreateMenu = new Menu(_guidGenerator.Create());
        atCreateMenu.TenantId = tenant?.Id;
        atCreateMenu.PId = articleTypeMenu.Id;
        atCreateMenu.MenuType = MenuType.Button;
        atCreateMenu.Name = string.Empty;
        atCreateMenu.Component = string.Empty;
        atCreateMenu.ComponentAlias = string.Empty;
        atCreateMenu.IsLink = false;
        atCreateMenu.MenuSort = 2;
        atCreateMenu.Path = string.Empty;
        atCreateMenu.Redirect = string.Empty;
        atCreateMenu.PermissionIdentifying = GoodServicePermissions.ArticleTypes.Create;
        atCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(atCreateMenu);

        #endregion 新增

        #region 修改

        Menu atUpdateMenu = new Menu(_guidGenerator.Create());
        atUpdateMenu.TenantId = tenant?.Id;
        atUpdateMenu.PId = articleTypeMenu.Id;
        atUpdateMenu.MenuType = MenuType.Button;
        atUpdateMenu.Name = string.Empty;
        atUpdateMenu.Component = string.Empty;
        atUpdateMenu.ComponentAlias = string.Empty;
        atUpdateMenu.IsLink = false;
        atUpdateMenu.MenuSort = 3;
        atUpdateMenu.Path = string.Empty;
        atUpdateMenu.Redirect = string.Empty;
        atUpdateMenu.PermissionIdentifying = GoodServicePermissions.ArticleTypes.Update;
        atUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(atUpdateMenu);

        #endregion 修改

        #region 删除

        Menu atDeleteMenu = new Menu(_guidGenerator.Create());
        atDeleteMenu.TenantId = tenant?.Id;
        atDeleteMenu.PId = articleTypeMenu.Id;
        atDeleteMenu.MenuType = MenuType.Button;
        atDeleteMenu.Name = string.Empty;
        atDeleteMenu.Component = string.Empty;
        atDeleteMenu.ComponentAlias = string.Empty;
        atDeleteMenu.IsLink = false;
        atDeleteMenu.MenuSort = 4;
        atDeleteMenu.Path = string.Empty;
        atDeleteMenu.Redirect = string.Empty;
        atDeleteMenu.PermissionIdentifying = GoodServicePermissions.ArticleTypes.Delete;
        atDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(atDeleteMenu);

        #endregion 删除

        #endregion 文章分类

        #region 文章列表

        Menu articlesMenu = new Menu(_guidGenerator.Create());
        articlesMenu.PId = articleManageMenu.Id;
        articlesMenu.TenantId = tenant?.Id;
        articlesMenu.MenuType = MenuType.Menu;
        articlesMenu.Name = "articles";
        articlesMenu.Component = "operationManage/articles/index";
        articlesMenu.ComponentAlias = string.Empty;
        articlesMenu.IsLink = false;
        articlesMenu.MenuSort = 4;
        articlesMenu.Path = "/operationManage/articleManage/index";
        articlesMenu.Redirect = string.Empty;
        articlesMenu.PermissionIdentifying = string.Empty;
        articlesMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.articles",
            Icon = "ele-Document",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(articlesMenu);

        #region 查询

        Menu articlesSearchMenu = new Menu(_guidGenerator.Create());
        articlesSearchMenu.TenantId = tenant?.Id;
        articlesSearchMenu.PId = articlesMenu.Id;
        articlesSearchMenu.MenuType = MenuType.Button;
        articlesSearchMenu.Name = string.Empty;
        articlesSearchMenu.Component = string.Empty;
        articlesSearchMenu.ComponentAlias = string.Empty;
        articlesSearchMenu.IsLink = false;
        articlesSearchMenu.MenuSort = 1;
        articlesSearchMenu.Path = string.Empty;
        articlesSearchMenu.Redirect = string.Empty;
        articlesSearchMenu.PermissionIdentifying = GoodServicePermissions.Articles.Default;
        articlesSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(articlesSearchMenu);

        #endregion 查询

        #region 新增

        Menu articlesCreateMenu = new Menu(_guidGenerator.Create());
        articlesCreateMenu.TenantId = tenant?.Id;
        articlesCreateMenu.PId = articlesMenu.Id;
        articlesCreateMenu.MenuType = MenuType.Button;
        articlesCreateMenu.Name = string.Empty;
        articlesCreateMenu.Component = string.Empty;
        articlesCreateMenu.ComponentAlias = string.Empty;
        articlesCreateMenu.IsLink = false;
        articlesCreateMenu.MenuSort = 2;
        articlesCreateMenu.Path = string.Empty;
        articlesCreateMenu.Redirect = string.Empty;
        articlesCreateMenu.PermissionIdentifying = GoodServicePermissions.Articles.Create;
        articlesCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(articlesCreateMenu);

        #endregion 新增

        #region 修改

        Menu articlesUpdateMenu = new Menu(_guidGenerator.Create());
        articlesUpdateMenu.TenantId = tenant?.Id;
        articlesUpdateMenu.PId = articlesMenu.Id;
        articlesUpdateMenu.MenuType = MenuType.Button;
        articlesUpdateMenu.Name = string.Empty;
        articlesUpdateMenu.Component = string.Empty;
        articlesUpdateMenu.ComponentAlias = string.Empty;
        articlesUpdateMenu.IsLink = false;
        articlesUpdateMenu.MenuSort = 3;
        articlesUpdateMenu.Path = string.Empty;
        articlesUpdateMenu.Redirect = string.Empty;
        articlesUpdateMenu.PermissionIdentifying = GoodServicePermissions.Articles.Update;
        articlesUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(articlesUpdateMenu);

        #endregion 修改

        #region 删除

        Menu articlesDeleteMenu = new Menu(_guidGenerator.Create());
        articlesDeleteMenu.TenantId = tenant?.Id;
        articlesDeleteMenu.PId = articlesMenu.Id;
        articlesDeleteMenu.MenuType = MenuType.Button;
        articlesDeleteMenu.Name = string.Empty;
        articlesDeleteMenu.Component = string.Empty;
        articlesDeleteMenu.ComponentAlias = string.Empty;
        articlesDeleteMenu.IsLink = false;
        articlesDeleteMenu.MenuSort = 4;
        articlesDeleteMenu.Path = string.Empty;
        articlesDeleteMenu.Redirect = string.Empty;
        articlesDeleteMenu.PermissionIdentifying = GoodServicePermissions.Articles.Delete;
        articlesDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(articlesDeleteMenu);

        #endregion 删除

        #endregion 文章列表

        #endregion 文章管理

        #endregion 运营管理

        #region 促销中心

        Menu promotionMenu = new Menu(_guidGenerator.Create());
        promotionMenu.TenantId = tenant?.Id;
        promotionMenu.MenuType = MenuType.Menu;
        promotionMenu.Name = "promotion";
        promotionMenu.Component = "layout/routerView/parent";
        promotionMenu.ComponentAlias = string.Empty;
        promotionMenu.IsLink = false;
        promotionMenu.MenuSort = 6;
        promotionMenu.Path = "/promotion";
        promotionMenu.Redirect = "/promotion/global";
        promotionMenu.PermissionIdentifying = string.Empty;
        promotionMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.promotion",
            Icon = "iconfont icon-cuxiaozhongxin",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(promotionMenu);

        #region 全局促销

        Menu gpMenu = new Menu(_guidGenerator.Create());
        gpMenu.PId = promotionMenu.Id;
        gpMenu.TenantId = tenant?.Id;
        gpMenu.MenuType = MenuType.Menu;
        gpMenu.Name = "globalPromotion";
        gpMenu.Component = "promotion/global/index";
        gpMenu.ComponentAlias = string.Empty;
        gpMenu.IsLink = false;
        gpMenu.MenuSort = 1;
        gpMenu.Path = "/promotion/global";
        gpMenu.Redirect = string.Empty;
        gpMenu.PermissionIdentifying = string.Empty;
        gpMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.globalPromotion",
            Icon = "iconfont icon-dianpuyingxiao",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(gpMenu);

        #region 查询

        Menu gpSearchMenu = new Menu(_guidGenerator.Create());
        gpSearchMenu.TenantId = tenant?.Id;
        gpSearchMenu.PId = gpMenu.Id;
        gpSearchMenu.MenuType = MenuType.Button;
        gpSearchMenu.Name = string.Empty;
        gpSearchMenu.Component = string.Empty;
        gpSearchMenu.ComponentAlias = string.Empty;
        gpSearchMenu.IsLink = false;
        gpSearchMenu.MenuSort = 1;
        gpSearchMenu.Path = string.Empty;
        gpSearchMenu.Redirect = string.Empty;
        gpSearchMenu.PermissionIdentifying = PromotionServicePermissions.Promotions.Default;
        gpSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(gpSearchMenu);

        #endregion 查询

        #region 新增

        Menu gpCreateMenu = new Menu(_guidGenerator.Create());
        gpCreateMenu.TenantId = tenant?.Id;
        gpCreateMenu.PId = gpMenu.Id;
        gpCreateMenu.MenuType = MenuType.Button;
        gpCreateMenu.Name = string.Empty;
        gpCreateMenu.Component = string.Empty;
        gpCreateMenu.ComponentAlias = string.Empty;
        gpCreateMenu.IsLink = false;
        gpCreateMenu.MenuSort = 2;
        gpCreateMenu.Path = string.Empty;
        gpCreateMenu.Redirect = string.Empty;
        gpCreateMenu.PermissionIdentifying = PromotionServicePermissions.Promotions.Create;
        gpCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(gpCreateMenu);

        #endregion 新增

        #region 设置参数

        Menu gpUpdateMenu = new Menu(_guidGenerator.Create());
        gpUpdateMenu.TenantId = tenant?.Id;
        gpUpdateMenu.PId = gpMenu.Id;
        gpUpdateMenu.MenuType = MenuType.Button;
        gpUpdateMenu.Name = string.Empty;
        gpUpdateMenu.Component = string.Empty;
        gpUpdateMenu.ComponentAlias = string.Empty;
        gpUpdateMenu.IsLink = false;
        gpUpdateMenu.MenuSort = 3;
        gpUpdateMenu.Path = string.Empty;
        gpUpdateMenu.Redirect = string.Empty;
        gpUpdateMenu.PermissionIdentifying = PromotionServicePermissions.Promotions.Update;
        gpUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.setParameters",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(gpUpdateMenu);

        #endregion 设置参数

        #region 删除

        Menu gpDeleteMenu = new Menu(_guidGenerator.Create());
        gpDeleteMenu.TenantId = tenant?.Id;
        gpDeleteMenu.PId = gpMenu.Id;
        gpDeleteMenu.MenuType = MenuType.Button;
        gpDeleteMenu.Name = string.Empty;
        gpDeleteMenu.Component = string.Empty;
        gpDeleteMenu.ComponentAlias = string.Empty;
        gpDeleteMenu.IsLink = false;
        gpDeleteMenu.MenuSort = 4;
        gpDeleteMenu.Path = string.Empty;
        gpDeleteMenu.Redirect = string.Empty;
        gpDeleteMenu.PermissionIdentifying = PromotionServicePermissions.Promotions.Delete;
        gpDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(gpDeleteMenu);

        #endregion 删除

        #endregion 全局促销

        #region 优惠劵

        Menu couponMenu = new Menu(_guidGenerator.Create());
        couponMenu.PId = promotionMenu.Id;
        couponMenu.TenantId = tenant?.Id;
        couponMenu.MenuType = MenuType.Menu;
        couponMenu.Name = "coupon";
        couponMenu.Component = "promotion/coupon/index";
        couponMenu.ComponentAlias = string.Empty;
        couponMenu.IsLink = false;
        couponMenu.MenuSort = 1;
        couponMenu.Path = "/promotion/coupon";
        couponMenu.Redirect = string.Empty;
        couponMenu.PermissionIdentifying = string.Empty;
        couponMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.coupon",
            Icon = "iconfont icon-youhuijuan",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(couponMenu);

        #region 查询

        Menu couponSearchMenu = new Menu(_guidGenerator.Create());
        couponSearchMenu.TenantId = tenant?.Id;
        couponSearchMenu.PId = couponMenu.Id;
        couponSearchMenu.MenuType = MenuType.Button;
        couponSearchMenu.Name = string.Empty;
        couponSearchMenu.Component = string.Empty;
        couponSearchMenu.ComponentAlias = string.Empty;
        couponSearchMenu.IsLink = false;
        couponSearchMenu.MenuSort = 1;
        couponSearchMenu.Path = string.Empty;
        couponSearchMenu.Redirect = string.Empty;
        couponSearchMenu.PermissionIdentifying = PromotionServicePermissions.Coupons.Default;
        couponSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(couponSearchMenu);

        #endregion 查询

        #region 新增

        Menu couponCreateMenu = new Menu(_guidGenerator.Create());
        couponCreateMenu.TenantId = tenant?.Id;
        couponCreateMenu.PId = couponMenu.Id;
        couponCreateMenu.MenuType = MenuType.Button;
        couponCreateMenu.Name = string.Empty;
        couponCreateMenu.Component = string.Empty;
        couponCreateMenu.ComponentAlias = string.Empty;
        couponCreateMenu.IsLink = false;
        couponCreateMenu.MenuSort = 2;
        couponCreateMenu.Path = string.Empty;
        couponCreateMenu.Redirect = string.Empty;
        couponCreateMenu.PermissionIdentifying = PromotionServicePermissions.Coupons.Create;
        couponCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(couponCreateMenu);

        #endregion 新增

        #region 设置参数

        Menu couponUpdateMenu = new Menu(_guidGenerator.Create());
        couponUpdateMenu.TenantId = tenant?.Id;
        couponUpdateMenu.PId = couponMenu.Id;
        couponUpdateMenu.MenuType = MenuType.Button;
        couponUpdateMenu.Name = string.Empty;
        couponUpdateMenu.Component = string.Empty;
        couponUpdateMenu.ComponentAlias = string.Empty;
        couponUpdateMenu.IsLink = false;
        couponUpdateMenu.MenuSort = 3;
        couponUpdateMenu.Path = string.Empty;
        couponUpdateMenu.Redirect = string.Empty;
        couponUpdateMenu.PermissionIdentifying = PromotionServicePermissions.Coupons.Update;
        couponUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.setParameters",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(couponUpdateMenu);

        #endregion 设置参数

        #region 删除

        Menu couponDeleteMenu = new Menu(_guidGenerator.Create());
        couponDeleteMenu.TenantId = tenant?.Id;
        couponDeleteMenu.PId = couponMenu.Id;
        couponDeleteMenu.MenuType = MenuType.Button;
        couponDeleteMenu.Name = string.Empty;
        couponDeleteMenu.Component = string.Empty;
        couponDeleteMenu.ComponentAlias = string.Empty;
        couponDeleteMenu.IsLink = false;
        couponDeleteMenu.MenuSort = 4;
        couponDeleteMenu.Path = string.Empty;
        couponDeleteMenu.Redirect = string.Empty;
        couponDeleteMenu.PermissionIdentifying = PromotionServicePermissions.Coupons.Delete;
        couponDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(couponDeleteMenu);

        #endregion 删除

        #region 卷码列表

        Menu clDeleteMenu = new Menu(_guidGenerator.Create());
        clDeleteMenu.TenantId = tenant?.Id;
        clDeleteMenu.PId = couponMenu.Id;
        clDeleteMenu.MenuType = MenuType.Button;
        clDeleteMenu.Name = string.Empty;
        clDeleteMenu.Component = string.Empty;
        clDeleteMenu.ComponentAlias = string.Empty;
        clDeleteMenu.IsLink = false;
        clDeleteMenu.MenuSort = 5;
        clDeleteMenu.Path = string.Empty;
        clDeleteMenu.Redirect = string.Empty;
        clDeleteMenu.PermissionIdentifying = PromotionServicePermissions.Coupons.Couponlist;
        clDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.couponlist",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(clDeleteMenu);

        #endregion 卷码列表

        #endregion 优惠劵

        #region 商品秒杀

        Menu gsMenu = new Menu(_guidGenerator.Create());
        gsMenu.PId = promotionMenu.Id;
        gsMenu.TenantId = tenant?.Id;
        gsMenu.MenuType = MenuType.Menu;
        gsMenu.Name = "groupSeckill";
        gsMenu.Component = "promotion/groupSeckill/index";
        gsMenu.ComponentAlias = string.Empty;
        gsMenu.IsLink = false;
        gsMenu.MenuSort = 3;
        gsMenu.Path = "/promotion/groupSeckill";
        gsMenu.Redirect = string.Empty;
        gsMenu.PermissionIdentifying = string.Empty;
        gsMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.groupSeckill",
            Icon = "iconfont icon-yingxiaogongju-tuangoumiaosha1",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(gsMenu);

        #region 查询

        Menu gsSearchMenu = new Menu(_guidGenerator.Create());
        gsSearchMenu.TenantId = tenant?.Id;
        gsSearchMenu.PId = gsMenu.Id;
        gsSearchMenu.MenuType = MenuType.Button;
        gsSearchMenu.Name = string.Empty;
        gsSearchMenu.Component = string.Empty;
        gsSearchMenu.ComponentAlias = string.Empty;
        gsSearchMenu.IsLink = false;
        gsSearchMenu.MenuSort = 1;
        gsSearchMenu.Path = string.Empty;
        gsSearchMenu.Redirect = string.Empty;
        gsSearchMenu.PermissionIdentifying = PromotionServicePermissions.GroupSeckills.Default;
        gsSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(gsSearchMenu);

        #endregion 查询

        #region 新增

        Menu gsCreateMenu = new Menu(_guidGenerator.Create());
        gsCreateMenu.TenantId = tenant?.Id;
        gsCreateMenu.PId = gsMenu.Id;
        gsCreateMenu.MenuType = MenuType.Button;
        gsCreateMenu.Name = string.Empty;
        gsCreateMenu.Component = string.Empty;
        gsCreateMenu.ComponentAlias = string.Empty;
        gsCreateMenu.IsLink = false;
        gsCreateMenu.MenuSort = 2;
        gsCreateMenu.Path = string.Empty;
        gsCreateMenu.Redirect = string.Empty;
        gsCreateMenu.PermissionIdentifying = PromotionServicePermissions.GroupSeckills.Create;
        gsCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(gsCreateMenu);

        #endregion 新增

        #region 设置参数

        Menu gsUpdateMenu = new Menu(_guidGenerator.Create());
        gsUpdateMenu.TenantId = tenant?.Id;
        gsUpdateMenu.PId = gsMenu.Id;
        gsUpdateMenu.MenuType = MenuType.Button;
        gsUpdateMenu.Name = string.Empty;
        gsUpdateMenu.Component = string.Empty;
        gsUpdateMenu.ComponentAlias = string.Empty;
        gsUpdateMenu.IsLink = false;
        gsUpdateMenu.MenuSort = 3;
        gsUpdateMenu.Path = string.Empty;
        gsUpdateMenu.Redirect = string.Empty;
        gsUpdateMenu.PermissionIdentifying = PromotionServicePermissions.GroupSeckills.Update;
        gsUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.setParameters",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(gsUpdateMenu);

        #endregion 设置参数

        #region 删除

        Menu gsDeleteMenu = new Menu(_guidGenerator.Create());
        gsDeleteMenu.TenantId = tenant?.Id;
        gsDeleteMenu.PId = gsMenu.Id;
        gsDeleteMenu.MenuType = MenuType.Button;
        gsDeleteMenu.Name = string.Empty;
        gsDeleteMenu.Component = string.Empty;
        gsDeleteMenu.ComponentAlias = string.Empty;
        gsDeleteMenu.IsLink = false;
        gsDeleteMenu.MenuSort = 4;
        gsDeleteMenu.Path = string.Empty;
        gsDeleteMenu.Redirect = string.Empty;
        gsDeleteMenu.PermissionIdentifying = PromotionServicePermissions.GroupSeckills.Delete;
        gsDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(couponDeleteMenu);

        #endregion 删除

        #endregion 商品秒杀

        #endregion 促销中心

        #region 财务管理

        Menu fManageMenu = new Menu(_guidGenerator.Create());
        fManageMenu.TenantId = tenant?.Id;
        fManageMenu.MenuType = MenuType.Menu;
        fManageMenu.Name = "financialManage";
        fManageMenu.Component = "layout/routerView/parent";
        fManageMenu.ComponentAlias = string.Empty;
        fManageMenu.IsLink = false;
        fManageMenu.MenuSort = 7;
        fManageMenu.Path = "/financialManage";
        fManageMenu.Redirect = "/financialManage/payment";
        fManageMenu.PermissionIdentifying = string.Empty;
        fManageMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.financialManage",
            Icon = "iconfont icon-caiwuguanli",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(fManageMenu);

        #region 支付方式

        Menu paymentMenu = new Menu(_guidGenerator.Create());
        paymentMenu.TenantId = tenant?.Id;
        paymentMenu.PId = fManageMenu.Id;
        paymentMenu.MenuType = MenuType.Menu;
        paymentMenu.Name = "payment";
        paymentMenu.Component = "financialManage/payment/index";
        paymentMenu.ComponentAlias = string.Empty;
        paymentMenu.IsLink = false;
        paymentMenu.MenuSort = 1;
        paymentMenu.Path = "/financialManage/payment";
        paymentMenu.Redirect = string.Empty;
        paymentMenu.PermissionIdentifying = string.Empty;
        paymentMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.payment",
            Icon = "iconfont icon-zhifufangshi",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(paymentMenu);

        #region 查询

        Menu paymentSearchMenu = new Menu(_guidGenerator.Create());
        paymentSearchMenu.TenantId = tenant?.Id;
        paymentSearchMenu.PId = paymentMenu.Id;
        paymentSearchMenu.MenuType = MenuType.Button;
        paymentSearchMenu.Name = string.Empty;
        paymentSearchMenu.Component = string.Empty;
        paymentSearchMenu.ComponentAlias = string.Empty;
        paymentSearchMenu.IsLink = false;
        paymentSearchMenu.MenuSort = 1;
        paymentSearchMenu.Path = string.Empty;
        paymentSearchMenu.Redirect = string.Empty;
        paymentSearchMenu.PermissionIdentifying = PaymentServicePermissions.Payments.Default;
        paymentSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(paymentSearchMenu);

        #endregion 查询

        #endregion 支付方式

        #region 支付单据

        Menu bPaymentMenu = new Menu(_guidGenerator.Create());
        bPaymentMenu.TenantId = tenant?.Id;
        bPaymentMenu.PId = fManageMenu.Id;
        bPaymentMenu.MenuType = MenuType.Menu;
        bPaymentMenu.Name = "billPayment";
        bPaymentMenu.Component = "financialManage/bill/index";
        bPaymentMenu.ComponentAlias = string.Empty;
        bPaymentMenu.IsLink = false;
        bPaymentMenu.MenuSort = 2;
        bPaymentMenu.Path = "/financialManage/billPayment";
        bPaymentMenu.Redirect = string.Empty;
        bPaymentMenu.PermissionIdentifying = string.Empty;
        bPaymentMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.billPayment",
            Icon = "iconfont icon-zhifudanju",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(bPaymentMenu);

        #region 查询

        Menu bPaymentSearchMenu = new Menu(_guidGenerator.Create());
        bPaymentSearchMenu.TenantId = tenant?.Id;
        bPaymentSearchMenu.PId = bPaymentMenu.Id;
        bPaymentSearchMenu.MenuType = MenuType.Button;
        bPaymentSearchMenu.Name = string.Empty;
        bPaymentSearchMenu.Component = string.Empty;
        bPaymentSearchMenu.ComponentAlias = string.Empty;
        bPaymentSearchMenu.IsLink = false;
        bPaymentSearchMenu.MenuSort = 1;
        bPaymentSearchMenu.Path = string.Empty;
        bPaymentSearchMenu.Redirect = string.Empty;
        bPaymentSearchMenu.PermissionIdentifying = PaymentServicePermissions.BillPayments.Default;
        bPaymentSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(bPaymentSearchMenu);

        #endregion 查询

        #endregion 支付单据

        #region 退款单列表

        Menu bRefundMenu = new Menu(_guidGenerator.Create());
        bRefundMenu.TenantId = tenant?.Id;
        bRefundMenu.PId = fManageMenu.Id;
        bRefundMenu.MenuType = MenuType.Menu;
        bRefundMenu.Name = "billRefund";
        bRefundMenu.Component = "financialManage/billRefund/index";
        bRefundMenu.ComponentAlias = string.Empty;
        bRefundMenu.IsLink = false;
        bRefundMenu.MenuSort = 3;
        bRefundMenu.Path = "/financialManage/billRefund";
        bRefundMenu.Redirect = string.Empty;
        bRefundMenu.PermissionIdentifying = string.Empty;
        bRefundMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.billRefund",
            Icon = "ele-Memo",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(bRefundMenu);

        #region 查询

        Menu bRefundSearchMenu = new Menu(_guidGenerator.Create());
        bRefundSearchMenu.TenantId = tenant?.Id;
        bRefundSearchMenu.PId = bRefundMenu.Id;
        bRefundSearchMenu.MenuType = MenuType.Button;
        bRefundSearchMenu.Name = string.Empty;
        bRefundSearchMenu.Component = string.Empty;
        bRefundSearchMenu.ComponentAlias = string.Empty;
        bRefundSearchMenu.IsLink = false;
        bRefundSearchMenu.MenuSort = 1;
        bRefundSearchMenu.Path = string.Empty;
        bRefundSearchMenu.Redirect = string.Empty;
        bRefundSearchMenu.PermissionIdentifying = PaymentServicePermissions.BillRefunds.Default;
        bRefundSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(bRefundSearchMenu);

        #endregion 查询

        #region 审核退款

        Menu auditRefundSearchMenu = new Menu(_guidGenerator.Create());
        auditRefundSearchMenu.TenantId = tenant?.Id;
        auditRefundSearchMenu.PId = bRefundMenu.Id;
        auditRefundSearchMenu.MenuType = MenuType.Button;
        auditRefundSearchMenu.Name = string.Empty;
        auditRefundSearchMenu.Component = string.Empty;
        auditRefundSearchMenu.ComponentAlias = string.Empty;
        auditRefundSearchMenu.IsLink = false;
        auditRefundSearchMenu.MenuSort = 2;
        auditRefundSearchMenu.Path = string.Empty;
        auditRefundSearchMenu.Redirect = string.Empty;
        auditRefundSearchMenu.PermissionIdentifying = PaymentServicePermissions.BillRefunds.AuditRefund;
        auditRefundSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.auditRefund",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(auditRefundSearchMenu);

        #endregion 审核退款

        #endregion 退款单列表

        #endregion 财务管理

        #region 商城设置

        Menu shopSettingMenu = new Menu(_guidGenerator.Create());
        shopSettingMenu.TenantId = tenant?.Id;
        shopSettingMenu.MenuType = MenuType.Menu;
        shopSettingMenu.Name = "shopSetting";
        shopSettingMenu.Component = "layout/routerView/parent";
        shopSettingMenu.ComponentAlias = string.Empty;
        shopSettingMenu.IsLink = false;
        shopSettingMenu.MenuSort = 8;
        shopSettingMenu.Path = "/shopSetting";
        shopSettingMenu.Redirect = string.Empty;
        shopSettingMenu.PermissionIdentifying = string.Empty;
        shopSettingMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.shopSetting",
            Icon = "iconfont icon-shangchengshezhi",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(shopSettingMenu);

        #region 平台设置

        Menu platformSettingMenu = new Menu(_guidGenerator.Create());
        platformSettingMenu.TenantId = tenant?.Id;
        platformSettingMenu.PId = shopSettingMenu.Id;
        platformSettingMenu.MenuType = MenuType.Menu;
        platformSettingMenu.Name = "platformSetting";
        platformSettingMenu.Component = "shopSetting/platform/index";
        platformSettingMenu.ComponentAlias = string.Empty;
        platformSettingMenu.IsLink = false;
        platformSettingMenu.MenuSort = 1;
        platformSettingMenu.Path = "/shopSetting/platform";
        platformSettingMenu.Redirect = string.Empty;
        platformSettingMenu.PermissionIdentifying = string.Empty;
        platformSettingMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.platformSetting",
            Icon = "iconfont icon-pingtaishezhi",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(platformSettingMenu);

        #region 平台管理

        Menu platformManageMenu = new Menu(_guidGenerator.Create());
        platformManageMenu.TenantId = tenant?.Id;
        platformManageMenu.PId = platformSettingMenu.Id;
        platformManageMenu.MenuType = MenuType.Button;
        platformManageMenu.Name = string.Empty;
        platformManageMenu.Component = string.Empty;
        platformManageMenu.ComponentAlias = string.Empty;
        platformManageMenu.IsLink = false;
        platformManageMenu.MenuSort = 1;
        platformManageMenu.Path = string.Empty;
        platformManageMenu.Redirect = string.Empty;
        platformManageMenu.PermissionIdentifying = BaseServicePermissions.PlatformSettings.Default;
        platformManageMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.platformManage",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(platformManageMenu);

        #endregion 平台管理

        #endregion 平台设置

        #region 门店管理

        Menu storeMenu = new Menu(_guidGenerator.Create());
        storeMenu.TenantId = tenant?.Id;
        storeMenu.PId = shopSettingMenu.Id;
        storeMenu.MenuType = MenuType.Menu;
        storeMenu.Name = "store";
        storeMenu.Component = "shopSetting/store/index";
        storeMenu.ComponentAlias = string.Empty;
        storeMenu.IsLink = false;
        storeMenu.MenuSort = 2;
        storeMenu.Path = "/shopSetting/store";
        storeMenu.Redirect = string.Empty;
        storeMenu.PermissionIdentifying = string.Empty;
        storeMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.store",
            Icon = "iconfont icon-8",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(storeMenu);

        #region 查询

        Menu storeSearchMenu = new Menu(_guidGenerator.Create());
        storeSearchMenu.TenantId = tenant?.Id;
        storeSearchMenu.PId = storeMenu.Id;
        storeSearchMenu.MenuType = MenuType.Button;
        storeSearchMenu.Name = string.Empty;
        storeSearchMenu.Component = string.Empty;
        storeSearchMenu.ComponentAlias = string.Empty;
        storeSearchMenu.IsLink = false;
        storeSearchMenu.MenuSort = 1;
        storeSearchMenu.Path = string.Empty;
        storeSearchMenu.Redirect = string.Empty;
        storeSearchMenu.PermissionIdentifying = GoodServicePermissions.Stores.Default;
        storeSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(storeSearchMenu);

        #endregion 查询

        #region 新增

        Menu storeCreateMenu = new Menu(_guidGenerator.Create());
        storeCreateMenu.TenantId = tenant?.Id;
        storeCreateMenu.PId = storeMenu.Id;
        storeCreateMenu.MenuType = MenuType.Button;
        storeCreateMenu.Name = string.Empty;
        storeCreateMenu.Component = string.Empty;
        storeCreateMenu.ComponentAlias = string.Empty;
        storeCreateMenu.IsLink = false;
        storeCreateMenu.MenuSort = 2;
        storeCreateMenu.Path = string.Empty;
        storeCreateMenu.Redirect = string.Empty;
        storeCreateMenu.PermissionIdentifying = GoodServicePermissions.Stores.Create;
        storeCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(storeCreateMenu);

        #endregion 新增

        #region 修改

        Menu storeUpdateMenu = new Menu(_guidGenerator.Create());
        storeUpdateMenu.TenantId = tenant?.Id;
        storeUpdateMenu.PId = storeMenu.Id;
        storeUpdateMenu.MenuType = MenuType.Button;
        storeUpdateMenu.Name = string.Empty;
        storeUpdateMenu.Component = string.Empty;
        storeUpdateMenu.ComponentAlias = string.Empty;
        storeUpdateMenu.IsLink = false;
        storeUpdateMenu.MenuSort = 3;
        storeUpdateMenu.Path = string.Empty;
        storeUpdateMenu.Redirect = string.Empty;
        storeUpdateMenu.PermissionIdentifying = GoodServicePermissions.Stores.Update;
        storeUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderUpdateMenu);

        #endregion 修改

        #region 删除

        Menu storeDeleteMenu = new Menu(_guidGenerator.Create());
        storeDeleteMenu.TenantId = tenant?.Id;
        storeDeleteMenu.PId = storeMenu.Id;
        storeDeleteMenu.MenuType = MenuType.Button;
        storeDeleteMenu.Name = string.Empty;
        storeDeleteMenu.Component = string.Empty;
        storeDeleteMenu.ComponentAlias = string.Empty;
        storeDeleteMenu.IsLink = false;
        storeDeleteMenu.MenuSort = 4;
        storeDeleteMenu.Path = string.Empty;
        storeDeleteMenu.Redirect = string.Empty;
        storeDeleteMenu.PermissionIdentifying = GoodServicePermissions.Stores.Delete;
        storeDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(storeDeleteMenu);

        #endregion 删除

        #endregion 门店管理

        #region 店铺店员关联

        Menu storeClerkMenu = new Menu(_guidGenerator.Create());
        storeClerkMenu.TenantId = tenant?.Id;
        storeClerkMenu.PId = shopSettingMenu.Id;
        storeClerkMenu.MenuType = MenuType.Menu;
        storeClerkMenu.Name = "storeClerk";
        storeClerkMenu.Component = "shopSetting/storeClerk/index";
        storeClerkMenu.ComponentAlias = string.Empty;
        storeClerkMenu.IsLink = false;
        storeClerkMenu.MenuSort = 3;
        storeClerkMenu.Path = "/shopSetting/storeClerk";
        storeClerkMenu.Redirect = string.Empty;
        storeClerkMenu.PermissionIdentifying = string.Empty;
        storeClerkMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.storeClerk",
            Icon = "fa fa-puzzle-piece",
            IsHide = true,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(storeClerkMenu);

        #region 查询

        Menu storeClerkSearchMenu = new Menu(_guidGenerator.Create());
        storeClerkSearchMenu.TenantId = tenant?.Id;
        storeClerkSearchMenu.PId = storeClerkMenu.Id;
        storeClerkSearchMenu.MenuType = MenuType.Button;
        storeClerkSearchMenu.Name = string.Empty;
        storeClerkSearchMenu.Component = string.Empty;
        storeClerkSearchMenu.ComponentAlias = string.Empty;
        storeClerkSearchMenu.IsLink = false;
        storeClerkSearchMenu.MenuSort = 1;
        storeClerkSearchMenu.Path = string.Empty;
        storeClerkSearchMenu.Redirect = string.Empty;
        storeClerkSearchMenu.PermissionIdentifying = GoodServicePermissions.StoreClerks.Default;
        storeClerkSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(storeClerkSearchMenu);

        #endregion 查询

        #region 新增

        Menu storeClerkCreateMenu = new Menu(_guidGenerator.Create());
        storeClerkCreateMenu.TenantId = tenant?.Id;
        storeClerkCreateMenu.PId = storeClerkMenu.Id;
        storeClerkCreateMenu.MenuType = MenuType.Button;
        storeClerkCreateMenu.Name = string.Empty;
        storeClerkCreateMenu.Component = string.Empty;
        storeClerkCreateMenu.ComponentAlias = string.Empty;
        storeClerkCreateMenu.IsLink = false;
        storeClerkCreateMenu.MenuSort = 2;
        storeClerkCreateMenu.Path = string.Empty;
        storeClerkCreateMenu.Redirect = string.Empty;
        storeClerkCreateMenu.PermissionIdentifying = GoodServicePermissions.StoreClerks.Create;
        storeClerkCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(storeClerkCreateMenu);

        #endregion 新增

        #region 修改

        Menu storeClerkUpdateMenu = new Menu(_guidGenerator.Create());
        storeClerkUpdateMenu.TenantId = tenant?.Id;
        storeClerkUpdateMenu.PId = storeClerkMenu.Id;
        storeClerkUpdateMenu.MenuType = MenuType.Button;
        storeClerkUpdateMenu.Name = string.Empty;
        storeClerkUpdateMenu.Component = string.Empty;
        storeClerkUpdateMenu.ComponentAlias = string.Empty;
        storeClerkUpdateMenu.IsLink = false;
        storeClerkUpdateMenu.MenuSort = 3;
        storeClerkUpdateMenu.Path = string.Empty;
        storeClerkUpdateMenu.Redirect = string.Empty;
        storeClerkUpdateMenu.PermissionIdentifying = GoodServicePermissions.StoreClerks.Update;
        storeClerkUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderUpdateMenu);

        #endregion 修改

        #region 删除

        Menu storeClerkDeleteMenu = new Menu(_guidGenerator.Create());
        storeClerkDeleteMenu.TenantId = tenant?.Id;
        storeClerkDeleteMenu.PId = storeClerkMenu.Id;
        storeClerkDeleteMenu.MenuType = MenuType.Button;
        storeClerkDeleteMenu.Name = string.Empty;
        storeClerkDeleteMenu.Component = string.Empty;
        storeClerkDeleteMenu.ComponentAlias = string.Empty;
        storeClerkDeleteMenu.IsLink = false;
        storeClerkDeleteMenu.MenuSort = 4;
        storeClerkDeleteMenu.Path = string.Empty;
        storeClerkDeleteMenu.Redirect = string.Empty;
        storeClerkDeleteMenu.PermissionIdentifying = GoodServicePermissions.StoreClerks.Delete;
        storeClerkDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(storeClerkDeleteMenu);

        #endregion 删除

        #endregion 店铺店员关联

        #region 页面设计

        Menu pageDesignMenu = new Menu(_guidGenerator.Create());
        pageDesignMenu.TenantId = tenant?.Id;
        pageDesignMenu.PId = shopSettingMenu.Id;
        pageDesignMenu.MenuType = MenuType.Menu;
        pageDesignMenu.Name = "pageDesign";
        pageDesignMenu.Component = "shopSetting/pageDesign/index";
        pageDesignMenu.ComponentAlias = string.Empty;
        pageDesignMenu.IsLink = false;
        pageDesignMenu.MenuSort = 4;
        pageDesignMenu.Path = "/shopSetting/pageDesign";
        pageDesignMenu.Redirect = string.Empty;
        pageDesignMenu.PermissionIdentifying = string.Empty;
        pageDesignMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.pageDesign",
            Icon = "iconfont icon-webyemiansheji",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(pageDesignMenu);

        #region 查询

        Menu pageDesignSearchMenu = new Menu(_guidGenerator.Create());
        pageDesignSearchMenu.TenantId = tenant?.Id;
        pageDesignSearchMenu.PId = pageDesignMenu.Id;
        pageDesignSearchMenu.MenuType = MenuType.Button;
        pageDesignSearchMenu.Name = string.Empty;
        pageDesignSearchMenu.Component = string.Empty;
        pageDesignSearchMenu.ComponentAlias = string.Empty;
        pageDesignSearchMenu.IsLink = false;
        pageDesignSearchMenu.MenuSort = 1;
        pageDesignSearchMenu.Path = string.Empty;
        pageDesignSearchMenu.Redirect = string.Empty;
        pageDesignSearchMenu.PermissionIdentifying = GoodServicePermissions.PageDesigns.Default;
        pageDesignSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(pageDesignSearchMenu);

        #endregion 查询

        #region 新增

        Menu pageDesignCreateMenu = new Menu(_guidGenerator.Create());
        pageDesignCreateMenu.TenantId = tenant?.Id;
        pageDesignCreateMenu.PId = pageDesignMenu.Id;
        pageDesignCreateMenu.MenuType = MenuType.Button;
        pageDesignCreateMenu.Name = string.Empty;
        pageDesignCreateMenu.Component = string.Empty;
        pageDesignCreateMenu.ComponentAlias = string.Empty;
        pageDesignCreateMenu.IsLink = false;
        pageDesignCreateMenu.MenuSort = 2;
        pageDesignCreateMenu.Path = string.Empty;
        pageDesignCreateMenu.Redirect = string.Empty;
        pageDesignCreateMenu.PermissionIdentifying = GoodServicePermissions.PageDesigns.Create;
        pageDesignCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(pageDesignCreateMenu);

        #endregion 新增

        #region 修改

        Menu pageDesignUpdateMenu = new Menu(_guidGenerator.Create());
        pageDesignUpdateMenu.TenantId = tenant?.Id;
        pageDesignUpdateMenu.PId = pageDesignMenu.Id;
        pageDesignUpdateMenu.MenuType = MenuType.Button;
        pageDesignUpdateMenu.Name = string.Empty;
        pageDesignUpdateMenu.Component = string.Empty;
        pageDesignUpdateMenu.ComponentAlias = string.Empty;
        pageDesignUpdateMenu.IsLink = false;
        pageDesignUpdateMenu.MenuSort = 3;
        pageDesignUpdateMenu.Path = string.Empty;
        pageDesignUpdateMenu.Redirect = string.Empty;
        pageDesignUpdateMenu.PermissionIdentifying = GoodServicePermissions.PageDesigns.Update;
        pageDesignUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderUpdateMenu);

        #endregion 修改

        #region 删除

        Menu pageDesignDeleteMenu = new Menu(_guidGenerator.Create());
        pageDesignDeleteMenu.TenantId = tenant?.Id;
        pageDesignDeleteMenu.PId = pageDesignMenu.Id;
        pageDesignDeleteMenu.MenuType = MenuType.Button;
        pageDesignDeleteMenu.Name = string.Empty;
        pageDesignDeleteMenu.Component = string.Empty;
        pageDesignDeleteMenu.ComponentAlias = string.Empty;
        pageDesignDeleteMenu.IsLink = false;
        pageDesignDeleteMenu.MenuSort = 4;
        pageDesignDeleteMenu.Path = string.Empty;
        pageDesignDeleteMenu.Redirect = string.Empty;
        pageDesignDeleteMenu.PermissionIdentifying = GoodServicePermissions.PageDesigns.Delete;
        pageDesignDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(pageDesignDeleteMenu);

        #endregion 删除

        #region 预览

        Menu pageDesignPreviewMenu = new Menu(_guidGenerator.Create());
        pageDesignPreviewMenu.TenantId = tenant?.Id;
        pageDesignPreviewMenu.PId = pageDesignMenu.Id;
        pageDesignPreviewMenu.MenuType = MenuType.Button;
        pageDesignPreviewMenu.Name = string.Empty;
        pageDesignPreviewMenu.Component = string.Empty;
        pageDesignPreviewMenu.ComponentAlias = string.Empty;
        pageDesignPreviewMenu.IsLink = false;
        pageDesignPreviewMenu.MenuSort = 5;
        pageDesignPreviewMenu.Path = string.Empty;
        pageDesignPreviewMenu.Redirect = string.Empty;
        pageDesignPreviewMenu.PermissionIdentifying = GoodServicePermissions.PageDesigns.Preview;
        pageDesignPreviewMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.preview",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(pageDesignPreviewMenu);

        #endregion 预览

        #region 版面设计

        Menu pageDesignLayoutMenu = new Menu(_guidGenerator.Create());
        pageDesignLayoutMenu.TenantId = tenant?.Id;
        pageDesignLayoutMenu.PId = pageDesignMenu.Id;
        pageDesignLayoutMenu.MenuType = MenuType.Menu;
        pageDesignLayoutMenu.Name = "layout";
        pageDesignLayoutMenu.Component = "shopSetting/pageDesign/layout";
        pageDesignLayoutMenu.ComponentAlias = string.Empty;
        pageDesignLayoutMenu.IsLink = false;
        pageDesignLayoutMenu.MenuSort = 6;
        pageDesignLayoutMenu.Path = "/shopSetting/pageDesign/layout";
        pageDesignLayoutMenu.Redirect = string.Empty;
        pageDesignLayoutMenu.PermissionIdentifying = GoodServicePermissions.PageDesigns.Layout;
        pageDesignLayoutMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.layout",
            Icon = "ele-Cellphone",
            IsHide = true,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(pageDesignLayoutMenu);

        #endregion 版面设计

        #region 复制

        Menu pageDesignCopyMenu = new Menu(_guidGenerator.Create());
        pageDesignCopyMenu.TenantId = tenant?.Id;
        pageDesignCopyMenu.PId = pageDesignMenu.Id;
        pageDesignCopyMenu.MenuType = MenuType.Button;
        pageDesignCopyMenu.Name = string.Empty;
        pageDesignCopyMenu.Component = string.Empty;
        pageDesignCopyMenu.ComponentAlias = string.Empty;
        pageDesignCopyMenu.IsLink = false;
        pageDesignCopyMenu.MenuSort = 7;
        pageDesignCopyMenu.Path = string.Empty;
        pageDesignCopyMenu.Redirect = string.Empty;
        pageDesignCopyMenu.PermissionIdentifying = GoodServicePermissions.PageDesigns.Copy;
        pageDesignCopyMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.copy",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(pageDesignCopyMenu);

        #endregion 复制

        #endregion 页面设计

        #region 商城服务描述

        Menu serviceDescriptionMenu = new Menu(_guidGenerator.Create());
        serviceDescriptionMenu.TenantId = tenant?.Id;
        serviceDescriptionMenu.PId = shopSettingMenu.Id;
        serviceDescriptionMenu.MenuType = MenuType.Menu;
        serviceDescriptionMenu.Name = "serviceDescription";
        serviceDescriptionMenu.Component = "shopSetting/serviceDescription/index";
        serviceDescriptionMenu.ComponentAlias = string.Empty;
        serviceDescriptionMenu.IsLink = false;
        serviceDescriptionMenu.MenuSort = 5;
        serviceDescriptionMenu.Path = "/shopSetting/serviceDescription";
        serviceDescriptionMenu.Redirect = string.Empty;
        serviceDescriptionMenu.PermissionIdentifying = string.Empty;
        serviceDescriptionMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.serviceDescription",
            Icon = "iconfont icon-icon",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(serviceDescriptionMenu);

        #region 查询

        Menu serviceDescriptionSearchMenu = new Menu(_guidGenerator.Create());
        serviceDescriptionSearchMenu.TenantId = tenant?.Id;
        serviceDescriptionSearchMenu.PId = serviceDescriptionMenu.Id;
        serviceDescriptionSearchMenu.MenuType = MenuType.Button;
        serviceDescriptionSearchMenu.Name = string.Empty;
        serviceDescriptionSearchMenu.Component = string.Empty;
        serviceDescriptionSearchMenu.ComponentAlias = string.Empty;
        serviceDescriptionSearchMenu.IsLink = false;
        serviceDescriptionSearchMenu.MenuSort = 1;
        serviceDescriptionSearchMenu.Path = string.Empty;
        serviceDescriptionSearchMenu.Redirect = string.Empty;
        serviceDescriptionSearchMenu.PermissionIdentifying = GoodServicePermissions.ServiceDescriptions.Default;
        serviceDescriptionSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(serviceDescriptionSearchMenu);

        #endregion 查询

        #region 新增

        Menu serviceDescriptionCreateMenu = new Menu(_guidGenerator.Create());
        serviceDescriptionCreateMenu.TenantId = tenant?.Id;
        serviceDescriptionCreateMenu.PId = serviceDescriptionMenu.Id;
        serviceDescriptionCreateMenu.MenuType = MenuType.Button;
        serviceDescriptionCreateMenu.Name = string.Empty;
        serviceDescriptionCreateMenu.Component = string.Empty;
        serviceDescriptionCreateMenu.ComponentAlias = string.Empty;
        serviceDescriptionCreateMenu.IsLink = false;
        serviceDescriptionCreateMenu.MenuSort = 2;
        serviceDescriptionCreateMenu.Path = string.Empty;
        serviceDescriptionCreateMenu.Redirect = string.Empty;
        serviceDescriptionCreateMenu.PermissionIdentifying = GoodServicePermissions.ServiceDescriptions.Create;
        serviceDescriptionCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(serviceDescriptionCreateMenu);

        #endregion 新增

        #region 修改

        Menu serviceDescriptionUpdateMenu = new Menu(_guidGenerator.Create());
        serviceDescriptionUpdateMenu.TenantId = tenant?.Id;
        serviceDescriptionUpdateMenu.PId = serviceDescriptionMenu.Id;
        serviceDescriptionUpdateMenu.MenuType = MenuType.Button;
        serviceDescriptionUpdateMenu.Name = string.Empty;
        serviceDescriptionUpdateMenu.Component = string.Empty;
        serviceDescriptionUpdateMenu.ComponentAlias = string.Empty;
        serviceDescriptionUpdateMenu.IsLink = false;
        serviceDescriptionUpdateMenu.MenuSort = 3;
        serviceDescriptionUpdateMenu.Path = string.Empty;
        serviceDescriptionUpdateMenu.Redirect = string.Empty;
        serviceDescriptionUpdateMenu.PermissionIdentifying = GoodServicePermissions.ServiceDescriptions.Update;
        serviceDescriptionUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderUpdateMenu);

        #endregion 修改

        #region 删除

        Menu serviceDescriptionDeleteMenu = new Menu(_guidGenerator.Create());
        serviceDescriptionDeleteMenu.TenantId = tenant?.Id;
        serviceDescriptionDeleteMenu.PId = serviceDescriptionMenu.Id;
        serviceDescriptionDeleteMenu.MenuType = MenuType.Button;
        serviceDescriptionDeleteMenu.Name = string.Empty;
        serviceDescriptionDeleteMenu.Component = string.Empty;
        serviceDescriptionDeleteMenu.ComponentAlias = string.Empty;
        serviceDescriptionDeleteMenu.IsLink = false;
        serviceDescriptionDeleteMenu.MenuSort = 4;
        serviceDescriptionDeleteMenu.Path = string.Empty;
        serviceDescriptionDeleteMenu.Redirect = string.Empty;
        serviceDescriptionDeleteMenu.PermissionIdentifying = GoodServicePermissions.ServiceDescriptions.Delete;
        serviceDescriptionDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(serviceDescriptionDeleteMenu);

        #endregion 删除

        #endregion 商城服务描述

        #region 物流公司

        Menu logisticsMenu = new Menu(_guidGenerator.Create());
        logisticsMenu.TenantId = tenant?.Id;
        logisticsMenu.PId = shopSettingMenu.Id;
        logisticsMenu.MenuType = MenuType.Menu;
        logisticsMenu.Name = "logistics";
        logisticsMenu.Component = "shopSetting/logistics/index";
        logisticsMenu.ComponentAlias = string.Empty;
        logisticsMenu.IsLink = false;
        logisticsMenu.MenuSort = 6;
        logisticsMenu.Path = "/shopSetting/logistics";
        logisticsMenu.Redirect = string.Empty;
        logisticsMenu.PermissionIdentifying = string.Empty;
        logisticsMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.logistics",
            Icon = "iconfont icon-wuliu",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(logisticsMenu);

        #region 查询

        Menu logisticsSearchMenu = new Menu(_guidGenerator.Create());
        logisticsSearchMenu.TenantId = tenant?.Id;
        logisticsSearchMenu.PId = logisticsMenu.Id;
        logisticsSearchMenu.MenuType = MenuType.Button;
        logisticsSearchMenu.Name = string.Empty;
        logisticsSearchMenu.Component = string.Empty;
        logisticsSearchMenu.ComponentAlias = string.Empty;
        logisticsSearchMenu.IsLink = false;
        logisticsSearchMenu.MenuSort = 1;
        logisticsSearchMenu.Path = string.Empty;
        logisticsSearchMenu.Redirect = string.Empty;
        logisticsSearchMenu.PermissionIdentifying = OrderServicePermissions.Logisticss.Default;
        logisticsSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(logisticsSearchMenu);

        #endregion 查询

        #region 新增

        Menu logisticsCreateMenu = new Menu(_guidGenerator.Create());
        logisticsCreateMenu.TenantId = tenant?.Id;
        logisticsCreateMenu.PId = logisticsMenu.Id;
        logisticsCreateMenu.MenuType = MenuType.Button;
        logisticsCreateMenu.Name = string.Empty;
        logisticsCreateMenu.Component = string.Empty;
        logisticsCreateMenu.ComponentAlias = string.Empty;
        logisticsCreateMenu.IsLink = false;
        logisticsCreateMenu.MenuSort = 2;
        logisticsCreateMenu.Path = string.Empty;
        logisticsCreateMenu.Redirect = string.Empty;
        logisticsCreateMenu.PermissionIdentifying = OrderServicePermissions.Logisticss.Create;
        logisticsCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(logisticsCreateMenu);

        #endregion 新增

        #region 修改

        Menu logisticsUpdateMenu = new Menu(_guidGenerator.Create());
        logisticsUpdateMenu.TenantId = tenant?.Id;
        logisticsUpdateMenu.PId = logisticsMenu.Id;
        logisticsUpdateMenu.MenuType = MenuType.Button;
        logisticsUpdateMenu.Name = string.Empty;
        logisticsUpdateMenu.Component = string.Empty;
        logisticsUpdateMenu.ComponentAlias = string.Empty;
        logisticsUpdateMenu.IsLink = false;
        logisticsUpdateMenu.MenuSort = 3;
        logisticsUpdateMenu.Path = string.Empty;
        logisticsUpdateMenu.Redirect = string.Empty;
        logisticsUpdateMenu.PermissionIdentifying = OrderServicePermissions.Logisticss.Update;
        logisticsUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderUpdateMenu);

        #endregion 修改

        #region 删除

        Menu logisticsDeleteMenu = new Menu(_guidGenerator.Create());
        logisticsDeleteMenu.TenantId = tenant?.Id;
        logisticsDeleteMenu.PId = logisticsMenu.Id;
        logisticsDeleteMenu.MenuType = MenuType.Button;
        logisticsDeleteMenu.Name = string.Empty;
        logisticsDeleteMenu.Component = string.Empty;
        logisticsDeleteMenu.ComponentAlias = string.Empty;
        logisticsDeleteMenu.IsLink = false;
        logisticsDeleteMenu.MenuSort = 4;
        logisticsDeleteMenu.Path = string.Empty;
        logisticsDeleteMenu.Redirect = string.Empty;
        logisticsDeleteMenu.PermissionIdentifying = OrderServicePermissions.Logisticss.Delete;
        logisticsDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(logisticsDeleteMenu);

        #endregion 删除

        #endregion 物流公司

        #region 配送方式

        Menu shipMenu = new Menu(_guidGenerator.Create());
        shipMenu.TenantId = tenant?.Id;
        shipMenu.PId = shopSettingMenu.Id;
        shipMenu.MenuType = MenuType.Menu;
        shipMenu.Name = "ship";
        shipMenu.Component = "shopSetting/ships/index";
        shipMenu.ComponentAlias = string.Empty;
        shipMenu.IsLink = false;
        shipMenu.MenuSort = 7;
        shipMenu.Path = "/shopSetting/ships";
        shipMenu.Redirect = string.Empty;
        shipMenu.PermissionIdentifying = string.Empty;
        shipMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.ship",
            Icon = "iconfont icon-peisongfangshi",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(shipMenu);

        #region 查询

        Menu shipSearchMenu = new Menu(_guidGenerator.Create());
        shipSearchMenu.TenantId = tenant?.Id;
        shipSearchMenu.PId = shipMenu.Id;
        shipSearchMenu.MenuType = MenuType.Button;
        shipSearchMenu.Name = string.Empty;
        shipSearchMenu.Component = string.Empty;
        shipSearchMenu.ComponentAlias = string.Empty;
        shipSearchMenu.IsLink = false;
        shipSearchMenu.MenuSort = 1;
        shipSearchMenu.Path = string.Empty;
        shipSearchMenu.Redirect = string.Empty;
        shipSearchMenu.PermissionIdentifying = OrderServicePermissions.Ships.Default;
        shipSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(shipSearchMenu);

        #endregion 查询

        #region 新增

        Menu shipCreateMenu = new Menu(_guidGenerator.Create());
        shipCreateMenu.TenantId = tenant?.Id;
        shipCreateMenu.PId = shipMenu.Id;
        shipCreateMenu.MenuType = MenuType.Button;
        shipCreateMenu.Name = string.Empty;
        shipCreateMenu.Component = string.Empty;
        shipCreateMenu.ComponentAlias = string.Empty;
        shipCreateMenu.IsLink = false;
        shipCreateMenu.MenuSort = 2;
        shipCreateMenu.Path = string.Empty;
        shipCreateMenu.Redirect = string.Empty;
        shipCreateMenu.PermissionIdentifying = OrderServicePermissions.Ships.Create;
        shipCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(shipCreateMenu);

        #endregion 新增

        #region 修改

        Menu shipUpdateMenu = new Menu(_guidGenerator.Create());
        shipUpdateMenu.TenantId = tenant?.Id;
        shipUpdateMenu.PId = shipMenu.Id;
        shipUpdateMenu.MenuType = MenuType.Button;
        shipUpdateMenu.Name = string.Empty;
        shipUpdateMenu.Component = string.Empty;
        shipUpdateMenu.ComponentAlias = string.Empty;
        shipUpdateMenu.IsLink = false;
        shipUpdateMenu.MenuSort = 3;
        shipUpdateMenu.Path = string.Empty;
        shipUpdateMenu.Redirect = string.Empty;
        shipUpdateMenu.PermissionIdentifying = OrderServicePermissions.Ships.Update;
        shipUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderUpdateMenu);

        #endregion 修改

        #region 删除

        Menu shipDeleteMenu = new Menu(_guidGenerator.Create());
        shipDeleteMenu.TenantId = tenant?.Id;
        shipDeleteMenu.PId = shipMenu.Id;
        shipDeleteMenu.MenuType = MenuType.Button;
        shipDeleteMenu.Name = string.Empty;
        shipDeleteMenu.Component = string.Empty;
        shipDeleteMenu.ComponentAlias = string.Empty;
        shipDeleteMenu.IsLink = false;
        shipDeleteMenu.MenuSort = 4;
        shipDeleteMenu.Path = string.Empty;
        shipDeleteMenu.Redirect = string.Empty;
        shipDeleteMenu.PermissionIdentifying = OrderServicePermissions.Ships.Delete;
        shipDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(shipDeleteMenu);

        #endregion 删除

        #endregion 配送方式

        #region 区域设置

        Menu areaMenu = new Menu(_guidGenerator.Create());
        areaMenu.TenantId = tenant?.Id;
        areaMenu.PId = shopSettingMenu.Id;
        areaMenu.MenuType = MenuType.Menu;
        areaMenu.Name = "area";
        areaMenu.Component = "shopSetting/area/index";
        areaMenu.ComponentAlias = string.Empty;
        areaMenu.IsLink = false;
        areaMenu.MenuSort = 8;
        areaMenu.Path = "/shopSetting/area";
        areaMenu.Redirect = string.Empty;
        areaMenu.PermissionIdentifying = string.Empty;
        areaMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.area",
            Icon = "iconfont icon-quyupeizhi",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(areaMenu);

        #region 查询

        Menu areaSearchMenu = new Menu(_guidGenerator.Create());
        areaSearchMenu.TenantId = tenant?.Id;
        areaSearchMenu.PId = areaMenu.Id;
        areaSearchMenu.MenuType = MenuType.Button;
        areaSearchMenu.Name = string.Empty;
        areaSearchMenu.Component = string.Empty;
        areaSearchMenu.ComponentAlias = string.Empty;
        areaSearchMenu.IsLink = false;
        areaSearchMenu.MenuSort = 1;
        areaSearchMenu.Path = string.Empty;
        areaSearchMenu.Redirect = string.Empty;
        areaSearchMenu.PermissionIdentifying = GoodServicePermissions.Areas.Default;
        areaSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(areaSearchMenu);

        #endregion 查询

        #region 新增

        Menu areaCreateMenu = new Menu(_guidGenerator.Create());
        areaCreateMenu.TenantId = tenant?.Id;
        areaCreateMenu.PId = areaMenu.Id;
        areaCreateMenu.MenuType = MenuType.Button;
        areaCreateMenu.Name = string.Empty;
        areaCreateMenu.Component = string.Empty;
        areaCreateMenu.ComponentAlias = string.Empty;
        areaCreateMenu.IsLink = false;
        areaCreateMenu.MenuSort = 2;
        areaCreateMenu.Path = string.Empty;
        areaCreateMenu.Redirect = string.Empty;
        areaCreateMenu.PermissionIdentifying = GoodServicePermissions.Areas.Create;
        areaCreateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemCreate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(areaCreateMenu);

        #endregion 新增

        #region 修改

        Menu areaUpdateMenu = new Menu(_guidGenerator.Create());
        areaUpdateMenu.TenantId = tenant?.Id;
        areaUpdateMenu.PId = areaMenu.Id;
        areaUpdateMenu.MenuType = MenuType.Button;
        areaUpdateMenu.Name = string.Empty;
        areaUpdateMenu.Component = string.Empty;
        areaUpdateMenu.ComponentAlias = string.Empty;
        areaUpdateMenu.IsLink = false;
        areaUpdateMenu.MenuSort = 3;
        areaUpdateMenu.Path = string.Empty;
        areaUpdateMenu.Redirect = string.Empty;
        areaUpdateMenu.PermissionIdentifying = GoodServicePermissions.Areas.Update;
        areaUpdateMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemUpdate",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(orderUpdateMenu);

        #endregion 修改

        #region 删除

        Menu areaDeleteMenu = new Menu(_guidGenerator.Create());
        areaDeleteMenu.TenantId = tenant?.Id;
        areaDeleteMenu.PId = areaMenu.Id;
        areaDeleteMenu.MenuType = MenuType.Button;
        areaDeleteMenu.Name = string.Empty;
        areaDeleteMenu.Component = string.Empty;
        areaDeleteMenu.ComponentAlias = string.Empty;
        areaDeleteMenu.IsLink = false;
        areaDeleteMenu.MenuSort = 4;
        areaDeleteMenu.Path = string.Empty;
        areaDeleteMenu.Redirect = string.Empty;
        areaDeleteMenu.PermissionIdentifying = GoodServicePermissions.Areas.Delete;
        areaDeleteMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemDelete",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(areaDeleteMenu);

        #endregion 删除

        #endregion 区域设置

        #endregion 商城设置

        #region 日志管理

        Menu logManageMenu = new Menu(_guidGenerator.Create());
        logManageMenu.TenantId = tenant?.Id;
        logManageMenu.MenuType = MenuType.Menu;
        logManageMenu.Name = "sysLogManagement";
        logManageMenu.Component = "layout/routerView/parent";
        logManageMenu.ComponentAlias = string.Empty;
        logManageMenu.IsLink = false;
        logManageMenu.MenuSort = 30;
        logManageMenu.Path = "/logManage";
        logManageMenu.Redirect = "/logManage/systemAuditLogging";
        logManageMenu.PermissionIdentifying = string.Empty;
        logManageMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.sysLogManagement",
            Icon = "iconfont icon--_xitongrizhi",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(logManageMenu);

        #region 审计日志

        Menu auditLoggingMenu = new Menu(_guidGenerator.Create());
        auditLoggingMenu.TenantId = tenant?.Id;
        auditLoggingMenu.PId = logManageMenu.Id;
        auditLoggingMenu.MenuType = MenuType.Menu;
        auditLoggingMenu.Name = "systemAuditLogging";
        auditLoggingMenu.Component = "logManage/auditLogging/index";
        auditLoggingMenu.ComponentAlias = string.Empty;
        auditLoggingMenu.IsLink = false;
        auditLoggingMenu.MenuSort = 1;
        auditLoggingMenu.Path = "/logManage/systemAuditLogging";
        auditLoggingMenu.Redirect = string.Empty;
        auditLoggingMenu.PermissionIdentifying = string.Empty;
        auditLoggingMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemAuditLogging",
            Icon = "iconfont icon-templatedefault",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(auditLoggingMenu);

        #region 查询

        Menu auditLoggingSearchMenu = new Menu(_guidGenerator.Create());
        auditLoggingSearchMenu.TenantId = tenant?.Id;
        auditLoggingSearchMenu.PId = auditLoggingMenu.Id;
        auditLoggingSearchMenu.MenuType = MenuType.Button;
        auditLoggingSearchMenu.Name = string.Empty;
        auditLoggingSearchMenu.Component = string.Empty;
        auditLoggingSearchMenu.ComponentAlias = string.Empty;
        auditLoggingSearchMenu.IsLink = false;
        auditLoggingSearchMenu.MenuSort = 1;
        auditLoggingSearchMenu.Path = string.Empty;
        auditLoggingSearchMenu.Redirect = string.Empty;
        auditLoggingSearchMenu.PermissionIdentifying = BaseServicePermissions.AuditLoggings.Default;
        auditLoggingSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(auditLoggingSearchMenu);

        #endregion 查询

        #endregion 审计日志

        #region 登录日志

        Menu securityLogMenu = new Menu(_guidGenerator.Create());
        securityLogMenu.TenantId = tenant?.Id;
        securityLogMenu.PId = logManageMenu.Id;
        securityLogMenu.MenuType = MenuType.Menu;
        securityLogMenu.Name = "systemSecurityLog";
        securityLogMenu.Component = "logManage/securityLog/index";
        securityLogMenu.ComponentAlias = string.Empty;
        securityLogMenu.IsLink = false;
        securityLogMenu.MenuSort = 2;
        securityLogMenu.Path = "/logManage/systemSecurityLog";
        securityLogMenu.Redirect = string.Empty;
        securityLogMenu.PermissionIdentifying = string.Empty;
        securityLogMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSecurityLog",
            Icon = "iconfont icon-jiansheyanshou",
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(securityLogMenu);

        #region 查询

        Menu securityLogSearchMenu = new Menu(_guidGenerator.Create());
        securityLogSearchMenu.TenantId = tenant?.Id;
        securityLogSearchMenu.PId = securityLogMenu.Id;
        securityLogSearchMenu.MenuType = MenuType.Button;
        securityLogSearchMenu.Name = string.Empty;
        securityLogSearchMenu.Component = string.Empty;
        securityLogSearchMenu.ComponentAlias = string.Empty;
        securityLogSearchMenu.IsLink = false;
        securityLogSearchMenu.MenuSort = 1;
        securityLogSearchMenu.Path = string.Empty;
        securityLogSearchMenu.Redirect = string.Empty;
        securityLogSearchMenu.PermissionIdentifying = BaseServicePermissions.SecurityLogs.Default;
        securityLogSearchMenu.MetaInfo = JsonSerializer.Serialize(new MenuMeta()
        {
            Title = "message.router.systemSelect",
            Icon = string.Empty,
            IsHide = false,
            IsKeepAlive = true,
            IsAffix = false,
            Link = string.Empty,
            IsIframe = false
        });
        menuList.Add(securityLogSearchMenu);

        #endregion 查询

        #endregion 登录日志

        #endregion 日志管理

        // 写入初始[菜单]
        await MenuRepository.InsertManyAsync(menuList);
        Logger.LogInformation($"租户【{(tenant == null ? "host" : tenant.Name)}】菜单初始化完成。");

        // 写入初始[角色]
        Logger.LogInformation($"正在给租户【{(tenant == null ? "host" : tenant.Name)}】管理员授予菜单权限...");
        List<RoleMenu> roleMenuList = new List<RoleMenu>();
        foreach (var menuItem in menuList)
        {
            RoleMenu rMenu = new RoleMenu(_guidGenerator.Create());
            rMenu.TenantId = tenant?.Id;
            rMenu.RoleId = identityRole.Id;
            rMenu.MenuId = menuItem.Id;
            roleMenuList.Add(rMenu);
        }
        await RoleMenuRepository.InsertManyAsync(roleMenuList);

        Logger.LogInformation($"租户【{(tenant == null ? "host" : tenant.Name)}】菜单权限授予完成。");
    }

    private bool AddInitialMigrationIfNotExist()
    {
        try
        {
            if (!DbMigrationsProjectExists())
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }

        try
        {
            if (!MigrationsFolderExists())
            {
                AddInitialMigration();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Logger.LogWarning("无法确定是否存在任何迁移操作 : " + e.Message);
            return false;
        }
    }

    private bool DbMigrationsProjectExists()
    {
        var dbMigrationsProjectFolder = GetEntityFrameworkCoreProjectFolderPath();

        return dbMigrationsProjectFolder != null;
    }

    private bool MigrationsFolderExists()
    {
        var dbMigrationsProjectFolder = GetEntityFrameworkCoreProjectFolderPath();
        return dbMigrationsProjectFolder != null && Directory.Exists(Path.Combine(dbMigrationsProjectFolder, "Migrations"));
    }

    private void AddInitialMigration()
    {
        Logger.LogInformation("开始进行迁移操作...");

        string argumentPrefix;
        string fileName;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            argumentPrefix = "-c";
            fileName = "/bin/bash";
        }
        else
        {
            argumentPrefix = "/C";
            fileName = "cmd.exe";
        }

        var procStartInfo = new ProcessStartInfo(fileName,
            $"{argumentPrefix} \"abp create-migration-and-run-migrator \"{GetEntityFrameworkCoreProjectFolderPath()}\"\""
        );

        try
        {
            Process.Start(procStartInfo);
        }
        catch (Exception)
        {
            throw new Exception("无法运行 ABP CLI...");
        }
    }

    private string? GetEntityFrameworkCoreProjectFolderPath()
    {
        var slnDirectoryPath = GetSolutionDirectoryPath();

        if (slnDirectoryPath == null)
        {
            throw new Exception("解决方案文件夹未找到!");
        }

        var srcDirectoryPath = Path.Combine(slnDirectoryPath, "src");

        return Directory.GetDirectories(srcDirectoryPath)
            .FirstOrDefault(d => d.EndsWith(".EntityFrameworkCore"));
    }

    private string? GetSolutionDirectoryPath()
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (currentDirectory != null && Directory.GetParent(currentDirectory.FullName) != null)
        {
            currentDirectory = Directory.GetParent(currentDirectory.FullName);

            if (currentDirectory != null && Directory.GetFiles(currentDirectory.FullName).FirstOrDefault(f => f.EndsWith(".sln")) != null)
            {
                return currentDirectory.FullName;
            }
        }

        return null;
    }
}