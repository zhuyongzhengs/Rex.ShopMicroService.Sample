using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Threading.Tasks;

namespace Rex.PromotionService.DbMigrator;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        Console.Title = "【Rex商城】促销服务";

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("Rex.PromotionService", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Rex.PromotionService", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.log"))
            .WriteTo.Async(c => c.Console())
            .WriteTo.Async(c => c.OpenTelemetry())
            .CreateLogger();

        try
        {
            await CreateHostBuilder(args).RunConsoleAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, ex.Message);
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .AddAppSettingsSecretsJson()
            .ConfigureLogging((context, logging) => logging.ClearProviders())
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<DbMigratorHostedService>();
            });
}