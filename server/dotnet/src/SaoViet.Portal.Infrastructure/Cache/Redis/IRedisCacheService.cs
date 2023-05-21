namespace SaoViet.Portal.Infrastructure.Cache.Redis;

public interface IRedisCacheService
{
    public T GetOrSet<T>(string key, Func<T> valueFactory);

    public T GetOrSet<T>(string key, Func<T> valueFactory, TimeSpan expiration);

    public T HashGetOrSet<T>(string key, string hashKey, Func<T> valueFactory);

    public IEnumerable<string> GetKeys(string pattern);

    public IEnumerable<T> GetValues<T>(string key);

    public bool RemoveAllKeys(string pattern = "*");

    public void Remove(string key);

    public void Reset();
}