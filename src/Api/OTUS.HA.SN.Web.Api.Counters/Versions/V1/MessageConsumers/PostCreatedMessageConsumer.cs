using MassTransit;
using OTUS.HA.SN.Kafka.Message;

namespace OTUS.HA.SN.Web.Api.Counters.Versions.V1
{
  public class PostCreatedMessageConsumer : IConsumer<PostCreatedKafkaMessage>
  {

    public PostCreatedMessageConsumer(
      )
    {
    }


    public async Task Consume(ConsumeContext<PostCreatedKafkaMessage> context)
    {
    }
  }
}
