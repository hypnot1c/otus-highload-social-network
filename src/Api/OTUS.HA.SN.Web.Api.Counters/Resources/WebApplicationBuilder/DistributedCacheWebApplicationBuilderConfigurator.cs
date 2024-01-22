using OTUS.HS.SN.Data.DataService;

namespace OTUS.HA.SN.Web.Api.Counters.Resources;

internal class DistributedCacheWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddScoped<IDataService, OTUS.HS.SN.Data.DataService.DataService>();

    if (builder.Environment.IsStaging())
    {
      builder.Services.AddStackExchangeRedisCache(opts =>
      {
        opts.Configuration = builder.Configuration.GetConnectionString("Redis");
        opts.InstanceName = "WEB_API_COUNTERS";
      });
      return builder;
    }

    if (builder.Environment.IsDevelopment())
    {
      builder.Services.AddDistributedMemoryCache();
      return builder;
    }

    return builder;
  }
}
