using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Minio;
using Minio.DataModel.Args;
using Rex.BaseService.BlobStorings;
using Rex.BaseService.EntityFrameworkCore;
using Rex.BaseService.MultiTenancy;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Minio;
using Volo.Abp.Caching;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.SecurityLog;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Timing;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Rex.BaseService;

[DependsOn(
    typeof(BaseServiceHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreMultiTenancyModule),
    typeof(BaseServiceApplicationModule),
    typeof(BaseServiceEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreAuthenticationJwtBearerModule)
)]
public class BaseServiceHttpApiHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        /*
        .NET Aspire：默认将 Redis 连接放在 ConnectionStrings 节点下。例如：ConnectionStrings:Redis。
        ABP Framework：默认将 Redis 连接放在 Redis 节点下。例如：Redis:Configuration。
        */
        var configuration = context.Services.GetConfiguration();
        if (configuration["ConnectionStrings:Redis"] != null)
        {
            configuration["Redis:Configuration"] = configuration["ConnectionStrings:Redis"];
        }
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        //var hostingEnvironment = context.Services.GetHostingEnvironment();

        ConfigureAuthentication(context, configuration);
        ConfigureBundles();
        ConfigureUrls(configuration);
        ConfigureConventionalControllers();
        ConfigureVirtualFileSystem(context);
        ConfigureCors(context, configuration);
        ConfigureSwaggerServices(context, configuration);

        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });

        // 日期Json(数据)配置
        ConfigureDateJsonOptions(context);

        // 防伪令牌配置
        ConfigureAntiForgeryOptions();

        // 配置审计日志
        ConfigureAuditing();

        // 配置安全日志
        ConfigureSecurityLog();

        // 分布式缓存配置
        ConfigureDistributedCacheOption(context);

        // 配置Minio文件存储
        ConfigureMinioOptions(context, configuration);

        // 配置RabbitMQ
        ConfigureCapRabbitMqOptions(context, configuration);

        // 心跳检测
        context.Services.AddHealthChecks();
    }

    /// <summary>
    /// 配置CAP事件总线RabbitMQ
    /// </summary>
    /// <param name="context"></param>
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
                rb.Port = configuration.GetSection("CapRabbitMQ").GetValue<int>("Port", 5672);
                rb.VirtualHost = configuration.GetSection("CapRabbitMQ").GetValue<string>("VirtualHost");
                rb.ExchangeName = configuration.GetSection("CapRabbitMQ").GetValue<string>("ExchangeName");
            });
            x.DefaultGroupName = configuration.GetSection("CapRabbitMQ").GetValue<string>("GroupName");

            // 2、存储消息
            //x.UseEntityFramework<BaseServiceDbContext>();
            x.UsePostgreSql(configuration.GetConnectionString("Default"));

            // 3、消息重试
            x.FailedRetryInterval = configuration.GetSection("CapRabbitMQ").GetValue<int>("FailedRetryInterval");
            x.FailedRetryCount = configuration.GetSection("CapRabbitMQ").GetValue<int>("FailedRetryCount");

            // 4、仪表盘
            x.UseDashboard();
        });
    }

    /// <summary>
    /// 配置Minio文件存储
    /// </summary>
    /// <param name="configuration">Configuration</param>
    private void ConfigureMinioOptions(ServiceConfigurationContext context, IConfiguration configuration)
    {
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.Configure<BlobBaseServiceContainer>(container =>
            {
                var blobStoringProvider = configuration.GetSection("BlobStoringProvider");
                container.UseMinio(minio =>
                {
                    minio.BucketName = blobStoringProvider.GetValue<string>("BucketName");
                    minio.EndPoint = blobStoringProvider.GetValue<string>("Endpoint");
                    minio.AccessKey = blobStoringProvider.GetValue<string>("AccessKey");
                    minio.SecretKey = blobStoringProvider.GetValue<string>("SecretKey");
                    minio.WithSSL = blobStoringProvider.GetValue<bool>("Ssl");
                });
            });
        });
    }

    /// <summary>
    /// 分布式缓存配置
    /// </summary>
    private void ConfigureDistributedCacheOption(ServiceConfigurationContext context)
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
                // AbsoluteExpiration = DateTimeOffset.Now.AddHours(24)
            };
        });

        /* Redis缓存配置 */
        var configuration = context.Services.GetConfiguration();
        var redisSection = configuration.GetSection("Redis");

        // 1. 自定义参数配置
        string redisConfiguration = redisSection.GetValue<string>("Configuration");
        ConfigurationOptions configOptions = ConfigurationOptions.Parse(redisConfiguration);

        // 2. 创建连接复用器并注册
        var multiplexer = ConnectionMultiplexer.Connect(configOptions);
        context.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

        // 3. 配置分布式缓存
        context.Services.AddStackExchangeRedisCache(options =>
        {
            options.ConfigurationOptions = configOptions;
            options.InstanceName = $"{typeof(BaseServiceHttpApiHostModule).Namespace}:";
        });
    }

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

    /// <summary>
    /// 配置验证防伪造令牌
    /// </summary>
    private void ConfigureAntiForgeryOptions()
    {
        Configure<AbpAntiForgeryOptions>(options =>
        {
            options.AutoValidate = false; // 表示不验证防伪令牌
        });
    }

    /// <summary>
    /// 配置审计日志
    /// </summary>
    private void ConfigureAuditing()
    {
        Configure<AbpAuditingOptions>(options =>
        {
            /* 启用审计日志：默认：True */
            options.IsEnabled = true;

            options.ApplicationName = "Base应用服务";
        });
    }

    /// <summary>
    /// 配置安全日志
    /// </summary>
    private void ConfigureSecurityLog()
    {
        Configure<AbpSecurityLogOptions>(options =>
        {
            /* 启用安全日志：默认：True */
            options.IsEnabled = true;
            options.ApplicationName = "Base应用服务";
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication().AddAbpJwtBearer(options =>
        {
            options.Authority = configuration.GetSection("AuthServer").GetValue<string>("Authority");
            options.RequireHttpsMetadata = configuration.GetSection("AuthServer").GetValue<bool>("RequireHttpsMetadata");
            options.Audience = configuration.GetSection("AuthServer").GetValue<string>("Audience");
            options.TokenValidationParameters.ValidateIssuer = false;
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());

            options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
            options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
        });
    }

    private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<BaseServiceDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}05.Modules{Path.DirectorySeparatorChar}Rex.BaseService{Path.DirectorySeparatorChar}Rex.BaseService.Domain.Shared"));

                options.FileSets.ReplaceEmbeddedByPhysical<BaseServiceDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                    $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}05.Modules{Path.DirectorySeparatorChar}Rex.BaseService{Path.DirectorySeparatorChar}Rex.BaseService.Domain"));

                options.FileSets.ReplaceEmbeddedByPhysical<BaseServiceApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}05.Modules{Path.DirectorySeparatorChar}Rex.BaseService{Path.DirectorySeparatorChar}Rex.BaseService.Application.Contracts"));

                options.FileSets.ReplaceEmbeddedByPhysical<BaseServiceApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}05.Modules{Path.DirectorySeparatorChar}Rex.BaseService{Path.DirectorySeparatorChar}Rex.BaseService.Application"));
            });
        }
    }

    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(BaseServiceApplicationModule).Assembly, optAction =>
            {
                optAction.RootPath = "base"; // 默认：app
                optAction.RemoteServiceName = "Base";
            });
        });
    }

    private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"],
            new Dictionary<string, string>
            {
                {configuration["AuthServer:DefaultScope"], "BaseService API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BaseService API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);

                // 添加Swagger API文档过滤器
                //options.DocumentFilter<IgnoreSystemApiDocumentFilter>();
            });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
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
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseRemoteErrorResponse(SystemStatusCode.Error.ToBaseServicePrefix());

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseHealthChecks("/HealthCheck");

        app.UseUnitOfWork();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseAbpSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseService API");

            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            c.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            c.OAuthScopes(configuration["AuthServer:DefaultScope"]);
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }

    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var minioClient = context.ServiceProvider.GetRequiredService<IMinioClient>();
        var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
        try
        {
            string bucketName = configuration["BlobStoringProvider:BucketName"] ?? "rex.shop";
            bool exists = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!exists) await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
        }
        catch (Exception ex)
        {
            var logger = context.ServiceProvider.GetRequiredService<ILogger<BaseServiceHttpApiHostModule>>();
            logger.LogError(ex, "MinIO 自动初始化 Bucket 失败！");
        }
    }
}