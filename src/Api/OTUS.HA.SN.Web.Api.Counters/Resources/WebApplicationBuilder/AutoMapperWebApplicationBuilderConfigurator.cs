namespace OTUS.HA.SN.Web.Api.Counters.Resources;

internal class AutoMapperWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddAutoMapper(cfg =>
    {
      cfg.AddMaps(
        typeof(Program)
        );
    });

    return builder;
  }
}
