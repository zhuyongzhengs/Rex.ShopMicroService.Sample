using Microsoft.Extensions.Hosting;

namespace Rex.ShopService.AppHost.Configurations
{
    /// <summary>
    /// AppHost服务配置
    /// </summary>
    public static partial class ServiceConfigure
    {
        /// <summary>
        /// 服务编排
        /// </summary>
        /// <param name="builder"></param>
        public static void AddOrchestrationService(this IDistributedApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                var parameters = AppParameters.Create(builder); // 应用参数管理
                ConfigureDevEnvironment(builder, parameters);
            }
            else
            {
                ConfigureProdEnvironment(builder);
            }
        }
    }
}