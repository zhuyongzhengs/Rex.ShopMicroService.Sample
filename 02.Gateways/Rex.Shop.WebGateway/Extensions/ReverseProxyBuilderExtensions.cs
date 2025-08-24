using Rex.Shop.WebGateway.Aggregations.ApplicationConfiguration;
using Rex.Shop.WebGateway.Aggregations.Localization;
using Yarp.ReverseProxy;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Model;

namespace Rex.Shop.WebGateway.Extensions;

public static class ReverseProxyBuilderExtensions
{
    public static ReverseProxyConventionBuilder MapReverseProxyWithLocalization(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapReverseProxy(proxyBuilder =>
        {
            proxyBuilder.Use(async (context, next) =>
            {
                var endpoint = context.GetEndpoint();

                var localizationAggregation = context.RequestServices
                    .GetRequiredService<ILocalizationAggregation>();

                var appConfigurationAggregation = context.RequestServices
                    .GetRequiredService<IAppConfigurationAggregation>();

                // The "/api/abp/application-localization" endpoint
                if (localizationAggregation.LocalizationRouteName == endpoint?.DisplayName)
                {
                    var localizationRequestInput =
                        CreateLocalizationRequestInput(context, localizationAggregation.LocalizationEndpoint);

                    var result = await localizationAggregation.GetLocalizationAsync(localizationRequestInput);
                    await context.Response.WriteAsJsonAsync(result);
                    return;
                }

                // The "/api/abp/application-configuration" endpoint
                if (appConfigurationAggregation.AppConfigRouteName == endpoint?.DisplayName)
                {
                    var appConfigurationRequestInput =
                        CreateAppConfigurationRequestInput(context, appConfigurationAggregation.AppConfigEndpoint);

                    var result =
                        await appConfigurationAggregation.GetAppConfigurationAsync(appConfigurationRequestInput);
                    await context.Response.WriteAsJsonAsync(result);
                    return;
                }

                await next();
            });

            proxyBuilder.UseLoadBalancing();
        });
    }

    private static AppConfigurationRequest CreateAppConfigurationRequestInput(HttpContext context,
        string appConfigurationPath)
    {
        //var proxyConfig = context.RequestServices.GetRequiredService<IProxyConfigProvider>();
        var proxyStateLookup = context.RequestServices.GetRequiredService<IProxyStateLookup>();

        var input = new AppConfigurationRequest();
        string path = $"{appConfigurationPath}?includeLocalizationResources=false";

        var clusterList = GetClusters(proxyStateLookup);
        foreach (var cluster in clusterList)
        {
            var hostUrl = new Uri(cluster.Value.Address) + $"{path}";
            // CacheKey/Endpoint dictionary key -> ex: ("xxx_AppConfig")
            input.Endpoints.Add($"{cluster.Key}_AppConfig", hostUrl);
        }

        return input;
    }

    private static LocalizationRequest CreateLocalizationRequestInput(HttpContext context, string localizationPath)
    {
        //var proxyConfig = context.RequestServices.GetRequiredService<IProxyConfigProvider>();
        var proxyStateLookup = context.RequestServices.GetRequiredService<IProxyStateLookup>();

        context.Request.Query.TryGetValue("CultureName", out var cultureName);

        var input = new LocalizationRequest(cultureName);
        string path = $"{localizationPath}?cultureName={cultureName}&onlyDynamics=false";

        var clusterList = GetClusters(proxyStateLookup);
        foreach (var cluster in clusterList)
        {
            var hostUrl = new Uri(cluster.Value.Address) + $"{path}";
            // Endpoint dictionary key -> ex: ("BaseService_en")
            input.Endpoints.Add($"{cluster.Key}_{cultureName}", hostUrl);
        }

        return input;
    }

    private static Dictionary<string, DestinationConfig> GetClusters(IProxyStateLookup proxyStateLookup)
    {
        var clusterList = proxyStateLookup.GetClusters();
        Dictionary<string, DestinationConfig> destinationConfigDic = new Dictionary<string, DestinationConfig>();
        foreach (var cluster in clusterList)
        {
            // 只获取[健康状态]下的集群
            if (!proxyStateLookup.TryGetCluster(cluster.ClusterId, out ClusterState? clusterState)) continue;
            for (int i = 0; i < clusterState.DestinationsState.AvailableDestinations.Count; i++)
            {
                string destinationId = clusterState.DestinationsState.AvailableDestinations[i].DestinationId;
                var availableModelConfig = clusterState.DestinationsState.AvailableDestinations[i].Model.Config;
                destinationConfigDic.Add($"{cluster.ClusterId}-{destinationId}", availableModelConfig);
            }
        }
        return destinationConfigDic;
    }
}