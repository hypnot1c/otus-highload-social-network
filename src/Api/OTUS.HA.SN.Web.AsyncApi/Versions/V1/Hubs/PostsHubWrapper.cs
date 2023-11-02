using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OTUS.HA.SN.Kafka.Message;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.Web.AsyncApi.Versions.V1
{
  public class PostsHubWrapper
  {
    public PostsHubWrapper(
        IHubContext<PostsHub, IPostsHubClient> postsHubContext,
        IMapper mapper,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<PostsHubWrapper> logger
        )
    {
      _postsHubContext = postsHubContext;
      this._mapper = mapper;
      _serviceScopeFactory = serviceScopeFactory;
      this._logger = logger;
    }

    private IServiceScopeFactory _serviceScopeFactory { get; set; }
    private readonly IHubContext<PostsHub, IPostsHubClient> _postsHubContext;
    private readonly IMapper _mapper;
    private readonly ILogger<PostsHubWrapper> _logger;

    public async Task SendPost(PostCreatedKafkaMessage post, CancellationToken cancellationToken = default)
    {
      Func<IPostsHubClient, PostMessage, CancellationToken, Task> hubCommand = async (IPostsHubClient hub, PostMessage post, CancellationToken ct) =>
      {
        await hub.PostCreated(post, ct);
      };

      await this.PostCreated(post, hubCommand, cancellationToken);
    }

    private async Task PostCreated(PostCreatedKafkaMessage post, Func<IPostsHubClient, PostMessage, CancellationToken, Task> hubCommand, CancellationToken cancellationToken)
    {
      using var scope = this._serviceScopeFactory.CreateScope();

      var serviceProvider = scope.ServiceProvider;

      var slaveContext = serviceProvider.GetRequiredService<Slave1Context>();

      var postingInfo = await slaveContext.Posts
        .Where(p => p.Id == post.PostId && p.AuthorId == post.AuthorId)
        .Select(p => new
        {
          PostPublicId = p.PublicId,
          AuthorPublicId = p.Author.PublicId
        })
        .SingleAsync(cancellationToken)
        ;

      var friendList = await slaveContext.Friends
        .Where(f => f.FriendOneId == post.AuthorId || f.FriendTwoId == post.AuthorId)
        .Select(f => f.FriendOne.PublicId)
        .Union(
          slaveContext.Friends
            .Where(f => f.FriendOneId == post.AuthorId || f.FriendTwoId == post.AuthorId)
            .Select(f => f.FriendTwo.PublicId)
        )
        .Where(p => p != postingInfo.AuthorPublicId)
        .ToListAsync(cancellationToken)
        ;


      var postMessage = this._mapper.Map<PostMessage>(post);
      postMessage.Payload.PostId = postingInfo.PostPublicId;
      postMessage.Payload.AuthorId = postingInfo.AuthorPublicId;

      foreach (var id in friendList)
      {
        this._logger.LogInformation("Sending post {postId} notification for user {userId}", post.PostId, id);
        await hubCommand(_postsHubContext.Clients.User(id.ToString()), postMessage, cancellationToken);
      }
    }
  }
}
