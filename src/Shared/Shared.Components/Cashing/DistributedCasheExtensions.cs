﻿using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed; 

namespace Shared.Components.Cashing;

public static class DistributedCacheExtensions
{
    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = null,
        WriteIndented = true,
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static Task SetAsync<T>(
        this IDistributedCache cache, 
        string key, 
        T value, 
        CancellationToken cancellationToken = default)
    {
        return SetAsync(cache, key, value, new DistributedCacheEntryOptions(), cancellationToken);
    }

    public static Task SetAsync<T>(
        this IDistributedCache cache, 
        string key, 
        T value, 
        DistributedCacheEntryOptions options, 
        CancellationToken cancellationToken)
    {
        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, _serializerOptions));
        return cache.SetAsync(key, bytes, options, cancellationToken);
    }

    public static bool TryGetValue<T>(this IDistributedCache cache, string key, out T? value)
    {
        var val = cache.Get(key);
        value = default;
        if (val == null) return false;
        value = JsonSerializer.Deserialize<T>(val, _serializerOptions);
        return true;
    }
}
