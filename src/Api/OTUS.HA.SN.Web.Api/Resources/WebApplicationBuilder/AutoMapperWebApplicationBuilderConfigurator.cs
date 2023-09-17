namespace OTUS.HA.SN.Web.Api.Resources;

internal class AutoMapperWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
  {
    builder.Services.AddAutoMapper(cfg =>
    {
      cfg.AddMaps(
        typeof(Program),
        typeof(OTUS.HA.SN.BusinessLogic.Users.AssemblyMarker)
        );
    });

    return builder;
  }
}
