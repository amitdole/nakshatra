﻿using Core.Model.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Core.Services.Caching;

public class RedisCacheService : ICacheService
{
    private readonly ConnectionMultiplexer _connectionMultiplexer;
    private readonly CacheConfiguration _cacheConfig;
    private DistributedCacheEntryOptions _cacheOptions;

    public RedisCacheService(IOptions<CacheConfiguration> cacheConfig, IConfiguration config)
    {
        _connectionMultiplexer = ConnectionMultiplexer.Connect(config["RedisConnectionString"]);
        _cacheConfig = cacheConfig.Value;

        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheConfig.AbsoluteExpirationInHours),
            SlidingExpiration = TimeSpan.FromMinutes(_cacheConfig.SlidingExpirationInMinutes),
        };
    }
    public void Remove(string cacheKey)
    {
        var database = _connectionMultiplexer.GetDatabase();
        database.KeyDelete(cacheKey);
    }

    public T Set<T>(string cacheKey, T value)
    {
        var database = _connectionMultiplexer.GetDatabase();

        var data = JsonConvert.SerializeObject(value);

        database.StringSet(cacheKey, data ?? "", _cacheOptions.AbsoluteExpirationRelativeToNow);
        return default;
    }

    public bool TryGet<T>(string cacheKey, out T value)
    {
        var database = _connectionMultiplexer.GetDatabase();

        var data = database.StringGet(cacheKey);

        if (!string.IsNullOrEmpty(data))
        {
            var jsonString = database.StringGet(cacheKey);
            value = JsonConvert.DeserializeObject<T>(jsonString);
            return true;
        }
        value = default;
        return false;
    }
}