using FluentValidation;

namespace OTUS.HA.SN.Web.Api.Model.Input.Validation
{
  public class PostFeedGetInputModelValidator : AbstractValidator<PostFeedGetInputModel>
  {
    public PostFeedGetInputModelValidator()
    {
      RuleFor(p => p.Offset)
        .NotNull()
        .Must(v => v >= 0)
        ;

      RuleFor(p => p.Limit)
        .NotNull()
        .Must(v => v >= 1)
        ;
    }
  }
}
