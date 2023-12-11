namespace OTUS.HA.SN.Web.App.Auth.Resources;

internal interface IWebApplicationBuilderConfigurator
{
  abstract WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config);
}
