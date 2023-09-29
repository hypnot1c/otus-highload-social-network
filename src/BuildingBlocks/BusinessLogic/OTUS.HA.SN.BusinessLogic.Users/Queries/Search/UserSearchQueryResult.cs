using System.Collections.Generic;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UserSearchQueryResult : BaseRequestResult
  {
    public UserSearchQueryResult()
    {

    }
    public UserSearchQueryResult(
      ResultError error
      )
    {
      this.Error = error;
    }

    public IEnumerable<UserGetByIdQueryResult> Items { get; set; }
  }
}
