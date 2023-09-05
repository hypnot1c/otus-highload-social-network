using FluentValidation;

namespace OTUS.HA.SN.Web.Api.Model.Input.Validation
{
  public class UserRegistrationInputModelValidator : AbstractValidator<UserRegistrationInputModel>
  {
    public UserRegistrationInputModelValidator()
    {
      RuleFor(p => p.Password)
        .NotEmpty()
        ;

      RuleFor(p => p.Firstname)
        .NotEmpty()
        ;

      RuleFor(p => p.Secondname)
        .NotEmpty()
        ;

      RuleFor(p => p.BirthDate)
        .NotEmpty()
        ;
    }
  }
}
