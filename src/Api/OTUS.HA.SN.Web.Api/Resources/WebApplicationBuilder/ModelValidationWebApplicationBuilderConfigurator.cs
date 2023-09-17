using FluentValidation;
using FluentValidation.AspNetCore;

namespace OTUS.HA.SN.Web.Api.Resources;

internal class ModelValidationWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
  {
    builder.Services.AddFluentValidationAutoValidation(fv =>
    {
      fv.DisableDataAnnotationsValidation = true;
      ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
    })
    .AddValidatorsFromAssemblyContaining<OTUS.HA.SN.Web.Api.Model.Input.Validation.AssemblyMarker>()
    ;

    return builder;
  }
}
