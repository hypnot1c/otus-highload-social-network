using AutoMapper;
using MassTransit;
using MediatR;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Kafka.Message;

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
    /// <param name="topicProducer"></param>
    /// <param name="busControl"></param>
    /// <param name="mapper"></param>
    /// <param name="backgroundTaskQueue"></param>
    public PostCreatedNotificationHandler(
      ITopicProducer<PostCreatedKafkaMessage> topicProducer,
      IBusControl busControl,
      IMapper mapper,
      IBackgroundTaskQueue<IBackgroundTask> backgroundTaskQueue
      )
    {
      this.topicProducer = topicProducer;
      this._busControl = busControl;
      this.mapper = mapper;
      this.backgroundTaskQueue = backgroundTaskQueue;
    }

    private readonly ITopicProducer<PostCreatedKafkaMessage> topicProducer;
    private readonly IBusControl _busControl;
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



      await this._busControl.StartAsync(cancellationToken);

      var message = this.mapper.Map<PostCreatedKafkaMessage>(notification);

      await this.topicProducer.Produce(message, cancellationToken);

      await this._busControl.StopAsync(cancellationToken);

    }
  }
}
