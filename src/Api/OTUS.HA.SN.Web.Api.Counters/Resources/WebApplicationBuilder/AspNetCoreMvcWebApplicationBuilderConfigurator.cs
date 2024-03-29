using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OTUS.HA.SN.Web.Api.Counters.Resources;

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
      opt.Filters.Add(new AuthorizeFilter());
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
