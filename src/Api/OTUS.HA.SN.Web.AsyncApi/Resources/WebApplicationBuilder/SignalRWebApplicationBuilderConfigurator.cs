using System.Text.Json;
using System.Text.Json.Serialization;
using OTUS.HA.SN.Web.AsyncApi.Versions.V1;

namespace OTUS.HA.SN.Web.AsyncApi.Resources;

internal class SignalRWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services
    .AddSignalR()
    .AddStackExchangeRedis(builder.Configuration.GetConnectionString("Redis"), options =>
    {
      options.Configuration.ChannelPrefix = "WEB_API-SignalR";
    })
    .AddJsonProtocol(opts =>
    {
      opts.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
      opts.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    ;

    builder.Services.AddScoped<PostsHubWrapper>();

    return builder;
  }
}
