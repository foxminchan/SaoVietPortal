﻿using Microsoft.Extensions.Caching.Memory;

namespace Portal.Application.Cache;

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
            .SetSize(1024);
    }

    public void Remove(string key) => _cache.Remove(key);

    public T Set<T>(string key, T value) => _cache.Set(key, value, _cacheEntryOptions);

    public bool TryGetValue<T>(string key, out T value) => _cache.TryGetValue(key, out value!);
}