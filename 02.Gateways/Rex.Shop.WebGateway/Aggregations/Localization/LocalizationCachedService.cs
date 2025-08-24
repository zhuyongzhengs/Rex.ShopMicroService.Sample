using Rex.Shop.WebGateway.Aggregations.Base;
using Microsoft.Extensions.Caching.Memory;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.DependencyInjection;

namespace Rex.Shop.WebGateway.Aggregations.Localization;

public class LocalizationCachedService(IMemoryCache localizationCache)
    : CachedServiceBase<ApplicationLocalizationDto>(localizationCache), ISingletonDependency;