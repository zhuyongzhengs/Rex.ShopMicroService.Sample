using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Rex.AuthService.EntityFrameworkCore;
using Rex.AuthService.Filters;
using Rex.AuthService.MultiTenancy;
using Rex.AuthService.WeChats;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using Serilog;
using SkiaSharp;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
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
using Volo.Abp.Caching;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;
using Volo.Abp.SecurityLog;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Timing;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Rex.AuthService;

[DependsOn(
    typeof(AuthServiceHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreMultiTenancyModule),
    typeof(AuthServiceApplicationModule),
    typeof(AuthServiceEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpHttpClientIdentityModelModule)
)]
public class AuthServiceHttpApiHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("AuthService");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

        PreConfigure<OpenIddictServerBuilder>(builder =>
        {
            builder.Configure(options =>
            {
                options.GrantTypes.Add(WeChatTokenExtensionGrant.ExtensionGrantName);
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        ConfigureAuthentication(context);
        ConfigureBundles();
        ConfigureUrls(configuration);
        ConfigureConventionalControllers();
        ConfigureVirtualFileSystem(context);
        ConfigureCors(context, configuration);
        ConfigureSwaggerServices(context, configuration);

        context.Services.AddHttpClient();

        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Local;
        });

        context.Services.AddMvc(setupAction =>
        {
            setupAction.Filters.Add<CaptchaAuthenticationFilter>();
        });

        // 配置自定义ITokenExtensionGrant
        Configure<AbpOpenIddictExtensionGrantsOptions>(options =>
        {
            options.Grants.Add(WeChatTokenExtensionGrant.ExtensionGrantName, new WeChatTokenExtensionGrant());
        });

        // 配置图片验证码
        ConfigureCaptcha(context, configuration);

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

        // 心跳检测
        context.Services.AddHealthChecks();
    }

    /// <summary>
    /// 分布式缓存配置
    /// </summary>
    private void ConfigureDistributedCacheOption(ServiceConfigurationContext context)
    {
        // 配置ABP缓存
        Configure<AbpDistributedCacheOptions>(options =>
        {
            // options.KeyPrefix = "Rex_Auth_Service"; // 缓存键前缀
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
            options.InstanceName = $"{typeof(AuthServiceHttpApiHostModule).Namespace}:";
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

            options.ApplicationName = "认证授权应用服务";
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
            options.ApplicationName = "认证授权应用服务";
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
            .AddOpenIddict()
            .AddServer(options =>
            {
                // 设置 AccessToken 有效期
                options.SetAccessTokenLifetime(TimeSpan.FromDays(7)); // 7天

                // 设置 RefreshToken 有效期
                options.SetRefreshTokenLifetime(TimeSpan.FromDays(8)); // 8天
            })
            .AddValidation();
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
                options.FileSets.ReplaceEmbeddedByPhysical<AuthServiceDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}05.Modules{Path.DirectorySeparatorChar}Rex.AuthService{Path.DirectorySeparatorChar}Rex.AuthService.Domain.Shared"));

                options.FileSets.ReplaceEmbeddedByPhysical<AuthServiceDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                    $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}05.Modules{Path.DirectorySeparatorChar}Rex.AuthService{Path.DirectorySeparatorChar}Rex.AuthService.Domain"));

                options.FileSets.ReplaceEmbeddedByPhysical<AuthServiceApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}05.Modules{Path.DirectorySeparatorChar}Rex.AuthService{Path.DirectorySeparatorChar}Rex.AuthService.Application.Contracts"));

                options.FileSets.ReplaceEmbeddedByPhysical<AuthServiceApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}05.Modules{Path.DirectorySeparatorChar}Rex.AuthService{Path.DirectorySeparatorChar}Rex.AuthService.Application"));
            });
        }
    }

    /// <summary>
    /// 配置常规的(自动API)控制器
    /// </summary>
    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(AuthServiceApplicationModule).Assembly, optAction =>
            {
                optAction.RootPath = "auth"; // 默认：app
                optAction.RemoteServiceName = "Auth";
            });
        });
    }

    private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"],
            new Dictionary<string, string>
            {
                    {configuration["AuthServer:DefaultScope"], "AuthService API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthService API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
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

    /// <summary>
    /// 配置图片验证码
    /// </summary>
    private void ConfigureCaptcha(ServiceConfigurationContext context, IConfiguration configuration)
    {
        // 默认使用内存存储（AddDistributedMemoryCache）
        context.Services.AddCaptcha(configuration, option =>
        {
            option.CaptchaType = CaptchaType.DEFAULT; // 验证码类型
            option.CodeLength = 4; // 验证码长度, 要放在CaptchaType设置后.  当类型为算术表达式时，长度代表操作的个数
            option.ExpirySeconds = 1 * 60; // 验证码过期时间：1分钟
            option.IgnoreCase = true; // 比较时是否忽略大小写
            option.StoreageKeyPrefix = "CAPTCHA."; // 存储键前缀

            option.ImageOption.Animation = false; // 是否启用动画
            option.ImageOption.FrameDelay = 30; // 每帧延迟,Animation=true时有效, 默认30

            option.ImageOption.Width = 145; // 验证码宽度
            option.ImageOption.Height = 40; // 验证码高度
            option.ImageOption.BackgroundColor = SKColors.White; // 验证码背景色

            option.ImageOption.BubbleCount = 2; // 气泡数量
            option.ImageOption.BubbleMinRadius = 5; // 气泡最小半径
            option.ImageOption.BubbleMaxRadius = 15; // 气泡最大半径
            option.ImageOption.BubbleThickness = 1; // 气泡边沿厚度

            option.ImageOption.InterferenceLineCount = 2; // 干扰线数量

            option.ImageOption.FontSize = 36; // 字体大小
            option.ImageOption.FontFamily = DefaultFontFamilys.Instance.Actionj; // 字体

            /*
             * 中文使用kaiti，其他字符可根据喜好设置（可能部分转字符会出现绘制不出的情况）。
             * 当验证码类型为“ARITHMETIC”时，不要使用“Ransom”字体。（运算符和等号绘制不出来）
             */
            option.ImageOption.TextBold = true;// 粗体，该配置2.0.3新增
        });

        // 使用redis分布式缓存
        //context.Services.AddStackExchangeRedisCache(options =>
        //{
        //    options.Configuration = context.Configuration.GetConnectionString("RedisCache");
        //    options.InstanceName = "captcha:";
        //});
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
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseHealthChecks("/HealthCheck");

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseRemoteErrorResponse(SystemStatusCode.Error.ToAuthServicePrefix());

        app.UseSwagger();
        app.UseAbpSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthService API");

            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            c.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            c.OAuthScopes(configuration["AuthServer:DefaultScope"]);
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}