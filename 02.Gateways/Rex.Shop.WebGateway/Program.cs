using Rex.Shop.WebGateway;
using Rex.Shop.WebGateway.Extensions;
using Serilog;
using Serilog.Events;

namespace Rex.Shop.WebGateway
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.log"))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();
            try
            {
                Console.Title = "【Rex商城】(后台)网关服务";

                var builder = WebApplication.CreateBuilder(args);
                builder.Host
                    .AddAppSettingsSecretsJson()
                    // 添加Yarp的配置文件
                    .AddYarpJson()
                    .UseAutofac()
                    .UseSerilog();

                
                builder.AddServiceDefaults();
                await builder.AddApplicationAsync<WebGatewayModule>();
                var app = builder.Build();
                await app.InitializeApplicationAsync();
                await app.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                if (ex is HostAbortedException)
                {
                    throw;
                }

                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}