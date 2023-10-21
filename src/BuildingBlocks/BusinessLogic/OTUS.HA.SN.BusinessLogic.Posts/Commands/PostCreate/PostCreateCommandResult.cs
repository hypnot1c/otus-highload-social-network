using System;

namespace OTUS.HA.SN.BusinessLogic
{
  public class PostCreateCommandResult : BaseRequestResult
  {
    public PostCreateCommandResult()
    {

    }
    public PostCreateCommandResult(
      ResultError error
      )
    {
      this.Error = error;
    }

    public Guid PostId { get; set; }
  }
}
