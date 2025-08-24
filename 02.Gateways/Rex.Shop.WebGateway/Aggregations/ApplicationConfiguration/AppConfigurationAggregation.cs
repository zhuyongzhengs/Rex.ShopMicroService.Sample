using Rex.Shop.WebGateway.Aggregations.Base;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Rex.Shop.WebGateway.Aggregations.ApplicationConfiguration;

public class AppConfigurationAggregation : AggregateServiceBase<ApplicationConfigurationDto>,
    IAppConfigurationAggregation, ITransientDependency
{
    public string AppConfigRouteName => "RexShopApplicationConfiguration";
    public string AppConfigEndpoint => "api/abp/application-configuration";

    protected IAppConfigurationRemoteService AppConfigurationRemoteService { get; }

    public AppConfigurationAggregation(
        IAppConfigurationRemoteService appConfigurationRemoteService) : base(
        appConfigurationRemoteService)
    {
        AppConfigurationRemoteService = appConfigurationRemoteService;
    }

    public async Task<ApplicationConfigurationDto> GetAppConfigurationAsync(AppConfigurationRequest input)
    {
        var remoteAppConfigurationResults =
            await AppConfigurationRemoteService.GetMultipleAsync(input.Endpoints);

        // 合并所需的应用程序配置数据
        var mergedResult = MergeAppConfigurationData(remoteAppConfigurationResults);

        return mergedResult;
    }

    private static ApplicationConfigurationDto MergeAppConfigurationData(
        IDictionary<string, ApplicationConfigurationDto> appConfigurations)
    {
        var appConfigurationDto = CreateInitialAppConfigDto(appConfigurations);

        foreach (var (_, appConfig) in appConfigurations)
        {
            // 设置
            foreach (var resource in appConfig.Setting.Values)
            {
                appConfigurationDto.Setting.Values.TryAdd(resource.Key, resource.Value);
            }

            // 授权
            foreach (var resource in appConfig.Auth.GrantedPolicies)
            {
                appConfigurationDto.Auth.GrantedPolicies.TryAdd(resource.Key, resource.Value);
            }

            // 租户信息
            if (appConfig.CurrentTenant.Id.HasValue)
            {
                appConfigurationDto.CurrentTenant = appConfig.CurrentTenant;
            }

            // 用户信息
            if (appConfig.CurrentUser.Id.HasValue)
            {
                appConfigurationDto.CurrentUser = appConfig.CurrentUser;
            }

            // ...
        }

        return appConfigurationDto;
    }

    /// <summary>
    /// 检查“BaseService”clusterId以设置来自BaseService的初始数据。
    /// 否则使用第一个可用的服务作为初始应用程序配置数据
    /// </summary>
    /// <param name="appConfigurations"></param>
    /// <returns></returns>
    private static ApplicationConfigurationDto CreateInitialAppConfigDto(
        IDictionary<string, ApplicationConfigurationDto> appConfigurations
    )
    {
        if (appConfigurations.Count == 0)
        {
            return new ApplicationConfigurationDto();
        }

        if (appConfigurations.TryGetValue("BaseServiceCluster-destination1_AppConfig", out var administrationServiceData))
        {
            return MapServiceData(administrationServiceData);
        }

        return MapServiceData(appConfigurations.First().Value);
    }

    private static ApplicationConfigurationDto MapServiceData(ApplicationConfigurationDto appConfiguration)
    {
        return new ApplicationConfigurationDto
        {
            Localization = appConfiguration.Localization,
            Auth = appConfiguration.Auth,
            Clock = appConfiguration.Clock,
            Setting = appConfiguration.Setting,
            Features = appConfiguration.Features,
            Timing = appConfiguration.Timing,
            CurrentTenant = appConfiguration.CurrentTenant,
            CurrentUser = appConfiguration.CurrentUser,
            ExtraProperties = appConfiguration.ExtraProperties,
            GlobalFeatures = appConfiguration.GlobalFeatures,
            MultiTenancy = appConfiguration.MultiTenancy,
            ObjectExtensions = appConfiguration.ObjectExtensions
        };
    }
}