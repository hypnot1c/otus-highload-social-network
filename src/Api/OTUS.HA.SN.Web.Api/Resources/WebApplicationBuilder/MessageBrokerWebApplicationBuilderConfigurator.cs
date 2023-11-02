using MassTransit;
using OTUS.HA.SN.Kafka.Message;

namespace OTUS.HA.SN.Web.Api.Resources;

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
        rider.AddProducer<PostCreatedKafkaMessage>("posts-topic");
        rider.UsingKafka((context, k) =>
        {
          k.Host(builder.Configuration.GetConnectionString("Kafka"));
        });
      });
    });

    return builder;
  }
}
