using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Rex.GoodService.MultiTenancy;
using Rex.GoodService.PageDesigns;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Rex.GoodService.Data;

public class GoodServiceDbMigrationService : ITransientDependency
{
    public ILogger<GoodServiceDbMigrationService> Logger { get; set; }
    public IRepository<PageDesign, Guid> PageDesignRepository { get; set; }
    public IRepository<PageDesignItem, Guid> PageDesignItemRepository { get; set; }
    private readonly IDataSeeder _dataSeeder;
    private readonly IEnumerable<IGoodServiceDbSchemaMigrator> _dbSchemaMigrators;
    private readonly ITenantRepository _tenantRepository;
    private readonly ICurrentTenant _currentTenant;
    private readonly IGuidGenerator _guidGenerator;
    private readonly AbpDataSeedOptions _options;

    public GoodServiceDbMigrationService(
        IDataSeeder dataSeeder,
        IEnumerable<IGoodServiceDbSchemaMigrator> dbSchemaMigrators,
        ITenantRepository tenantRepository,
        ICurrentTenant currentTenant,
        IGuidGenerator guidGenerator,
        IOptions<AbpDataSeedOptions> options)
    {
        _dataSeeder = dataSeeder;
        _dbSchemaMigrators = dbSchemaMigrators;
        _tenantRepository = tenantRepository;
        _currentTenant = currentTenant;
        _guidGenerator = guidGenerator;
        _options = options.Value;
        Logger = NullLogger<GoodServiceDbMigrationService>.Instance;
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
        await InitializationPageDesignAsync();

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

    /// <summary>
    /// 初始化页面布局
    /// </summary>
    /// <returns></returns>
    private async Task InitializationPageDesignAsync()
    {
        long pageDesignCount = await PageDesignRepository.GetCountAsync();
        if (pageDesignCount > 0) return; // 可根据自己的需求调整

        PageDesign pageDesign = new PageDesign(_guidGenerator.Create());
        pageDesign.Code = "mobile_home";
        pageDesign.Name = "移动端页面布局";
        pageDesign.Description = "包含【H5、小程序、Android、IOS】页面设计！";
        pageDesign.Layout = 1;
        pageDesign.Type = 1;
        await PageDesignRepository.InsertAsync(pageDesign);

        PageDesignItem pageDesignItem = new PageDesignItem(_guidGenerator.Create());
        pageDesignItem.WidgetCode = "search";
        pageDesignItem.PageCode = pageDesign.Code;
        pageDesignItem.Sort = 1;
        pageDesignItem.Parameters = "{\"keywords\":\"请输入商品信息\",\"style\":\"radius\"}";
        await PageDesignItemRepository.InsertAsync(pageDesignItem);
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
        #region 移除默认的身份数据填充

        /*
        var identityDataSeed = _options.Contributors.FirstOrDefault(x => x.Name == nameof(IdentityDataSeedContributor));
        if (identityDataSeed != null) _options.Contributors.Remove(identityDataSeed);
        var permissionDataSeed = _options.Contributors.FirstOrDefault(x => x.Name == nameof(PermissionDataSeedContributor));
        if (permissionDataSeed != null) _options.Contributors.Remove(permissionDataSeed);
        */

        #endregion 移除默认的身份数据填充

        Logger.LogInformation($"正在执行租户【{(tenant == null ? "host" : tenant.Name)}】数据填充操作...");

        await _dataSeeder.SeedAsync(new DataSeedContext(tenant?.Id)
            .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName, IdentityDataSeedContributor.AdminEmailDefaultValue)
            .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName, IdentityDataSeedContributor.AdminPasswordDefaultValue)
        );
        Logger.LogInformation($"租户【{(tenant == null ? "host" : tenant.Name)}】数据操作完成。");
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