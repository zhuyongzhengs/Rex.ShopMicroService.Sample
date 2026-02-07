using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using Rex.Shop.WebPublicGateway.Extensions;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Autofac;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace Rex.Shop.WebPublicGateway
{
    /// <summary>
    /// (外部)网关服务模块
    /// </summary>
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(AbpSwashbuckleModule)
        )]
    public class WebPublicGatewayModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddControllersWithViews();
            context.Services.AddRazorPages().AddRazorRuntimeCompilation();

            // 日期Json(数据)配置
            ConfigureDateJsonOptions(context);

            #region 反向代理

            context.Services.AddReverseProxy()
                .LoadFromConfig(configuration.GetSection("ReverseProxy"))
                .ConfigureHttpClient((context, handler) =>
                {
                    if (handler is SocketsHttpHandler socketsHandler)
                    {
                        socketsHandler.SslOptions.RemoteCertificateValidationCallback =
                            (sender, certificate, chain, sslPolicyErrors) => true;
                    }
                });

            #endregion 反向代理

            #region Swagger 配置

            ConfigureSwaggerServices(context, configuration);

            #endregion Swagger 配置

            #region Cors

            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    string corsOrigins = configuration.GetSection("App").GetSection("CorsOrigins").Value;
                    policy
                        .WithOrigins(corsOrigins.Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray() ?? Array.Empty<string>())
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            #endregion Cors
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

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAbpSwaggerGenWithOAuth(
                configuration["AuthServer:Authority"],
                new Dictionary<string, string>
                {
                {configuration["AuthServer:DefaultScope"], "WebPublicGateway API"}
                },
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebPublicGateway API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
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

            // 添加内部服务的Swagger终点
            app.UseSwaggerUiWithYarp();  //访问网关地址，自动跳转到 /swagger 的首页
            app.UseRewriter(new RewriteOptions()
                // Regex for "", "/" and "" (whitespace)
                .AddRedirect("^(|\\|\\s+)$", "/swagger"));

            app.UseCors();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAbpClaimsMap();

            app.MapWhen(
                ctx =>
                    ctx.Request.Path.ToString().StartsWith("/api/abp/") ||
                    ctx.Request.Path.ToString().StartsWith("/Abp/") ||
                    ctx.Request.Path.ToString().StartsWith("/Test/"),
                app2 =>
                {
                    app2.UseRouting();
                    app2.UseConfiguredEndpoints();
                }
            );

            app.UseEndpoints(endpoints =>
            {
                /*
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                */

                // 添加Yarp终端Endpoints
                //endpoints.MapReverseProxy();
                endpoints.MapReverseProxyWithLocalization();
            });
        }
    }
}