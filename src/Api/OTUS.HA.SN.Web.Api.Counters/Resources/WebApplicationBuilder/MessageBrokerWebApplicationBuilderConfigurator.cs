using MassTransit;
using OTUS.HA.SN.Kafka.Message;
using OTUS.HA.SN.Web.Api.Counters.Versions.V1;

namespace OTUS.HA.SN.Web.Api.Counters.Resources;

internal class MessageBrokerWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddMassTransit(x =>
    {
      x.UsingRabbitMq((context, cfg) =>
      {
        cfg.Host("rabbit");
        cfg.ConfigureEndpoints(context);
      });

      x.AddRider(rider =>
      {
        rider.AddConsumer<PostCreatedMessageConsumer>();

        rider.UsingKafka((context, k) =>
        {
          k.Host(builder.Configuration.GetConnectionString("Kafka"));

          k.TopicEndpoint<PostCreatedKafkaMessage>("posts-topic", "post-counter-consumer", e =>
          {
            e.ConfigureConsumer<PostCreatedMessageConsumer>(context);
          });
        });
      });
    });

    return builder;
  }
}
