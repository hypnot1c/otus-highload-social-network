namespace OTUS.HA.SN.Web.Api.Dialogs.Resources;

internal class AutoMapperWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddAutoMapper(cfg =>
    {
      cfg.AddMaps(
        typeof(Program),
        typeof(OTUS.HA.SN.BusinessLogic.Dialogs.AssemblyMarker)
        );
    });

    return builder;
  }
}
