using System.Reflection;
using Microsoft.OpenApi.Models;

namespace OTUS.HA.SN.Web.Api.Dialogs.Resources;

internal class SwaggerWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "Social network dialogs Api", Version = "v1" });

      var files = new string[]
      {
    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml",
    $"{typeof(OTUS.HA.SN.Web.Api.Dialogs.Model.Input.AssemblyMarker).Assembly.GetName().Name}.xml",
    $"{typeof(OTUS.HA.SN.Web.Api.Dialogs.Model.Output.AssemblyMarker).Assembly.GetName().Name}.xml"
      };

      foreach (var file in files)
      {
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, file));
      }

    });

    return builder;
  }
}
