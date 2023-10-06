namespace OTUS.HA.SN.Web.Api.Resources;

internal class MediatorWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssembly(typeof(OTUS.HA.SN.BusinessLogic.Users.AssemblyMarker).Assembly);
    });

    return builder;
  }
}
