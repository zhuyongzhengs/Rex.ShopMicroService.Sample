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

namespace Rex.PromotionService.EntityFrameworkCore;

[DependsOn(
    typeof(PromotionServiceDomainModule),
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
public class PromotionServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PromotionServiceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ChangeDbTablePrefix();
        context.Services.AddAbpDbContext<PromotionServiceDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also PromotionServiceMigrationsDbContextFactory for EF Core tooling. */
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
                config.UseDbContext<PromotionServiceDbContext>();
            });
            options.Inboxes.Configure(config =>
            {
                config.UseDbContext<PromotionServiceDbContext>();
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

        AbpTenantManagementDbProperties.DbTablePrefix = PromotionServiceConsts.SysDbTablePrefix;
        AbpTenantManagementDbProperties.DbSchema = PromotionServiceConsts.SysDbSchema;

        #endregion TenantManagement

        #region Identity

        AbpIdentityDbProperties.DbTablePrefix = PromotionServiceConsts.SysDbTablePrefix;
        AbpIdentityDbProperties.DbSchema = PromotionServiceConsts.SysDbSchema;

        #endregion Identity

        #region PermissionManagement

        AbpPermissionManagementDbProperties.DbTablePrefix = PromotionServiceConsts.SysDbTablePrefix;
        AbpPermissionManagementDbProperties.DbSchema = PromotionServiceConsts.SysDbSchema;

        #endregion PermissionManagement

        #region SettingManagement

        AbpSettingManagementDbProperties.DbTablePrefix = PromotionServiceConsts.SysDbTablePrefix;
        AbpSettingManagementDbProperties.DbSchema = PromotionServiceConsts.SysDbSchema;

        #endregion SettingManagement

        #region FeatureManagement

        AbpFeatureManagementDbProperties.DbTablePrefix = PromotionServiceConsts.SysDbTablePrefix;
        AbpFeatureManagementDbProperties.DbSchema = PromotionServiceConsts.SysDbSchema;

        #endregion FeatureManagement

        #region AuditLogging

        AbpAuditLoggingDbProperties.DbTablePrefix = PromotionServiceConsts.SysDbTablePrefix;
        AbpAuditLoggingDbProperties.DbSchema = PromotionServiceConsts.SysDbSchema;

        #endregion AuditLogging
    }
}