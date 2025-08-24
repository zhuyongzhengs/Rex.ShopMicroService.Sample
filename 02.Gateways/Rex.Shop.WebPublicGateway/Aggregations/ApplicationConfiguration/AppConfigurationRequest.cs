using Rex.Shop.WebPublicGateway.Aggregations.Base;

namespace Rex.Shop.WebPublicGateway.Aggregations.ApplicationConfiguration;

public class AppConfigurationRequest : IRequestInput
{
    public Dictionary<string, string> Endpoints { get; } = new();
}