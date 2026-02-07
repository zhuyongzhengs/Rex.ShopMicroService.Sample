using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Rex.BaseService;
using Rex.GoodService;
using Rex.OrderService;
using Rex.PaymentService;
using Rex.PromotionService;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Rex.BackendAggregationService
{
    /// <summary>
    /// 后台聚合服务模块
    /// </summary>
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(BaseServiceHttpApiClientModule),
        typeof(GoodServiceHttpApiClientModule),
        typeof(PromotionServiceHttpApiClientModule),
        typeof(OrderServiceHttpApiClientModule),
        typeof(PaymentServiceHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
     )]
    public class BackendAggregationModule : AbpModule
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
                options.Kind = DateTimeKind.Utc;
            });

            // 日期Json(数据)配置
            ConfigureDateJsonOptions(context);

            #region 添加Swagger

            context.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "商城(后台)商品聚合服务", Version = "v1", Description = "Rex商城(后台)商品聚合服务" });
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

            context.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies();

            #endregion Cors

            // 心跳检测
            context.Services.AddHealthChecks();
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
            app.UseRemoteErrorResponse(SystemStatusCode.Error.ToBackendAggregationServicePrefix());

            if (!env.IsProduction())
            {
                app.UseSwagger();
                string sysName = "商城(后台)商品聚合服务";
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