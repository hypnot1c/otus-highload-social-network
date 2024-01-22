using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UserCounterGetByIdQuery : IRequest<UserCounterGetByIdQueryResult>
  {
    public UserCounterGetByIdQuery(Guid id)
    {
      this.Id = id;
    }

    public Guid Id { get; set; }
  }
}
