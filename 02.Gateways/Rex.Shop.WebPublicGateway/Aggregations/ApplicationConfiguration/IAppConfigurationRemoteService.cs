using Rex.Shop.WebPublicGateway.Aggregations.Base;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;

namespace Rex.Shop.WebPublicGateway.Aggregations.ApplicationConfiguration;

public interface IAppConfigurationRemoteService : IAggregateRemoteService<ApplicationConfigurationDto>;