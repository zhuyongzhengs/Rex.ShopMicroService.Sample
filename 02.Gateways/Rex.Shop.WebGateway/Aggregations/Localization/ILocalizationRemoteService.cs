using Rex.Shop.WebGateway.Aggregations.Base;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;

namespace Rex.Shop.WebGateway.Aggregations.Localization;

public interface ILocalizationRemoteService : IAggregateRemoteService<ApplicationLocalizationDto>;