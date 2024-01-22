using System;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UserCounterGetByIdQueryResult : BaseRequestResult
  {
    public UserCounterGetByIdQueryResult()
    {

    }
    public UserCounterGetByIdQueryResult(
      ResultError error
      )
    {
      this.Error = error;
    }

    public Guid Id { get; set; }
    public int UnreadMessages { get; set; }
  }
}
