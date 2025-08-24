using Rex.Shop.WebGateway.Aggregations.Base;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;

namespace Rex.Shop.WebGateway.Aggregations.ApplicationConfiguration;

public interface IAppConfigurationRemoteService : IAggregateRemoteService<ApplicationConfigurationDto>;