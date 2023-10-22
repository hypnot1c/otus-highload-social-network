using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class FriendDeleteCommand : IRequest<FriendDeleteCommandResult>
  {
    public Guid FriendOneId { get; set; }
    public Guid FriendTwoId { get; set; }
  }
}
