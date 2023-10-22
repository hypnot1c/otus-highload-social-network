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
  public class PostCreatedBackgroundTask : IBackgroundTask
  {
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int AuthorId { get; set; }
  }

  /// <summary>
  /// 
  /// </summary>
  public class PostCreatedBackgroundTaskHandler : IBackgroundTaskHandler<PostCreatedBackgroundTask>
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="masterContext"></param>
    /// <param name="logger"></param>
    public PostCreatedBackgroundTaskHandler(
      IDistributedCache cache,
      MasterContext masterContext,
      ILogger<PostCreatedBackgroundTaskHandler> logger
      )
    {
      this.cache = cache;
      this.masterContext = masterContext;
      this.logger = logger;
    }

    private readonly IDistributedCache cache;
    private readonly MasterContext masterContext;
    private readonly ILogger<PostCreatedBackgroundTaskHandler> logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask Handle(PostCreatedBackgroundTask task, CancellationToken cancellationToken)
    {
      var friendList = await this.masterContext.Friends
        .Where(f => f.FriendOneId == task.AuthorId)
        .Select(f => f.FriendTwo.PublicId)
        .Union(
          this.masterContext.Friends
          .Where(f => f.FriendTwoId == task.AuthorId)
          .Select(f => f.FriendOne.PublicId)
          )
        .ToListAsync(cancellationToken)
        ;

      var tasks = friendList.Select(i => this.ConstructFeed(i, cancellationToken));

      await Task.WhenAll(tasks);
    }

    private async Task ConstructFeed(Guid PublicId, CancellationToken cancellationToken)
    {
      var entry = await this.masterContext.Friends
        .AsNoTracking()
        .Where(p => p.FriendOne.PublicId == PublicId || p.FriendTwo.PublicId == PublicId)
        .SelectMany(u => u.FriendOne.Posts)
        .Union(
          this.masterContext.Friends
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
