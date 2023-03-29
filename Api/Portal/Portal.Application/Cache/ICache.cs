namespace Portal.Application.Cache;

public interface ICache
{
    void Remove(string key);
    T Set<T>(string key, T value);
    bool TryGetValue<T>(string key, out T value);
}