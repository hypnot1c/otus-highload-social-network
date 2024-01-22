using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using OTUS.HA.SN.Kafka.Message;

namespace OTUS.HA.SN.Web.Api.Counters.Versions.V1
{
  public class PostCreatedMessageConsumer : IConsumer<PostCreatedKafkaMessage>
  {
    public PostCreatedMessageConsumer(
      IDistributedCache distributedCache
      )
    {
      this.distributedCache = distributedCache;
    }

    private readonly IDistributedCache distributedCache;

    public async Task Consume(ConsumeContext<PostCreatedKafkaMessage> context)
    {
      var userId = context.Message.AuthorPublicId;

      var userUnreadMessagesCount = await this.distributedCache.GetOrCreate<int>(
        $"user-{userId}",
        (opts, cancellationToken) =>
        {
          return Task.FromResult(0);
        },
        context.CancellationToken
      );

      await this.distributedCache.SetStringAsync(
        $"user-{userId}",
        (userUnreadMessagesCount++).ToString(),
        context.CancellationToken
        );
    }
  }
}
