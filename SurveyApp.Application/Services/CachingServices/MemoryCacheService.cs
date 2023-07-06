using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.CachingServices
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createItem, TimeSpan cacheDuration)
        {
            if (!_memoryCache.TryGetValue(key, out T cacheEntry))
            {
                cacheEntry = await createItem();
                var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(cacheDuration);
                _memoryCache.Set(key, cacheEntry, cacheOptions);
            }

            return cacheEntry;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
