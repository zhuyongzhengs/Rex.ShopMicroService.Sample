using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Rex.AuthService.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */

public class AuthServiceDbContextFactory : IDesignTimeDbContextFactory<AuthServiceDbContext>
{
    public AuthServiceDbContext CreateDbContext(string[] args)
    {
        AuthServiceEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<AuthServiceDbContext>()
            .UseMySql(configuration.GetConnectionString(AuthServiceConsts.ConnectionStringName), MySqlServerVersion.LatestSupportedServerVersion);

        return new AuthServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Rex.AuthService.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}