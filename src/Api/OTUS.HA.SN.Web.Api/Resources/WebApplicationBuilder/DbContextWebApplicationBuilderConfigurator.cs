using Microsoft.EntityFrameworkCore;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.Web.Api.Resources;

internal class DbContextWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddDbContext<MasterContext>(options => options
      .UseNpgsql(builder.Configuration.GetConnectionString("MasterContext"))
    );
    builder.Services.AddDbContext<Slave1Context>(options => options
      .UseNpgsql(builder.Configuration.GetConnectionString("Slave1Context"))
    );
    //builder.Services.AddDbContext<MasterContext>(options => options.UseInMemoryDatabase("Master"));

    return builder;
  }
}
