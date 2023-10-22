using System.Collections.Generic;

namespace OTUS.HA.SN.BusinessLogic
{
  public class PostFeedGetQueryResult : BaseRequestResult
  {
    public PostFeedGetQueryResult()
    {

    }
    public PostFeedGetQueryResult(
      ResultError error
      )
    {
      this.Error = error;
    }

    public IEnumerable<PostGetByIdQueryResult> Items { get; set; }
  }
}
