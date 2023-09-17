using Microsoft.EntityFrameworkCore;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.Web.Api.Resources;

internal class DbContextWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
  {
    builder.Services.AddDbContext<MasterContext>(options => options
      .UseNpgsql(builder.Configuration.GetConnectionString("MasterContext"))
    );
    //builder.Services.AddDbContext<MasterContext>(options => options.UseInMemoryDatabase("Master"));

    return builder;
  }
}
