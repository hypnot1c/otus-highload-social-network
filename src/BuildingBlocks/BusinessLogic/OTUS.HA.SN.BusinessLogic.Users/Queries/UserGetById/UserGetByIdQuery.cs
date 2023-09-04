using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UserGetByIdQuery : IRequest<UserGetByIdQueryResult>
  {
    public UserGetByIdQuery(Guid id)
    {
      this.Id = id;
    }

    public Guid Id { get; set; }
  }
}
