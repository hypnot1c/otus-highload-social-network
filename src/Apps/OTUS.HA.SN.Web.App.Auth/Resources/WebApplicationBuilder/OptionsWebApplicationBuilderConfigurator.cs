using OTUS.HA.SN.Auth.Jwt;

namespace OTUS.HA.SN.Web.App.Auth.Resources;

internal class OptionsWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddOptions();
    builder.Services.Configure<JwtConfigurationOptions>(builder.Configuration.GetSection("Jwt"));

    return builder;
  }
}
