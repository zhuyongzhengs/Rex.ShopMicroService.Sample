using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.MongoDB;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Rex.GoodService.EntityFrameworkCore;

[DependsOn(
    typeof(GoodServiceDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreMySQLModule),
    typeof(AbpMongoDbModule),
    typeof(AbpAuditLoggingMongoDbModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    //typeof(AbpEventBusRabbitMqModule)
    )]
public class GoodServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        GoodServiceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ChangeDbTablePrefix();
        context.Services.AddAbpDbContext<GoodServiceDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also GoodServiceMigrationsDbContextFactory for EF Core tooling. */
            options.UseMySQL();
        });

        // 配置有序的Guid生成
        Configure<AbpSequentialGuidGeneratorOptions>(options =>
        {
            options.DefaultSequentialGuidType = SequentialGuidType.SequentialAsString;
        });

        /*
        // 配置事件收/发件箱
        Configure<AbpDistributedEventBusOptions>(options =>
        {
            options.Outboxes.Configure(config =>
            {
                config.UseDbContext<GoodServiceDbContext>();
            });
            options.Inboxes.Configure(config =>
            {
                config.UseDbContext<GoodServiceDbContext>();
            });
        });
        */
    }

    /// <summary>
    /// 更改[数据库]表前缀
    /// </summary>
    private void ChangeDbTablePrefix()
    {
        #region TenantManagement

        AbpTenantManagementDbProperties.DbTablePrefix = GoodServiceConsts.SysDbTablePrefix;
        AbpTenantManagementDbProperties.DbSchema = GoodServiceConsts.SysDbSchema;

        #endregion TenantManagement

        #region Identity

        AbpIdentityDbProperties.DbTablePrefix = GoodServiceConsts.SysDbTablePrefix;
        AbpIdentityDbProperties.DbSchema = GoodServiceConsts.SysDbSchema;

        #endregion Identity

        #region PermissionManagement

        AbpPermissionManagementDbProperties.DbTablePrefix = GoodServiceConsts.SysDbTablePrefix;
        AbpPermissionManagementDbProperties.DbSchema = GoodServiceConsts.SysDbSchema;

        #endregion PermissionManagement

        #region SettingManagement

        AbpSettingManagementDbProperties.DbTablePrefix = GoodServiceConsts.SysDbTablePrefix;
        AbpSettingManagementDbProperties.DbSchema = GoodServiceConsts.SysDbSchema;

        #endregion SettingManagement

        #region FeatureManagement

        AbpFeatureManagementDbProperties.DbTablePrefix = GoodServiceConsts.SysDbTablePrefix;
        AbpFeatureManagementDbProperties.DbSchema = GoodServiceConsts.SysDbSchema;

        #endregion FeatureManagement

        #region AuditLogging

        AbpAuditLoggingDbProperties.DbTablePrefix = GoodServiceConsts.SysDbTablePrefix;
        AbpAuditLoggingDbProperties.DbSchema = GoodServiceConsts.SysDbSchema;

        #endregion AuditLogging
    }
}