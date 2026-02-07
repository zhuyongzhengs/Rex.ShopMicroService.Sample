//using Elastic.Channels;
//using Elastic.Ingest.Elasticsearch;
//using Elastic.Ingest.Elasticsearch.DataStreams;
//using Elastic.Serilog.Sinks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Rex.BaseService;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

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
            .WriteTo.Async(c => c.OpenTelemetry())
            /*
            .WriteTo.Elasticsearch(new[] { new Uri(elasticUri) }, opts =>
            {
                opts.DataStream = new DataStreamName("logs", "rex-shop", "base");
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
            Console.Title = "【Rex商城】Base服务";
            Log.Information("Starting Rex.BaseService.HttpApi.Host.");

            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();

            builder.AddServiceDefaults();
            builder.AddRedisClient("rex-redis");
            builder.AddMinioClient("rex-minio");
            builder.AddNpgsqlDataSource("Default");
            builder.AddMongoDBClient("AbpAuditLogging");

            await builder.AddApplicationAsync<BaseServiceHttpApiHostModule>();
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