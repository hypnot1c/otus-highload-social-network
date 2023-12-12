using Correlate.DependencyInjection;

namespace OTUS.HA.SN.Web.App.Auth.Resources;

internal class DistributedTracingWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddCorrelate(options =>
      options.RequestHeaders = new[]
      {
        "X-Correlation-ID"
      }
    );


    return builder;
  }
}
