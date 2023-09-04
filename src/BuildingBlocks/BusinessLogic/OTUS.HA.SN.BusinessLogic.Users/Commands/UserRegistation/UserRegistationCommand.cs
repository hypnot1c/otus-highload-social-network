using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UserRegistationCommand : IRequest<UserRegistationCommandResult>
  {
    public string Firstname { get; set; }
    public string Secondname { get; set; }
    public DateTime BirthDate { get; set; }
    public string Biography { get; set; }
    public string City { get; set; }
    public string Password { get; set; }
  }
}
