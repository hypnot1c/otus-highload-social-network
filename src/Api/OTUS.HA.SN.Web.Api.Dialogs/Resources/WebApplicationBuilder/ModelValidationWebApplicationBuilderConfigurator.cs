using FluentValidation;
using FluentValidation.AspNetCore;

namespace OTUS.HA.SN.Web.Api.Dialogs.Resources;

internal class ModelValidationWebApplicationBuilderConfigurator : IWebApplicationBuilderConfigurator
{
  public WebApplicationBuilder AddServices(WebApplicationBuilder builder, IConfiguration config)
  {
    builder.Services.AddFluentValidationAutoValidation(fv =>
    {
      fv.DisableDataAnnotationsValidation = true;
      ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
    })
    .AddValidatorsFromAssemblyContaining<OTUS.HA.SN.Web.Api.Dialogs.Model.Input.Validation.AssemblyMarker>()
    ;

    return builder;
  }
}
