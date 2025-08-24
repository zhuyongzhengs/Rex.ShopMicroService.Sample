using Rex.Shop.WebPublicGateway.Aggregations.Base;

namespace Rex.Shop.WebPublicGateway.Aggregations.Localization;

public class LocalizationRequest : IRequestInput
{
    public Dictionary<string, string> Endpoints { get; } = new();
    public string CultureName { get; set; }

    public LocalizationRequest(string cultureName)
    {
        CultureName = cultureName;
    }
}