using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class PostCreateCommand : IRequest<PostCreateCommandResult>
  {
    public Guid AuthorId { get; set; }
    public string Text { get; set; }
  }
}
