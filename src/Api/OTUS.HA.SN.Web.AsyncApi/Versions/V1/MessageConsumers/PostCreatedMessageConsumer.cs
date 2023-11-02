using MassTransit;
using OTUS.HA.SN.Kafka.Message;

namespace OTUS.HA.SN.Web.AsyncApi.Versions.V1
{
  public class PostCreatedMessageConsumer : IConsumer<PostCreatedKafkaMessage>
  {

    public PostCreatedMessageConsumer(
      PostsHubWrapper postsHubWrapper
      )
    {
      this._postsHubWrapper = postsHubWrapper;
    }

    private readonly PostsHubWrapper _postsHubWrapper;

    public async Task Consume(ConsumeContext<PostCreatedKafkaMessage> context)
    {
      await this._postsHubWrapper.SendPost(context.Message, context.CancellationToken);
    }
  }
}
