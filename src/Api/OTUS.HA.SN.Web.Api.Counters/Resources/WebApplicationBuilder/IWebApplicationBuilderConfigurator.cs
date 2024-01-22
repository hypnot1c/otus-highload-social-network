namespace OTUS.HA.SN.Web.Api.Counters.Resources;

internal interface IWebApplicationBuilderConfigurator
{
  abstract WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config);
}
