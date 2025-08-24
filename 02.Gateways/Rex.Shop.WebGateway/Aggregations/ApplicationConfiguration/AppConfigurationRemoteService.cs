using Rex.Shop.WebGateway.Aggregations.Base;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace Rex.Shop.WebGateway.Aggregations.ApplicationConfiguration;

public class AppConfigurationRemoteService(
    IHttpContextAccessor httpContextAccessor,
    IJsonSerializer jsonSerializer,
    ILogger<AggregateRemoteServiceBase<ApplicationConfigurationDto>> logger)
    : AggregateRemoteServiceBase<ApplicationConfigurationDto>(httpContextAccessor, jsonSerializer, logger),
        IAppConfigurationRemoteService, ITransientDependency;