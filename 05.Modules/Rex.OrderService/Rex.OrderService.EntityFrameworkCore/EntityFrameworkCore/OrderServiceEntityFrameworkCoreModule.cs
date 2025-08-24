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

namespace Rex.OrderService.EntityFrameworkCore;

[DependsOn(
    typeof(OrderServiceDomainModule),
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
public class OrderServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        OrderServiceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ChangeDbTablePrefix();
        context.Services.AddAbpDbContext<OrderServiceDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also OrderServiceMigrationsDbContextFactory for EF Core tooling. */
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
                config.UseDbContext<OrderServiceDbContext>();
            });
            options.Inboxes.Configure(config =>
            {
                config.UseDbContext<OrderServiceDbContext>();
            });
        });
        */

        //Configure<AbpDistributedEntityEventOptions>(options =>
        //{
        //    options.AutoEventSelectors.Add<Order>();
        //    options.EtoMappings.Add<Order, OrderCreateEto>();
        //    options.EtoMappings.Add<OrderItem, OrderItemCreateEto>();
        //});
    }

    /// <summary>
    /// 更改[数据库]表前缀
    /// </summary>
    private void ChangeDbTablePrefix()
    {
        #region TenantManagement

        AbpTenantManagementDbProperties.DbTablePrefix = OrderServiceConsts.SysDbTablePrefix;
        AbpTenantManagementDbProperties.DbSchema = OrderServiceConsts.SysDbSchema;

        #endregion TenantManagement

        #region Identity

        AbpIdentityDbProperties.DbTablePrefix = OrderServiceConsts.SysDbTablePrefix;
        AbpIdentityDbProperties.DbSchema = OrderServiceConsts.SysDbSchema;

        #endregion Identity

        #region PermissionManagement

        AbpPermissionManagementDbProperties.DbTablePrefix = OrderServiceConsts.SysDbTablePrefix;
        AbpPermissionManagementDbProperties.DbSchema = OrderServiceConsts.SysDbSchema;

        #endregion PermissionManagement

        #region SettingManagement

        AbpSettingManagementDbProperties.DbTablePrefix = OrderServiceConsts.SysDbTablePrefix;
        AbpSettingManagementDbProperties.DbSchema = OrderServiceConsts.SysDbSchema;

        #endregion SettingManagement

        #region FeatureManagement

        AbpFeatureManagementDbProperties.DbTablePrefix = OrderServiceConsts.SysDbTablePrefix;
        AbpFeatureManagementDbProperties.DbSchema = OrderServiceConsts.SysDbSchema;

        #endregion FeatureManagement

        #region AuditLogging

        AbpAuditLoggingDbProperties.DbTablePrefix = OrderServiceConsts.SysDbTablePrefix;
        AbpAuditLoggingDbProperties.DbSchema = OrderServiceConsts.SysDbSchema;

        #endregion AuditLogging
    }
}