using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Rex.PaymentService.MultiTenancy;
using Rex.PaymentService.Payments;
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
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace Rex.PaymentService.Data;

public class PaymentServiceDbMigrationService : ITransientDependency
{
    public ILogger<PaymentServiceDbMigrationService> Logger { get; set; }
    public IRepository<Payment, Guid> PaymentRepository { get; set; }

    private readonly IDataSeeder _dataSeeder;
    private readonly IEnumerable<IPaymentServiceDbSchemaMigrator> _dbSchemaMigrators;
    private readonly ITenantRepository _tenantRepository;
    private readonly ICurrentTenant _currentTenant;

    public PaymentServiceDbMigrationService(
        IDataSeeder dataSeeder,
        IEnumerable<IPaymentServiceDbSchemaMigrator> dbSchemaMigrators,
        ITenantRepository tenantRepository,
        ICurrentTenant currentTenant)
    {
        _dataSeeder = dataSeeder;
        _dbSchemaMigrators = dbSchemaMigrators;
        _tenantRepository = tenantRepository;
        _currentTenant = currentTenant;

        Logger = NullLogger<PaymentServiceDbMigrationService>.Instance;
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
        await InitializationPaymentAsync();

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
    /// 初始化支付方式
    /// </summary>
    /// <param name="tenant"></param>
    /// <returns></returns>
    private async Task InitializationPaymentAsync(Tenant? tenant = null)
    {
        long paymentCount = await PaymentRepository.GetCountAsync();
        if (paymentCount > 0) return; // 可根据自己的需求调整

        Logger.LogInformation($"正在初始化租户【{(tenant == null ? "host" : tenant.Name)}】的支付方式...");

        List<Payment> paymentList = new List<Payment>();
        paymentList.AddRange(
            new Payment
            {
                Code = "WechatPay",
                Name = "微信支付",
                IsOnline = true,
                Sort = 1,
                Memo = "点击去微信支付",
                IsEnable = true
            },
            new Payment
            {
                Code = "AliPay",
                Name = "支付宝支付",
                IsOnline = true,
                Sort = 2,
                Memo = "点击去支付宝支付",
                IsEnable = false
            },
            new Payment
            {
                Code = "BalancePay",
                Name = "余额支付",
                IsOnline = true,
                Sort = 3,
                Memo = "账户余额支付",
                IsEnable = true
            },
            new Payment
            {
                Code = "Offline",
                Name = "线下支付",
                IsOnline = false,
                Sort = 4,
                Memo = "联系客服进行线下付款",
                IsEnable = false
            }
        );

        // 写入初始[支付方式]
        await PaymentRepository.InsertManyAsync(paymentList);
        Logger.LogInformation($"租户【{(tenant == null ? "host" : tenant.Name)}】初始化支付方式完成。");
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