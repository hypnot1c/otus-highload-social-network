using AutoMapper;
using MediatR;
using OTUS.HA.SN.BusinessLogic;

namespace OTUS.HA.SN.Web.Api.V1.Mediator
{
  /// <summary>
  /// 
  /// </summary>
  public class PostCreatedNotificationHandler : INotificationHandler<PostCreatedNotification>
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="backgroundTaskQueue"></param>
    public PostCreatedNotificationHandler(
      IMapper mapper,
      IBackgroundTaskQueue<IBackgroundTask> backgroundTaskQueue
      )
    {
      this.mapper = mapper;
      this.backgroundTaskQueue = backgroundTaskQueue;
    }

    private readonly IMapper mapper;
    private readonly IBackgroundTaskQueue<IBackgroundTask> backgroundTaskQueue;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(PostCreatedNotification notification, CancellationToken cancellationToken)
    {
      var task = this.mapper.Map<PostCreatedBackgroundTask>(notification);
      await this.backgroundTaskQueue.Enqueue(task, cancellationToken);
    }
  }
}
