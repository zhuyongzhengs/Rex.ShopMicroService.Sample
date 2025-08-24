﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Rex.PaymentService.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class PaymentServiceDbContextFactory : IDesignTimeDbContextFactory<PaymentServiceDbContext>
{
    public PaymentServiceDbContext CreateDbContext(string[] args)
    {
        PaymentServiceEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<PaymentServiceDbContext>()
            .UseMySql(configuration.GetConnectionString(PaymentServiceConsts.ConnectionStringName), MySqlServerVersion.LatestSupportedServerVersion);

        return new PaymentServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Rex.PaymentService.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
