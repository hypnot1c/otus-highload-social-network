namespace OTUS.HA.SN.Web.Api.Dialogs.Resources;

internal interface IWebApplicationBuilderConfigurator
{
  abstract WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config);
}
