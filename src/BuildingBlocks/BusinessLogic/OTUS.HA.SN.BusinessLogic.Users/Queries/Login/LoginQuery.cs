using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class LoginQuery : IRequest<LoginQueryResult>
  {
    public Guid Id { get; set; }
    public string Password { get; set; }
  }
}
