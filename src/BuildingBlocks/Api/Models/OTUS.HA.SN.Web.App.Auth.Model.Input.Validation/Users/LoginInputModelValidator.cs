using FluentValidation;

namespace OTUS.HA.SN.Web.App.Auth.Model.Input
{
  public class LoginInputModelValidator : AbstractValidator<LoginInputModel>
  {
    public LoginInputModelValidator()
    {
      RuleFor(p => p.Id)
      .NotEmpty();
      RuleFor(p => p.Password)
      .NotEmpty();
    }
  }
}
