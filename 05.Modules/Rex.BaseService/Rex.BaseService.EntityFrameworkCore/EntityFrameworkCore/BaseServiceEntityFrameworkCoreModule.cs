using Microsoft.EntityFrameworkCore;
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

namespace Rex.BaseService.EntityFrameworkCore;

[DependsOn(
    typeof(BaseServiceDomainModule),
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
public class BaseServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        BaseServiceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ChangeDbTablePrefix();
        context.Services.AddAbpDbContext<BaseServiceDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also BaseServiceMigrationsDbContextFactory for EF Core tooling. */
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
                config.UseDbContext<BaseServiceDbContext>();
            });
            options.Inboxes.Configure(config =>
            {
                config.UseDbContext<BaseServiceDbContext>();
            });
        });
        */
    }

    /// <summary>
    /// 更改[数据库]表前缀
    /// </summary>
    private void ChangeDbTablePrefix()
    {
        #region 租户管理

        AbpTenantManagementDbProperties.DbTablePrefix = BaseServiceConsts.DefaultDbTablePrefix;
        AbpTenantManagementDbProperties.DbSchema = BaseServiceConsts.DefaultDbSchema;

        #endregion 租户管理

        #region 身份(用户)

        AbpIdentityDbProperties.DbTablePrefix = BaseServiceConsts.DefaultDbTablePrefix;
        AbpIdentityDbProperties.DbSchema = BaseServiceConsts.DefaultDbSchema;

        #endregion 身份(用户)

        #region 权限管理

        AbpPermissionManagementDbProperties.DbTablePrefix = BaseServiceConsts.DefaultDbTablePrefix;
        AbpPermissionManagementDbProperties.DbSchema = BaseServiceConsts.DefaultDbSchema;

        #endregion 权限管理

        #region 设置管理

        AbpSettingManagementDbProperties.DbTablePrefix = BaseServiceConsts.DefaultDbTablePrefix;
        AbpSettingManagementDbProperties.DbSchema = BaseServiceConsts.DefaultDbSchema;

        #endregion 设置管理

        #region 特征管理

        AbpFeatureManagementDbProperties.DbTablePrefix = BaseServiceConsts.DefaultDbTablePrefix;
        AbpFeatureManagementDbProperties.DbSchema = BaseServiceConsts.DefaultDbSchema;

        #endregion 特征管理

        #region 审计日志

        AbpAuditLoggingDbProperties.DbTablePrefix = BaseServiceConsts.DefaultDbTablePrefix;
        AbpAuditLoggingDbProperties.DbSchema = BaseServiceConsts.DefaultDbSchema;

        #endregion 审计日志
    }
}