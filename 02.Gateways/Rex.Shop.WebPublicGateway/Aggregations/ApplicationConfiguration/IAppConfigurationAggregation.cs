using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;

namespace Rex.Shop.WebPublicGateway.Aggregations.ApplicationConfiguration;

public interface IAppConfigurationAggregation
{
    string AppConfigRouteName { get; }
    string AppConfigEndpoint { get; }

    Task<ApplicationConfigurationDto> GetAppConfigurationAsync(AppConfigurationRequest input);
}