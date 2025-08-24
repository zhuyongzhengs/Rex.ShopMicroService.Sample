using Yarp.ReverseProxy.Configuration;

namespace Rex.Shop.WebPublicGateway.Extensions
{
    /// <summary>
    /// YarpSwaggerUI构建扩展
    /// </summary>
    public static class YarpSwaggerUIBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUiWithYarp(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;

            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                var proxyConfigProvider = serviceProvider.GetRequiredService<IProxyConfigProvider>();
                var yarpConfig = proxyConfigProvider.GetConfig();

                var routedClusters = yarpConfig.Clusters
                    .SelectMany(t => t.Destinations,
                        (clusterId, destination) => new { clusterId.ClusterId, destination.Value });

                var groupedClusters = routedClusters
                    .GroupBy(q => q.Value.Address)
                    .Select(t => t.First())
                    .Distinct()
                    .ToList();

                foreach (var clusterGroup in groupedClusters)
                {
                    var routeConfig = yarpConfig.Routes.FirstOrDefault(q =>
                        q.ClusterId == clusterGroup.ClusterId);
                    if (routeConfig == null)
                    {
                        logger.LogWarning($"Swagger UI: Couldn't find route configuration for {clusterGroup.ClusterId}..."); continue;
                    }

                    options.SwaggerEndpoint($"{clusterGroup.Value.Address}/swagger/v1/swagger.json", $"{routeConfig.RouteId} API");
                    options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                    options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
                }
            });
            return app;
        }
    }
}