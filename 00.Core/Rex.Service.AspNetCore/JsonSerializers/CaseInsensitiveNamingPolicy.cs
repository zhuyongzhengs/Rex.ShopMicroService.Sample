using System.Text.Json;

namespace Rex.Service.AspNetCore.JsonSerializers
{
    /// <summary>
    /// JSON命名策略
    /// </summary>
    public class CaseInsensitiveNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.ToLower();
        }
    }
}