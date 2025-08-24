using Rex.Shop.WebPublicGateway.Aggregations.Base;
using Microsoft.Extensions.Caching.Memory;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.DependencyInjection;

namespace Rex.Shop.WebPublicGateway.Aggregations.ApplicationConfiguration;

public class AppConfigurationCachedService(IMemoryCache applicationConfigurationCache)
    : CachedServiceBase<ApplicationConfigurationDto>(applicationConfigurationCache), ISingletonDependency;