using System;
using FluentValidation;

namespace OTUS.HA.SN.Web.Api.Model.Input.Validation
{
  public class UserSearchInputModelValidator : AbstractValidator<UserSearchInputModel>
  {
    public UserSearchInputModelValidator()
    {
      RuleFor(p => p.Firstname)
        .NotEmpty()
        .When(p => String.IsNullOrWhiteSpace(p.Lastname))
        ;

      RuleFor(p => p.Lastname)
        .NotEmpty()
        .When(p => String.IsNullOrWhiteSpace(p.Firstname))
        ;
    }
  }
}
