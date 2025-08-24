using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;

namespace Rex.Shop.WebPublicGateway.Aggregations.Localization;

public interface ILocalizationAggregation
{
    string LocalizationRouteName { get; }
    string LocalizationEndpoint { get; }

    Task<ApplicationLocalizationDto> GetLocalizationAsync(LocalizationRequest input);
}