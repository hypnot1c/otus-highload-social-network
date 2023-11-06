using System.Reflection;

namespace OTUS.HA.SN.Web.Api.Resources;

internal class MediatorWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
      cfg.RegisterServicesFromAssembly(typeof(OTUS.HA.SN.BusinessLogic.Dialogs.AssemblyMarker).Assembly);
    });

    return builder;
  }
}
