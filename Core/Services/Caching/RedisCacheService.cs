using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nakshatra.Core.Api.Model.Caching;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Nakshatra.Core.Services.Caching;

public class RedisCacheService : ICacheService
{
    private readonly ConnectionMultiplexer? _connectionMultiplexer;
    private readonly CacheConfiguration _cacheConfig;
    private DistributedCacheEntryOptions _cacheOptions;

    public RedisCacheService(IOptions<CacheConfiguration> cacheConfig, IConfiguration config)
    {
        var redisConnectionString = config["RedisConnectionString"];

        if (!string.IsNullOrEmpty(redisConnectionString))
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        }

        _cacheConfig = cacheConfig.Value;

        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheConfig.AbsoluteExpirationInHours),
            SlidingExpiration = TimeSpan.FromMinutes(_cacheConfig.SlidingExpirationInMinutes),
        };
    }
    public void Remove(string cacheKey)
    {
        var database = _connectionMultiplexer?.GetDatabase();
        database?.KeyDelete(cacheKey);
    }

    public T Set<T>(string cacheKey, T value)
    {
        var database = _connectionMultiplexer?.GetDatabase();

        var data = JsonConvert.SerializeObject(value);

        database?.StringSet(cacheKey, data ?? "", _cacheOptions.AbsoluteExpirationRelativeToNow);
        return value;
    }

    public bool TryGet<T>(string cacheKey, out T value)
    {
        var database = _connectionMultiplexer?.GetDatabase();

        var data = database?.StringGet(cacheKey);

        if (!string.IsNullOrEmpty(data))
        {
            var jsonString = database?.StringGet(cacheKey);
            value = JsonConvert.DeserializeObject<T>(jsonString!)!;
            return true;
        }
        value = default!;
        return false;
    }
}