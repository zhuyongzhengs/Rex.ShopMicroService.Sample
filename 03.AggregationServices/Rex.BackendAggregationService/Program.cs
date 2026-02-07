//using Elastic.Channels;
//using Elastic.Ingest.Elasticsearch;
//using Elastic.Ingest.Elasticsearch.DataStreams;
//using Elastic.Serilog.Sinks;
using Serilog.Events;
using Serilog;

namespace Rex.BackendAggregationService
{
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
                .WriteTo.Async(c => c.OpenTelemetry())
                /*
                .WriteTo.Elasticsearch(new[] { new Uri(elasticUri) }, opts =>
                {
                    opts.DataStream = new DataStreamName("logs", "rex-shop", "backend-aggregation");
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
                Console.Title = "【Rex商城】(后台)商品聚合服务";

                builder.Host
                    .AddAppSettingsSecretsJson()
                    .UseAutofac()
                    .UseSerilog();

                
                builder.AddServiceDefaults();
                await builder.AddApplicationAsync<BackendAggregationModule>();
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