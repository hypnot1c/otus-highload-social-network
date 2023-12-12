using OTUS.HA.SN.Web.App.Auth.Model.Input;
using Refit;

namespace Web.App.Auth.Client
{
  public interface IWebAppAuthClient
  {
    [Post("/v1/users")]
    Task UserCreate(UserCreateInputModel im);
  }
}
