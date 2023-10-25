using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class DialogGetQuery : IRequest<DialogGetQueryResult>
  {
    public DialogGetQuery(Guid fromUserId, Guid toUserId)
    {
      FromUserId = fromUserId;
      ToUserId = toUserId;
    }

    public Guid FromUserId { get; }
    public Guid ToUserId { get; }
  }
}
