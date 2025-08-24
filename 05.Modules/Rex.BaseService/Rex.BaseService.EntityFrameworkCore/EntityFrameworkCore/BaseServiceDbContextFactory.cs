using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Rex.BaseService.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */

public class BaseServiceDbContextFactory : IDesignTimeDbContextFactory<BaseServiceDbContext>
{
    public BaseServiceDbContext CreateDbContext(string[] args)
    {
        BaseServiceEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<BaseServiceDbContext>()
            .UseMySql(configuration.GetConnectionString(BaseServiceConsts.ConnectionStringName), MySqlServerVersion.LatestSupportedServerVersion);

        return new BaseServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Rex.BaseService.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}