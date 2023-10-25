namespace OTUS.HA.SN.Web.Api.Resources;

internal class AutoMapperWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddAutoMapper(cfg =>
    {
      cfg.AddMaps(
        typeof(Program),
        typeof(OTUS.HA.SN.BusinessLogic.Users.AssemblyMarker),
        typeof(OTUS.HA.SN.BusinessLogic.Friends.AssemblyMarker),
        typeof(OTUS.HA.SN.BusinessLogic.Posts.AssemblyMarker),
        typeof(OTUS.HA.SN.BusinessLogic.Dialogs.AssemblyMarker)
        );
    });

    return builder;
  }
}
