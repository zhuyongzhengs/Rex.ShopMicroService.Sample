using Rex.Shop.WebPublicGateway.Aggregations.Base;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;

namespace Rex.Shop.WebPublicGateway.Aggregations.Localization;

public interface ILocalizationRemoteService : IAggregateRemoteService<ApplicationLocalizationDto>;