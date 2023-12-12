using Web.App.Auth.Client;

namespace OTUS.HA.SN.Web.Api.Resources;

internal class HttpClientsWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddWebAppAuthClient(config["Clients:WebAppAuth"]);

    return builder;
  }
}
