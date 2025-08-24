using Rex.Shop.WebPublicGateway.Aggregations.Base;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace Rex.Shop.WebPublicGateway.Aggregations.Localization;

public class LocalizationRemoteService : AggregateRemoteServiceBase<ApplicationLocalizationDto>,
    ILocalizationRemoteService, ITransientDependency
{
    public LocalizationRemoteService(
        IHttpContextAccessor httpContextAccessor,
        IJsonSerializer jsonSerializer,
        ILogger<AggregateRemoteServiceBase<ApplicationLocalizationDto>> logger)
        : base(httpContextAccessor, jsonSerializer, logger)
    {
    }
}