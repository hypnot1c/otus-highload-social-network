using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class PostUpdateCommand : IRequest<PostUpdateCommandResult>
  {
    public Guid Id { get; set; }
    public Guid UpdaterId { get; set; }
    public string Text { get; set; }
  }
}
