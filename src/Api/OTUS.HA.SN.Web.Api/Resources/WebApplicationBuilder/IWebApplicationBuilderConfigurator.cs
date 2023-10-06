namespace OTUS.HA.SN.Web.Api.Resources;

internal interface IWebApplicationBuilderConfigurator
{
  abstract WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config);
}
