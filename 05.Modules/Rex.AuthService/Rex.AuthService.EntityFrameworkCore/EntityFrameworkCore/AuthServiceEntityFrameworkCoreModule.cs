﻿using Microsoft.Extensions.DependencyInjection;
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
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Rex.AuthService.EntityFrameworkCore;

[DependsOn(
    typeof(AuthServiceDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpMongoDbModule),
    typeof(AbpAuditLoggingMongoDbModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreMySQLModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
public class AuthServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AuthServiceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ChangeDbTablePrefix();
        context.Services.AddAbpDbContext<AuthServiceDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also AuthServiceMigrationsDbContextFactory for EF Core tooling. */
            options.UseMySQL();
        });

        // 配置有序的Guid生成
        Configure<AbpSequentialGuidGeneratorOptions>(options =>
        {
            options.DefaultSequentialGuidType = SequentialGuidType.SequentialAsString;
        });
    }

    /// <summary>
    /// 更改[数据库]表前缀
    /// </summary>
    private void ChangeDbTablePrefix()
    {
        #region TenantManagement

        AbpTenantManagementDbProperties.DbTablePrefix = AuthServiceConsts.SysDbTablePrefix;
        AbpTenantManagementDbProperties.DbSchema = AuthServiceConsts.SysDbSchema;

        #endregion TenantManagement

        #region Identity

        AbpIdentityDbProperties.DbTablePrefix = AuthServiceConsts.SysDbTablePrefix;
        AbpIdentityDbProperties.DbSchema = AuthServiceConsts.SysDbSchema;

        #endregion Identity

        #region PermissionManagement

        AbpPermissionManagementDbProperties.DbTablePrefix = AuthServiceConsts.SysDbTablePrefix;
        AbpPermissionManagementDbProperties.DbSchema = AuthServiceConsts.SysDbSchema;

        #endregion PermissionManagement

        #region SettingManagement

        AbpSettingManagementDbProperties.DbTablePrefix = AuthServiceConsts.SysDbTablePrefix;
        AbpSettingManagementDbProperties.DbSchema = AuthServiceConsts.SysDbSchema;

        #endregion SettingManagement

        #region FeatureManagement

        AbpFeatureManagementDbProperties.DbTablePrefix = AuthServiceConsts.SysDbTablePrefix;
        AbpFeatureManagementDbProperties.DbSchema = AuthServiceConsts.SysDbSchema;

        #endregion FeatureManagement

        #region AuditLogging

        AbpAuditLoggingDbProperties.DbTablePrefix = AuthServiceConsts.SysDbTablePrefix;
        AbpAuditLoggingDbProperties.DbSchema = AuthServiceConsts.SysDbSchema;

        #endregion AuditLogging
    }
}