using FluentValidation;

namespace OTUS.HA.SN.Web.Api.Model.Input.Validation
{
  public class PostCreateInputModelValidator : AbstractValidator<PostCreateInputModel>
  {
    public PostCreateInputModelValidator()
    {
      RuleFor(p => p.Text)
        .NotEmpty()
        ;
    }
  }
}
