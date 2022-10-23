using System.Text.Json;

namespace FloatAway.Gateway.Services.Helpers
{
    public static class JsonSerializerHelper
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static T? DeserializeWithCamelCasing<T>(string message) =>
            System.Text.Json.JsonSerializer.Deserialize<T>(message, _options);

        public static string SerializeWithCamelCasing<T>(T obj) =>
            System.Text.Json.JsonSerializer.Serialize(obj, _options);
    }
}