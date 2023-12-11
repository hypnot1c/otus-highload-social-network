using Microsoft.EntityFrameworkCore;
using OTUS.HS.SN.Data.Auth.Context;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.Web.App.Auth.Resources;

internal class DbContextWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddDbContext<MasterContext>(options => options
      .UseNpgsql(builder.Configuration.GetConnectionString("MasterContext"))
    );
    builder.Services.AddDbContext<AuthContext>(options => options
      .UseNpgsql(builder.Configuration.GetConnectionString("AuthContext"))
    );
    builder.Services.AddDbContext<AuthSlave1Context>(options => options
      .UseNpgsql(builder.Configuration.GetConnectionString("AuthSlave1Context"))
    );

    return builder;
  }
}
