using EasyAbp.Abp.WeChat.Pay.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Models;
using Microsoft.Extensions.DependencyInjection;
using Rex.PaymentService.EntityFrameworkCore.Events;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.MongoDB;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
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

namespace Rex.PaymentService.EntityFrameworkCore;

[DependsOn(
    typeof(PaymentServiceDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpMongoDbModule),
    typeof(AbpAuditLoggingMongoDbModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    //typeof(AbpEventBusRabbitMqModule)
    )]
public class PaymentServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PaymentServiceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ChangeDbTablePrefix();
        context.Services.AddAbpDbContext<PaymentServiceDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also PaymentServiceMigrationsDbContextFactory for EF Core tooling. */
            options.UseNpgsql();
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
                config.UseDbContext<PaymentServiceDbContext>();
            });
            options.Inboxes.Configure(config =>
            {
                config.UseDbContext<PaymentServiceDbContext>();
            });
        });
        */

        context.Services.AddTransient<IWeChatPayEventHandler<WeChatPayPaidEventModel>, PaidWeChatPayEventHandler>();
        context.Services.AddTransient<IWeChatPayEventHandler<WeChatPayRefundEventModel>, RefundWeChatPayEventHandler>();
    }

    /// <summary>
    /// 更改[数据库]表前缀
    /// </summary>
    private void ChangeDbTablePrefix()
    {
        AbpCommonDbProperties.DbTablePrefix = PaymentServiceConsts.DefaultDbTablePrefix;
        AbpCommonDbProperties.DbSchema = PaymentServiceConsts.DefaultDbSchema;

        #region TenantManagement

        AbpTenantManagementDbProperties.DbTablePrefix = PaymentServiceConsts.SysDbTablePrefix;
        AbpTenantManagementDbProperties.DbSchema = PaymentServiceConsts.SysDbSchema;

        #endregion TenantManagement

        #region Identity

        AbpIdentityDbProperties.DbTablePrefix = PaymentServiceConsts.SysDbTablePrefix;
        AbpIdentityDbProperties.DbSchema = PaymentServiceConsts.SysDbSchema;

        #endregion Identity

        #region PermissionManagement

        AbpPermissionManagementDbProperties.DbTablePrefix = PaymentServiceConsts.SysDbTablePrefix;
        AbpPermissionManagementDbProperties.DbSchema = PaymentServiceConsts.SysDbSchema;

        #endregion PermissionManagement

        #region SettingManagement

        AbpSettingManagementDbProperties.DbTablePrefix = PaymentServiceConsts.SysDbTablePrefix;
        AbpSettingManagementDbProperties.DbSchema = PaymentServiceConsts.SysDbSchema;

        #endregion SettingManagement

        #region FeatureManagement

        AbpFeatureManagementDbProperties.DbTablePrefix = PaymentServiceConsts.SysDbTablePrefix;
        AbpFeatureManagementDbProperties.DbSchema = PaymentServiceConsts.SysDbSchema;

        #endregion FeatureManagement

        #region AuditLogging

        AbpAuditLoggingDbProperties.DbTablePrefix = PaymentServiceConsts.SysDbTablePrefix;
        AbpAuditLoggingDbProperties.DbSchema = PaymentServiceConsts.SysDbSchema;

        #endregion AuditLogging
    }
}