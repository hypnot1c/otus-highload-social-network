using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Caching.Distributed
{
  public static class DistributedCacheExtensions
  {
    public static async Task<T> GetOrCreate<T>(this IDistributedCache cache, string key, Func<DistributedCacheEntryOptions, CancellationToken, Task<T>> source, CancellationToken cancellationToken) where T : class
    {
      var entry = await cache.GetStringAsync(key, cancellationToken);

      if (!(entry is null))
      {
        return JsonSerializer.Deserialize<T>(entry);
      }

      var opts = new DistributedCacheEntryOptions();
      var data = await source(opts, cancellationToken);

      entry = JsonSerializer.Serialize(data);
      await cache.SetStringAsync(key, entry, opts, cancellationToken);

      return data;
    }
  }
}
