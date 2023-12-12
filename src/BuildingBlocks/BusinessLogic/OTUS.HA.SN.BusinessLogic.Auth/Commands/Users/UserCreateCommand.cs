using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic.Auth
{
  public class UserCreateCommand : IRequest<UserCreateCommandResult>
  {
    public Guid PublicId { get; set; }
    public string Password { get; set; }
  }
}
