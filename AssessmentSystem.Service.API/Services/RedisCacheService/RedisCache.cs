using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace AssessmentSystem.Service.API.Services.RedisCacheService
{
    public class RedisCache : IRedisCache
    {
        private readonly IDistributedCache _cache;

        public RedisCache(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task Add(string key, string value)
        {
            // Setting up the cache options
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddDays(7))
                .SetSlidingExpiration(TimeSpan.FromDays(2));
            var dataToCache = Encoding.UTF8.GetBytes(value);
            await _cache.SetAsync(key, dataToCache, options);
        }

        public async Task<byte[]?> Get(string key)
        {
            return await _cache.GetAsync(key);
        }
    }
}
