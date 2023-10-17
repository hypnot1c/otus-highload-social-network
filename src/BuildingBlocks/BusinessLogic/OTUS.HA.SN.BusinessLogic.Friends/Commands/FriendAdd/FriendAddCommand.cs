using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class FriendAddCommand : IRequest<FriendAddCommandResult>
  {
    public Guid FriendOneId { get; set; }
    public Guid FriendTwoId { get; set; }
  }
}
