using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace Aimrank.Infrastructure.Configuration.Redis
{
    public static class RedisExtensions
    {
        public static async Task<T> GetJsonAsync<T>(this IDatabase database, string key)
        {
            var value = await database.StringGetAsync(key);
            return string.IsNullOrEmpty(value) ? default : JsonSerializer.Deserialize<T>(value);
        }

        public static Task SetJsonAsync<T>(this IDatabase database, string key, T value, TimeSpan? expiry = null)
            => database.StringSetAsync(key, JsonSerializer.Serialize(value), expiry);
    }
}