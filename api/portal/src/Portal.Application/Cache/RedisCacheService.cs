﻿using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace Portal.Application.Cache;

public class RedisCacheService : IRedisCacheService
{
    private const string GET_KEYS_LUA_SCRIPT = "return redis.call('KEYS', @pattern)";

    private const string CLEAR_CACHE_LUA_SCRIPT = """
        for _,k in ipairs(redis.call('KEYS', @pattern)) do
           redis.call('DEL', k)
        end
        """;

    private readonly RedisCacheOption _redisCacheOption;

    private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

    private readonly SemaphoreSlim _connectionLock = new(1, 1);

    public RedisCacheService(IOptions<RedisCacheOption> options)
    {
        _redisCacheOption = options.Value;
        _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() => 
            ConnectionMultiplexer.Connect(options.Value.GetConnectionString()));
    }

    private ConnectionMultiplexer ConnectionMultiplexer => _connectionMultiplexer.Value;

    private IDatabase Database
    {
        get
        {
            _connectionLock.Wait();

            try
            {
                return ConnectionMultiplexer.GetDatabase();
            }
            finally
            {
                _connectionLock.Release();
            }
        }
    }

    public T GetOrSet<T>(string key, Func<T> valueFactory) 
        => GetOrSet(key, valueFactory, TimeSpan.FromSeconds(_redisCacheOption.RedisDefaultSlidingExpirationInSecond));

    public T GetOrSet<T>(string key, Func<T> valueFactory, TimeSpan expiration)
    {
        if(string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key));

        if (!string.IsNullOrEmpty(Database.StringGet(key)))
            return GetByteToObject<T>(key);

        if (valueFactory() != null)
            Database.StringSet(key, JsonSerializer.SerializeToUtf8Bytes(valueFactory()), expiration);

        return valueFactory();
    }

    public T HashGetOrSet<T>(string key, string hashKey, Func<T> valueFactory)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key));

        if(string.IsNullOrEmpty(hashKey))
            throw new ArgumentNullException(nameof(hashKey));

        var keyWithPrefix = $"{_redisCacheOption.Prefix}:{key}";
        var value = Database.HashGet(keyWithPrefix, hashKey.ToLower());
        if(!string.IsNullOrEmpty(value))
            return GetByteToObject<T>(value);

        if (valueFactory() != null)
            Database.HashSet(keyWithPrefix, hashKey.ToLower(), JsonSerializer.SerializeToUtf8Bytes(valueFactory()));
        return valueFactory();
    }

    public IEnumerable<string> GetKeys(string pattern)
        => ((RedisResult[])Database.ScriptEvaluate(GET_KEYS_LUA_SCRIPT, values: new RedisValue[] { pattern })!)
            .Where(x => x.ToString()!.StartsWith(_redisCacheOption.Prefix))
            .Select(x => x.ToString())
            .ToArray()!;

    public IEnumerable<T> GetValues<T>(string key)
        => Database.HashGetAll($"{_redisCacheOption.Prefix}:{key}").Select(x => GetByteToObject<T>(x.Value));

    public bool RemoveAllKeys(string pattern = "*")
    {
        var succeed = true;

        var keys = GetKeys($"{_redisCacheOption.Prefix}:{pattern}");
        foreach (var key in keys)
            succeed = Database.KeyDelete(key);

        return succeed;
    }

    public void Remove(string key) => Database.KeyDelete($"{_redisCacheOption.Prefix}:{key}");

    public void Reset() 
        => Database.ScriptEvaluate(
            CLEAR_CACHE_LUA_SCRIPT, 
            values: new RedisValue[] { _redisCacheOption.Prefix + "*" },
            flags: CommandFlags.FireAndForget);

    private static T GetByteToObject<T>(RedisValue value)
        => JsonSerializer.Deserialize<T>(new ReadOnlySpan<byte>(value))!;
}