using System.Reflection;
using OTUS.HA.SN.Web.Api.Resources.HostedServices;

namespace OTUS.HA.SN.Web.Api.Resources;

internal class BackgroundWorkApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddSingleton(typeof(IBackgroundTaskQueue<>), typeof(BackgroundTaskQueue<>));

    Assembly.GetExecutingAssembly()
      .GetTypes()
      .Where(a => !a.IsAbstract && !a.IsInterface && a.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBackgroundTaskHandler<>)))
      .Select(a => new { assignedType = a, serviceTypes = a.GetInterfaces().ToList() })
      .ToList()
      .ForEach(typesToRegister =>
      {
        typesToRegister.serviceTypes.ForEach(typeToRegister => builder.Services.AddScoped(typeToRegister, typesToRegister.assignedType));
      });

    builder.Services.AddHostedService<QueuedHostedService<IBackgroundTask>>();

    return builder;
  }
}
