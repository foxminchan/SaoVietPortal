using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace SaoViet.Portal.Infrastructure.Cache.Redis.Internals;

public class RedisCacheService : IRedisCacheService
{
    private const string GetKeysLuaScript = "return redis.call('KEYS', @pattern)";

    private const string ClearCacheLuaScript = @"
            for _,k in ipairs(redis.call('KEYS', @pattern)) do
               redis.call('DEL', k)
            end
            ";

    private readonly RedisCache _redisCacheOption;

    private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

    private readonly SemaphoreSlim _connectionLock = new(1, 1);

    public RedisCacheService(IOptions<RedisCache> options)
    {
        _redisCacheOption = options.Value;
        _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect(options.Value.GetConnectionString()));
    }

    private ConnectionMultiplexer connectionMultiplexer => _connectionMultiplexer.Value;

    private IDatabase database
    {
        get
        {
            _connectionLock.Wait();

            try
            {
                return connectionMultiplexer.GetDatabase();
            }
            finally
            {
                _connectionLock.Release();
            }
        }
    }

    public T GetOrSet<T>(string key, Func<T> valueFactory)
    {
        return GetOrSet(key, valueFactory,
            TimeSpan.FromSeconds(_redisCacheOption.RedisDefaultSlidingExpirationInSecond));
    }

    public T GetOrSet<T>(string key, Func<T> valueFactory, TimeSpan expiration)
    {
        ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

        var cachedValue = database.StringGet(key);
        if (!string.IsNullOrEmpty(cachedValue))
            return GetByteToObject<T>(cachedValue);

        var newValue = valueFactory();
        if (newValue is not null)
            database.StringSet(key, JsonConvert.SerializeObject(newValue), expiration);

        return newValue;
    }

    public T HashGetOrSet<T>(string key, string hashKey, Func<T> valueFactory)
    {
        ArgumentException.ThrowIfNullOrEmpty(key, nameof(hashKey));
        ArgumentException.ThrowIfNullOrEmpty(hashKey, nameof(hashKey));

        var keyWithPrefix = $"{_redisCacheOption.Prefix}:{key}";
        var value = database.HashGet(keyWithPrefix, hashKey.ToLower());

        if (!string.IsNullOrEmpty(value))
            return GetByteToObject<T>(value);

        if (valueFactory() is not null)
            database.HashSet(keyWithPrefix, hashKey.ToLower(),
                JsonConvert.SerializeObject(valueFactory()));
        return valueFactory();
    }

    public IEnumerable<string> GetKeys(string pattern)
    {
        return ((RedisResult[])database.ScriptEvaluate(GetKeysLuaScript, values: new RedisValue[] { pattern })!)
            .Where(x => x.ToString()!.StartsWith(_redisCacheOption.Prefix))
            .Select(x => x.ToString())
            .ToArray()!;
    }

    public IEnumerable<T> GetValues<T>(string key)
    {
        return database.HashGetAll($"{_redisCacheOption.Prefix}:{key}").Select(x => GetByteToObject<T>(x.Value));
    }

    public bool RemoveAllKeys(string pattern = "*")
    {
        var succeed = true;

        var keys = GetKeys($"{_redisCacheOption.Prefix}:{pattern}");
        foreach (var key in keys)
            succeed = database.KeyDelete(key);

        return succeed;
    }

    public void Remove(string key)
    {
        database.KeyDelete($"{_redisCacheOption.Prefix}:{key}");
    }

    public void Reset()
    {
        database.ScriptEvaluate(
            ClearCacheLuaScript,
            values: new RedisValue[] { _redisCacheOption.Prefix + "*" },
            flags: CommandFlags.FireAndForget);
    }

    private static T GetByteToObject<T>(RedisValue value)
    {
        return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(value!)) ?? throw new NullReferenceException();
    }
}