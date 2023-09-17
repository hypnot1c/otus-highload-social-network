namespace OTUS.HA.SN.Web.Api.Resources;

internal interface IWebApplicationBuilderConfigurator
{
  abstract static WebApplicationBuilder AddServices(WebApplicationBuilder builder);
}
