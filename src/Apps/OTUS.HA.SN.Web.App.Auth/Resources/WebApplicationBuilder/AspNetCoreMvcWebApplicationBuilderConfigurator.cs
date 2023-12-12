using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OTUS.HA.SN.Web.App.Auth.Resources;

internal class AspNetCoreMvcWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddControllers(opt =>
    {
      var cacheProfile = new CacheProfile()
      {
        NoStore = true
      };
      opt.CacheProfiles.Add("Default", cacheProfile);
      opt.Filters.Add<GlobalExceptionFilter>();
    })
    .AddJsonOptions(opts =>
    {
      opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    ;

    return builder;
  }
}
