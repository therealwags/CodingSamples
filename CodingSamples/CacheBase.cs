using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Diagnostics.CodeAnalysis;

namespace CodingSamples {

  public class CacheBase {
    protected static readonly ObjectCache Cache = MemoryCache.Default;

    public enum CacheType {
      Default,
      Report,
      Catalog
    }

    [SuppressMessage("Usage", "RCS1155:Use StringComparison when comparing strings.",
      Justification = "Bug::Error displays using Contains")]
    internal static Dictionary<CacheType, List<string>> GetCacheDictionary() {
      var allCachedItems = Cache.Select(x => x.Key).ToList();
      return new Dictionary<CacheType, List<string>> {
        { CacheType.Catalog, allCachedItems.FindAll(x => x.ToLowerInvariant().Contains("catalog")) },
        { CacheType.Default, allCachedItems.FindAll(x => !x.ToLowerInvariant().Contains("catalog") && !x.ToLowerInvariant().Contains("select_")) },
        { CacheType.Report, allCachedItems.FindAll(x => x.ToLowerInvariant().Contains("select_")) }
      };
    }
  }
}