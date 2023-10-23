using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class DialogSendCommand : IRequest<DialogSendCommandResult>
  {
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public string Text { get; set; }
  }
}
