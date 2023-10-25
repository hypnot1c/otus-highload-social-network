using FluentValidation;

namespace OTUS.HA.SN.Web.Api.Model.Input.Validation
{
  public class DialogSendInputModelValidator : AbstractValidator<DialogSendInputModel>
  {
    public DialogSendInputModelValidator()
    {
      RuleFor(p => p.Text)
        .NotEmpty()
        ;
    }
  }
}
