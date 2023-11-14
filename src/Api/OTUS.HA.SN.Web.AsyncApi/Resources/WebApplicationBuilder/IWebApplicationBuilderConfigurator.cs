namespace OTUS.HA.SN.Web.AsyncApi.Resources;

internal interface IWebApplicationBuilderConfigurator
{
  abstract WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config);
}
