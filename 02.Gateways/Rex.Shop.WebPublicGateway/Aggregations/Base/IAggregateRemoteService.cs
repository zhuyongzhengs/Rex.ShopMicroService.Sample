namespace Rex.Shop.WebPublicGateway.Aggregations.Base;

public interface IAggregateRemoteService<TDto>
{
    Task<Dictionary<string, TDto>> GetMultipleAsync(Dictionary<string, string> serviceNameWithUrlDictionary);

    Task<T> MakeRequestAsync<T>(HttpClient httpClient, string url);

    Task<KeyValuePair<TKey, Task<TValue>>> WaitForAnyTaskAsync<TKey, TValue>(Dictionary<TKey, Task<TValue>> tasks);
}