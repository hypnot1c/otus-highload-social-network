using OTUS.HA.SN.Data.Dialog.TarantoolModel;
using ProGaudi.MsgPack.Light;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;

namespace OTUS.HA.SN.Web.Api.Resources;

internal class TarantoolWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    var msgPackContext = new MsgPackContext();
    msgPackContext.GenerateAndRegisterArrayConverter<UserDialogModel>();

    var clientOptions = new ClientOptions(builder.Configuration.GetConnectionString("Tarantool"), context: msgPackContext);

    var box = new Box(clientOptions);
    box.Connect().ConfigureAwait(false).GetAwaiter().GetResult();

    builder.Services.AddSingleton(box);

    return builder;
  }
}
