//using Elastic.Channels;
//using Elastic.Ingest.Elasticsearch;
//using Elastic.Ingest.Elasticsearch.DataStreams;
//using Elastic.Serilog.Sinks;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Rex.OrderService;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // var elasticUri = builder.Configuration["Elasticsearch:Uri"] ?? "http://localhost:9200";

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
            /*
            .WriteTo.Elasticsearch(new[] { new Uri(elasticUri) }, opts =>
            {
                opts.DataStream = new DataStreamName("logs", "rex-shop", "order");
                opts.BootstrapMethod = BootstrapMethod.Failure;
                opts.ConfigureChannel = channelOpts =>
                {
                    channelOpts.BufferOptions = new BufferOptions
                    {
                        ExportMaxConcurrency = 10
                    };
                };
            })
            */
            .CreateLogger();

        try
        {
            Console.Title = "【Rex商城】订单服务";
            Log.Information("Starting Rex.OrderService.HttpApi.Host.");

            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<OrderServiceHttpApiHostModule>();
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