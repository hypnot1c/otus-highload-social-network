using OTUS.HS.SN.Data.DataService;

namespace OTUS.HA.SN.Web.Api.Resources;

internal class DistributedCacheWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddDistributedMemoryCache();

    builder.Services.AddScoped<IDataService, OTUS.HS.SN.Data.DataService.DataService>();

    return builder;
  }
}
