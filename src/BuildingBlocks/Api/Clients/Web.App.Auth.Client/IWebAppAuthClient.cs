using OTUS.HA.SN.Web.App.Auth.Model.Input;
using OTUS.HA.SN.Web.App.Auth.Model.Output;
using Refit;

namespace Web.App.Auth.Client
{
  public interface IWebAppAuthClient
  {
    [Post("/v1/users")]
    Task UserCreate(UserCreateInputModel im, CancellationToken cancellationToken);

    [Post("/v1/login")]
    Task<LoginOutputModel> Login(LoginInputModel im, CancellationToken cancellationToken);
  }
}
