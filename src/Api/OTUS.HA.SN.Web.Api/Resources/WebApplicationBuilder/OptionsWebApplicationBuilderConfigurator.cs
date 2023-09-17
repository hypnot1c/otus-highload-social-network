namespace OTUS.HA.SN.Web.Api.Resources;

internal class OptionsWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
  {
    builder.Services.AddOptions();
    builder.Services.Configure<JwtConfigurationOptions>(builder.Configuration.GetSection("Jwt"));

    return builder;
  }
}
