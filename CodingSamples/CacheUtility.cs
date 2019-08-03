using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace CodingSamples {
  public class CacheUtility : CacheBase {
    public static IEnumerable<string> GetCacheEntries(CacheType cacheType) {
      var cacheDictionary = GetCacheDictionary();
      var cacheItems = new List<string>();
      var filteredCacheItems = cacheDictionary.Where(x => x.Key.ToString().Contains(cacheType.ToString())).ToList();
      foreach (var item in filteredCacheItems[0].Value) {
        cacheItems.Add(item);
      }
      cacheItems.Sort();
      return cacheItems;
    }

    /// <summary>
    ///  Gets, or adds if not found, the value
    ///  using sliding expiration
    /// </summary>
    /// <typeparam name="T">Type of cached data</typeparam>
    /// <param name="key">cache key</param>
    /// <param name="value">value to be cached</param>
    /// <param name="days">number of days to be cached</param>
    /// <returns></returns>
    public static T GetOrAddAbsoluteDays<T>(string key, T value, double days) {
      var cacheItem = (T)Cache[key];
      if (cacheItem != null) { return cacheItem; }
      Cache.Set(key, value, DateTimeOffset.Now.AddDays(days));
      return value;
    }

    /// <summary>
    ///  Gets, or adds if not found, the value
    ///  using sliding expiration
    /// </summary>
    /// <typeparam name="T">Type of cached data</typeparam>
    /// <param name="key">cache key</param>
    /// <param name="value">value to be cached</param>
    /// <param name="hours">number of hours to be cached</param>
    /// <returns></returns>
    public static T GetOrAbsoluteHours<T>(string key, T value, double hours) {
      var cacheItem = (T)Cache[key];
      if (cacheItem != null) { return cacheItem; }
      Cache.Set(key, value, DateTimeOffset.Now.AddHours(hours));
      return value;
    }

    /// <summary>
    ///  Gets, or adds if not found, the value
    ///  using sliding expiration
    /// </summary>
    /// <typeparam name="T">Type of cached data</typeparam>
    /// <param name="key">cache key</param>
    /// <param name="value">value to be cached</param>
    /// <param name="minutes">number of minutes to be cached</param>
    /// <returns></returns>
    public static T GetOrAddAbsoluteMinutes<T>(string key, T value, double minutes) {
      var cacheItem = (T)Cache[key];
      if (cacheItem != null) { return cacheItem; }
      Cache.Set(key, value, DateTimeOffset.Now.AddMinutes(minutes));
      return value;
    }

    /// <summary>
    ///  Gets, or adds if not found, the value
    ///  using sliding expiration
    /// </summary>
    /// <typeparam name="T">Type of cached data</typeparam>
    /// <param name="key">cache key</param>
    /// <param name="value">value to be cached</param>
    /// <param name="days">number of days to be cached</param>
    /// <returns></returns>
    public static T GetOrAddSlidingDays<T>(string key, T value, double days) {
      var cacheItem = (T)Cache[key];
      if (cacheItem != null) { return cacheItem; }
      Cache.Set(key, value, new CacheItemPolicy { SlidingExpiration = TimeSpan.FromDays(days) });
      return value;
    }

    /// <summary>
    ///  Gets, or adds if not found, the value
    ///  using sliding expiration
    /// </summary>
    /// <typeparam name="T">Type of cached data</typeparam>
    /// <param name="key">cache key</param>
    /// <param name="value">value to be cached</param>
    /// <param name="hours">number of hours to be cached</param>
    /// <returns></returns>
    public static T GetOrAddSlidingHours<T>(string key, T value, double hours) {
      var cacheItem = (T)Cache[key];
      if (cacheItem != null) { return cacheItem; }
      Cache.Set(key, value, new CacheItemPolicy { SlidingExpiration = TimeSpan.FromHours(hours) });
      return value;
    }

    /// <summary>
    ///  Gets, or adds if not found, the value
    ///  using sliding expiration
    /// </summary>
    /// <typeparam name="T">Type of cached data</typeparam>
    /// <param name="key">cache key</param>
    /// <param name="value">value to be cached</param>
    /// <param name="minutes">number of minutes to be cached</param>
    /// <returns></returns>
    public static T GetOrAddSlidingMinutes<T>(string key, T value, double minutes) {
      var cacheItem = (T)Cache[key];
      if (cacheItem != null) { return cacheItem; }
      Cache.Set(key, value, new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(minutes) });
      return value;
    }

    /// <summary>
    /// Gets the specified cache item
    /// </summary>
    /// <param name="key">The cache key</param>
    /// <returns></returns>
    public static object GetItem(string key) => Cache.Get(key);
    /// <summary>
    /// Removes the specified item from the cache
    /// </summary>
    /// <param name="key">The cache key</param>
    public static void Remove(string key) => Cache.Remove(key);
  }
}
