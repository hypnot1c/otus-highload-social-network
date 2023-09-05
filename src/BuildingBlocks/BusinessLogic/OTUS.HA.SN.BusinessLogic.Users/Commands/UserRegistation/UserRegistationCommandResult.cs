using System;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UserRegistationCommandResult : BaseRequestResult
  {
    public UserRegistationCommandResult()
    {

    }
    public UserRegistationCommandResult(
      ResultError error
      )
    {
      this.Error = error;
    }

    public Guid UserId { get; set; }
  }
}
