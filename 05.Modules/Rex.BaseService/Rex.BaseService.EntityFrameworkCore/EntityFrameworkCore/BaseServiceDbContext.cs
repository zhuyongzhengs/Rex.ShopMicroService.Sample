using Microsoft.EntityFrameworkCore;
using Rex.BaseService.Systems.Menus;
using Rex.BaseService.Systems.RoleMenus;
using Rex.BaseService.Systems.SysUsers;
using Rex.BaseService.Systems.UserBalances;
using Rex.BaseService.Systems.UserGrades;
using Rex.BaseService.Systems.UserWeChats;
using Rex.BaseService.UserShips;
using Volo.Abp.AuditLogging;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Rex.BaseService.EntityFrameworkCore;

//[ReplaceDbContext(typeof(IIdentityDbContext))]
//[ReplaceDbContext(typeof(IPermissionManagementDbContext))]
//[ReplaceDbContext(typeof(ITenantManagementDbContext))]
//[ReplaceDbContext(typeof(ISettingManagementDbContext))]
//[ReplaceDbContext(typeof(IAuditLoggingDbContext))]
//[ReplaceDbContext(typeof(IFeatureManagementDbContext))]
[ConnectionStringName(BaseServiceConsts.ConnectionStringName)]
public class BaseServiceDbContext :
    AbpDbContext<BaseServiceDbContext>,
    IIdentityDbContext,
    IPermissionManagementDbContext,
    ITenantManagementDbContext,
    ISettingManagementDbContext,
    IFeatureManagementDbContext
//IHasEventOutbox,
//IHasEventInbox
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    /*

    #region 事件发/收件箱

    /// <summary>
    /// 事件发件箱
    /// </summary>
    public DbSet<OutgoingEventRecord> OutgoingEvents { get; set; }

    /// <summary>
    /// 事件收件箱
    /// </summary>
    public DbSet<IncomingEventRecord> IncomingEvents { get; set; }

    #endregion 事件发/收件箱

    */

    #region 租户管理

    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion 租户管理

    #region 用户身份

    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<OrganizationUnitRole> OrganizationUnitRoles { get; set; }
    public DbSet<IdentityUserOrganizationUnit> IdentityUserOrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<SysUser> SysUsers { get; set; }
    public DbSet<UserPointLog> UserPointLogs { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    #endregion 用户身份

    #region 权限授予

    public DbSet<PermissionGrant> PermissionGrants { get; set; }
    public DbSet<PermissionGroupDefinitionRecord> PermissionGroups { get; set; }
    public DbSet<PermissionDefinitionRecord> Permissions { get; set; }

    #endregion 权限授予

    #region 设置管理

    public DbSet<Setting> Settings { get; set; }

    public DbSet<SettingDefinitionRecord> SettingDefinitionRecords { get; set; }

    #endregion 设置管理

    #region 特征管理

    public DbSet<FeatureGroupDefinitionRecord> FeatureGroups { get; set; }

    public DbSet<FeatureDefinitionRecord> Features { get; set; }

    public DbSet<FeatureValue> FeatureValues { get; set; }

    #endregion 特征管理

    #region 菜单管理

    public DbSet<Menu> Menus { get; set; }
    public DbSet<RoleMenu> RoleMenus { get; set; }

    #endregion 菜单管理

    #region 微信用户

    public DbSet<UserWeChat> UserWeChats { get; set; }

    #endregion 微信用户

    #region 用户等级

    public DbSet<UserGrade> UserGrades { get; set; }

    #endregion 用户等级

    #region 用户余额

    public DbSet<UserBalance> UserBalances { get; set; }

    #endregion 用户余额

    #region 用户(收货)地址

    public DbSet<UserShip> UserShips { get; set; }

    #endregion 用户(收货)地址

    public BaseServiceDbContext(DbContextOptions<BaseServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /*

        #region 事件发/收件箱

        builder.ConfigureEventOutbox();

        builder.ConfigureEventInbox();

        #endregion 事件发/收件箱

        */

        #region 租户管理

        builder.ConfigureTenantManagement();

        #endregion 租户管理

        #region 身份(用户)

        builder.ConfigureIdentity();
        builder.ConfigureSysUserManagement();

        #endregion 身份(用户)

        #region 权限管理

        builder.ConfigurePermissionManagement();

        #endregion 权限管理

        #region 设置管理

        builder.ConfigureSettingManagement();

        #endregion 设置管理

        #region 特征管理

        builder.ConfigureFeatureManagement();

        #endregion 特征管理

        #region 菜单管理

        builder.ConfigureMenuManagement();

        #endregion 菜单管理

        #region 微信用户

        builder.ConfigureUserWeChatManagement();

        #endregion 微信用户

        #region 用户等级

        builder.ConfigureUserGradeManagement();

        #endregion 用户等级

        #region 用户余额

        builder.ConfigureUserBalanceManagement();

        #endregion 用户余额

        #region 用户(收货)地址

        builder.ConfigureUserShipManagement();

        #endregion 用户(收货)地址

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(BaseServiceConsts.DbTablePrefix + "YourEntities", BaseServiceConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}