using OTUS.HA.SN.Auth.Jwt;

namespace OTUS.HA.SN.Web.App.Auth.Resources;

internal class AuthWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

    return builder;
  }
}
