using FluentValidation;

namespace OTUS.HA.SN.Web.App.Auth.Model.Input.Validation
{
  public class UserCreateModelValidator : AbstractValidator<UserCreateInputModel>
  {
    public UserCreateModelValidator()
    {
      RuleFor(p => p.PublicId)
      .NotEmpty()
      ;

      RuleFor(p => p.Password)
      .NotEmpty()
      ;
    }
  }
}
