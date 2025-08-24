namespace Rex.Shop.WebGateway.Extensions
{
    /// <summary>
    /// Yarp配置文件扩展
    /// </summary>
    public static class GatewayHostBuilderExtensions
    {
        public const string AppYarpJsonPath = "Config/yarp.json";

        public static IHostBuilder AddYarpJson(
            this IHostBuilder hostBuilder, bool optional = true, bool reloadOnChange = true, string path = AppYarpJsonPath)
        {
            return hostBuilder.ConfigureAppConfiguration((_, builder) =>
            {
                builder.AddJsonFile(
                        path: AppYarpJsonPath,
                        optional: optional,
                        reloadOnChange: reloadOnChange
                    )
                    .AddEnvironmentVariables();
            });
        }
    }
}