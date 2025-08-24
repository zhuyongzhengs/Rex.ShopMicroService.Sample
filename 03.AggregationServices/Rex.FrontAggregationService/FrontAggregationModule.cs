using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.OpenApi.Models;
using Rex.BaseService;
using Rex.FrontAggregationService.EntityFrameworkCore;
using Rex.FrontAggregationService.Luas;
using Rex.GoodService;
using Rex.OrderService;
using Rex.PaymentService;
using Rex.PromotionService;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using Serilog;
using StackExchange.Redis;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Rex.FrontAggregationService
{
    /// <summary>
    /// 前台聚合服务模块
    /// </summary>
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpAutofacModule),
        typeof(BaseServiceHttpApiClientModule),
        typeof(GoodServiceHttpApiClientModule),
        typeof(PromotionServiceHttpApiClientModule),
        typeof(OrderServiceHttpApiClientModule),
        typeof(PaymentServiceHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule),
        typeof(AbpEntityFrameworkCoreMySQLModule)
     )]
    public class FrontAggregationModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            /*
            PreConfigure<AbpHttpClientBuilderOptions>(options =>
            {
                options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
                {
                    clientBuilder.AddTransientHttpErrorPolicy(policyBuilder =>
                        policyBuilder.WaitAndRetryAsync(
                            3,
                            i => TimeSpan.FromSeconds(Math.Pow(2, i))
                        )
                    );
                });
            });
            */
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddControllersWithViews();
            context.Services.AddHttpClient();

            Configure<AbpClockOptions>(options =>
            {
                options.Kind = DateTimeKind.Local;
            });

            #region 配置MySQL数据库

            context.Services.AddAbpDbContext<FrontAggregationServiceDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also BaseServiceMigrationsDbContextFactory for EF Core tooling. */
                options.UseMySQL();
            });

            /*
            // 配置事件收/发件箱
            Configure<AbpDistributedEventBusOptions>(options =>
            {
                options.Outboxes.Configure(config =>
                {
                    config.UseDbContext<FrontAggregationServiceDbContext>();
                });
                options.Inboxes.Configure(config =>
                {
                    config.UseDbContext<FrontAggregationServiceDbContext>();
                });
            });
            */

            #endregion 配置MySQL数据库

            #region 日期Json(数据)配置

            ConfigureDateJsonOptions(context);

            #endregion 日期Json(数据)配置

            #region 添加Swagger

            context.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "商城(前台)商品聚合服务", Version = "v1", Description = "Rex商城(前台)商品聚合服务" });
                // 获取xml文件名
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // 获取xml文件路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Config/" + xmlFile);
                // 添加控制器层注释，true表示显示控制器注释
                options.IncludeXmlComments(xmlPath, true);
            });

            #endregion 添加Swagger

            #region Cors

            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(configuration["App:CorsOrigins"]?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray() ?? Array.Empty<string>())
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            #endregion Cors

            #region 配置RabbitMQ

            //ConfigureRabbitMqOptions(context);
            ConfigureCapRabbitMqOptions(context, configuration);

            #endregion 配置RabbitMQ

            #region 配置Redis

            ConfigureRedisCacheOption(context);

            #endregion 配置Redis

            #region 配置下单Lua脚本

            context.Services.AddPlaceOrderLua();

            #endregion 配置下单Lua脚本

            // 心跳检测
            context.Services.AddHealthChecks();
        }

        /// <summary>
        /// 配置Redis缓存
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureRedisCacheOption(ServiceConfigurationContext context)
        {
            Configure<AbpDistributedCacheOptions>(options =>
            {
                // options.KeyPrefix = "Rex_Good_Service"; // 缓存键前缀
                options.HideErrors = true; // 启用/禁用隐藏从缓存服务器写入/读取值时的错误

                // 全局缓存配置
                options.GlobalCacheEntryOptions = new DistributedCacheEntryOptions()
                {
                    // 滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）
                    SlidingExpiration = TimeSpan.FromMinutes(20), // 默认：20分钟

                    // 绝对过期时长
                    //AbsoluteExpiration = DateTimeOffset.Now.AddHours(24)
                };
            });

            /* Redis缓存配置 */
            // 创建配置选项
            var redisSection = context.Services.GetConfiguration().GetSection("Redis");
            var configOptions = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                ConnectTimeout = redisSection.GetValue<int>("ConnectTimeout", 5000),
                SyncTimeout = redisSection.GetValue<int>("SyncTimeout", 5000),
                ConnectRetry = redisSection.GetValue<int>("ConnectRetry", 3),
                KeepAlive = redisSection.GetValue<int>("KeepAlive", 180),
                Password = redisSection["Password"],
                Ssl = redisSection.GetValue<bool>("Ssl", false),
                DefaultDatabase = redisSection.GetValue<int>("DefaultDatabase", 0)
            };

            // 添加端点
            foreach (var endpoint in redisSection.GetSection("EndPoints").GetChildren())
            {
                configOptions.EndPoints.Add(endpoint["Host"], endpoint.GetValue<int>("Port"));
            }

            // 集群特定配置
            var isCluster = redisSection.GetValue<bool>("IsCluster", false);
            if (isCluster)
            {
                configOptions.Proxy = Proxy.None;
                configOptions.DefaultVersion = new Version(7, 0);
                configOptions.CommandMap = CommandMap.Default;
            }

            // 创建连接复用器
            var multiplexer = ConnectionMultiplexer.Connect(configOptions);

            // 将 IConnectionMultiplexer 注册为单例
            context.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            // 配置分布式缓存
            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = configOptions;
                options.InstanceName = $"{typeof(FrontAggregationModule).Namespace}:";
            });

            // 集群事件处理
            if (isCluster)
            {
                multiplexer.ErrorMessage += (_, e) =>
                    Log.Error($"Redis错误: {e.Message}");

                multiplexer.ConnectionFailed += (_, e) =>
                    Log.Warning($"Redis连接失败: {e.EndPoint}, {e.FailureType}");

                multiplexer.InternalError += (_, e) =>
                    Log.Error($"Redis内部错误: {e.Exception.Message}");

                multiplexer.ConfigurationChanged += (_, e) =>
                    Log.Information($"Redis配置变更: {e.EndPoint}");

                multiplexer.HashSlotMoved += (_, e) =>
                    Log.Information($"哈希槽迁移: {e.HashSlot} 从 {e.OldEndPoint} 到 {e.NewEndPoint}");
            }
        }

        /// <summary>
        /// 配置CAP事件总线RabbitMQ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        private void ConfigureCapRabbitMqOptions(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCap(x =>
            {
                // 1、使用RabbitMQ存储事件
                x.UseRabbitMQ(rb =>
                {
                    rb.HostName = configuration.GetSection("CapRabbitMQ").GetValue<string>("HostName");
                    rb.UserName = configuration.GetSection("CapRabbitMQ").GetValue<string>("UserName");
                    rb.Password = configuration.GetSection("CapRabbitMQ").GetValue<string>("Password");
                    rb.Port = configuration.GetSection("CapRabbitMQ").GetValue<int>("Port");
                    rb.VirtualHost = configuration.GetSection("CapRabbitMQ").GetValue<string>("VirtualHost");
                    rb.ExchangeName = configuration.GetSection("CapRabbitMQ").GetValue<string>("ExchangeName");
                });
                x.DefaultGroupName = configuration.GetSection("CapRabbitMQ").GetValue<string>("GroupName");

                // 2、存储消息
                x.UseEntityFramework<FrontAggregationServiceDbContext>();
                x.UseMySql(configuration.GetConnectionString("Default"));

                // 3、消息重试
                x.FailedRetryInterval = configuration.GetSection("CapRabbitMQ").GetValue<int>("FailedRetryInterval");
                x.FailedRetryCount = configuration.GetSection("CapRabbitMQ").GetValue<int>("FailedRetryCount");

                // 4、仪表盘
                x.UseDashboard();
            });
        }

        /*
        /// <summary>
        /// 配置事件总线RabbitMQ
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureRabbitMqOptions(ServiceConfigurationContext context)
        {
            var connections = context.Services.GetConfiguration().GetSection("RabbitMQ").GetSection("Connections").GetSection("Default");
            Configure<AbpRabbitMqOptions>(options =>
            {
                options.Connections.Default.HostName = connections.GetValue<string>("HostName");
                options.Connections.Default.Port = connections.GetValue<int>("Port");
                options.Connections.Default.UserName = connections.GetValue<string>("UserName");
                options.Connections.Default.Password = connections.GetValue<string>("Password");
                options.Connections.Default.VirtualHost = connections.GetValue<string>("VirtualHost");
            });

            var eventBus = context.Services.GetConfiguration().GetSection("RabbitMQ").GetSection("EventBus");
            Configure<AbpRabbitMqEventBusOptions>(options =>
            {
                options.ClientName = eventBus.GetValue<string>("ClientName");
                options.ExchangeName = eventBus.GetValue<string>("ExchangeName");
            });
        }
        */

        /// <summary>
        /// 配置日期Json策略
        /// </summary>
        private void ConfigureDateJsonOptions(ServiceConfigurationContext context)
        {
            Configure<JsonOptions>(options =>
            {
                /* 采用驼峰命名法 */
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                /* Json序列化字符编码 */
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

                /* 在序列化和反序列化时保留对象引用，从而避免循环引用的问题 */
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            Configure<AbpJsonOptions>(options =>
            {
                // 默认日期格式化
                options.OutputDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                options.InputDateTimeFormats = new List<string>() { "yyyy-MM-dd", "yyyy-MM-dd HH:mm", "yyyy-MM-dd HH:mm:ss" };
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            var configuration = context.GetConfiguration();

            // Configure the HTTP request pipeline.
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseHealthChecks("/HealthCheck");

            app.UseAuthorization();
            app.UseRemoteErrorResponse(SystemStatusCode.Error.ToFrontAggregationServicePrefix());

            if (!env.IsProduction())
            {
                app.UseSwagger();
                string sysName = "商城(前台)商品聚合服务";
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"API {sysName} v1");
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}