using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.App.Configuration.Authentication
{
    public class RedisCacheTicketStore : ITicketStore
    {
        private const string KeyPrefix = "AuthSessionStore-";
        private readonly IDistributedCache _cache;

        public RedisCacheTicketStore(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var key = $"{KeyPrefix}{Guid.NewGuid()}";
            await RenewAsync(key, ticket);
            return key;
        }

        public async Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            var options = new DistributedCacheEntryOptions();
            var expiresAt = ticket.Properties.ExpiresUtc;
            if (expiresAt.HasValue)
            {
                options.SetAbsoluteExpiration(expiresAt.Value);
            }

            await _cache.SetAsync(key, Serialize(ticket), options);
        }

        public async Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            var data = await _cache.GetAsync(key);
            return Deserialize(data);
        }

        public Task RemoveAsync(string key) => _cache.RemoveAsync(key);

        private static byte[] Serialize(AuthenticationTicket source)
            => TicketSerializer.Default.Serialize(source);

        private static AuthenticationTicket Deserialize(byte[] source)
            => source is null ? null : TicketSerializer.Default.Deserialize(source);
    }
}