using Imagegram.Core.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Imagegram.Infrastructure.Cache
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache memoryCache;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public virtual T Get<T>(string key)
        {
            return memoryCache.Get<T>($"{typeof(T).Name}_{key}");
        }

        public virtual void Set<T>(string key, T value, CachingOptions options = null)
        {
            memoryCache.Set<T>($"{typeof(T).Name}_{key}", value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = options.ExpirationPeriod,
                SlidingExpiration = options.InactivePeriod
            });
        }
    }
}
