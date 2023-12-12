using System;

namespace OTUS.HA.SN.BusinessLogic
{
  public class LoginQueryResult : BaseRequestResult
  {
    public LoginQueryResult()
    {

    }
    public LoginQueryResult(
      ResultError error
      )
    {
      this.Error = error;
    }

    public Guid Id { get; set; }
  }
}
