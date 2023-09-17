namespace OTUS.HA.SN.Web.Api.Resources;

internal class MediatorWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
  {
    builder.Services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssembly(typeof(OTUS.HA.SN.BusinessLogic.Users.AssemblyMarker).Assembly);
    });

    return builder;
  }
}
