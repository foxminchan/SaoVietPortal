using Microsoft.Extensions.Caching.Memory;
using Portal.Domain.Interfaces.Common;

namespace Portal.Application.Cache
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class CacheService : ICache
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
            _cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                .SetPriority(CacheItemPriority.High)
                .SetSize(1024); ;
        }

        public void Remove(string key) => _cache.Remove(key);

        public T Set<T>(string key, T value) => _cache.Set(key, value, _cacheEntryOptions);

        public bool TryGetValue<T>(string key, out T value) => _cache.TryGetValue(key, out value!);
    }
}
