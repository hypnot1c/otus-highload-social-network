using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class PostDeleteCommand : IRequest<PostDeleteCommandResult>
  {
    public PostDeleteCommand(Guid id, Guid deleterId)
    {
      Id = id;
      DeleterId = deleterId;
    }
    public Guid Id { get; set; }
    public Guid DeleterId { get; set; }
  }
}
