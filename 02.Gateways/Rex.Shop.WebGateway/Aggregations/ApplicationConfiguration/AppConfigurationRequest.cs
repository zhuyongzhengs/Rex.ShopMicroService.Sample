using Rex.Shop.WebGateway.Aggregations.Base;

namespace Rex.Shop.WebGateway.Aggregations.ApplicationConfiguration;

public class AppConfigurationRequest : IRequestInput
{
    public Dictionary<string, string> Endpoints { get; } = new();
}