using System;
using FluentValidation;

namespace OTUS.HA.SN.Web.Api.Model.Input.Validation
{
  public class PostUpdateInputModelValidator : AbstractValidator<PostUpdateInputModel>
  {
    public PostUpdateInputModelValidator()
    {
      RuleFor(p => p.Id)
        .NotEmpty()
        .Must(id => Guid.TryParse(id, out var _))
        .WithMessage("Invalid id format")
        ;

      RuleFor(p => p.Text)
        .NotEmpty()
        ;
    }
  }
}
