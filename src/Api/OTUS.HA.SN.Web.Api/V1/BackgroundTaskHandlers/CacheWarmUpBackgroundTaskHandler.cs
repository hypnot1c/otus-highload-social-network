using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.Web.Api.V1
{
  /// <summary>
  /// 
  /// </summary>
  public class CacheWarmUpBackgroundTask : IBackgroundTask
  {
  }

  /// <summary>
  /// 
  /// </summary>
  public class CacheWarmUpBackgroundTaskHandler : IBackgroundTaskHandler<CacheWarmUpBackgroundTask>
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="slave1Context"></param>
    /// <param name="logger"></param>
    public CacheWarmUpBackgroundTaskHandler(
      IDistributedCache cache,
      Slave1Context slave1Context,
      ILogger<CacheWarmUpBackgroundTaskHandler> logger
      )
    {
      this.cache = cache;
      this.slave1Context = slave1Context;
      this.logger = logger;
    }

    private readonly IDistributedCache cache;
    private readonly Slave1Context slave1Context;
    private readonly ILogger<CacheWarmUpBackgroundTaskHandler> logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask Handle(CacheWarmUpBackgroundTask task, CancellationToken cancellationToken)
    {
      this.logger.LogInformation("Starting cache warm up...");

      var friendList = await this.slave1Context.Friends
        .Select(f => f.FriendTwo.PublicId)
        .Union(
          this.slave1Context.Friends
          .Select(f => f.FriendOne.PublicId)
          )
        .ToListAsync(cancellationToken)
        ;


      foreach (var id in friendList)
      {
        await this.ConstructFeed(id, cancellationToken);
      }

      this.logger.LogInformation("Cache warm up finished");
    }

    private async Task ConstructFeed(Guid PublicId, CancellationToken cancellationToken)
    {
      var entry = await this.slave1Context.Friends
        .AsNoTracking()

        .Where(p => p.FriendOne.PublicId == PublicId || p.FriendTwo.PublicId == PublicId)
        .SelectMany(u => u.FriendOne.Posts)
        .Union(
          this.slave1Context.Friends
            .AsNoTracking()
            .Where(p => p.FriendOne.PublicId == PublicId || p.FriendTwo.PublicId == PublicId)
            .SelectMany(u => u.FriendTwo.Posts)
            )
        .Include(p => p.Author)
        .OrderByDescending(d => d.CreatedAt)
        .Take(1000)
        .ToListAsync(cancellationToken)
        ;

      await this.cache.SetStringAsync($"feed-{PublicId}", JsonSerializer.Serialize(entry, entry.GetType(), new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }), cancellationToken);
    }
  }
}
