using System.Reflection;
using Microsoft.OpenApi.Models;

namespace OTUS.HA.SN.Web.Api.Resources;

internal class SwaggerWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "Social network Api", Version = "v1" });

      var files = new string[]
      {
    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml",
    $"{typeof(OTUS.HA.SN.Web.Api.Model.Input.AssemblyMarker).Assembly.GetName().Name}.xml",
    $"{typeof(OTUS.HA.SN.Web.Api.Model.Output.AssemblyMarker).Assembly.GetName().Name}.xml"
      };

      foreach (var file in files)
      {
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, file));
      }

    });

    return builder;
  }
}
