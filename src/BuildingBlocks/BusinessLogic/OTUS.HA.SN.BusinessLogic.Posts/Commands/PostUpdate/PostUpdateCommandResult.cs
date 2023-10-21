namespace OTUS.HA.SN.BusinessLogic
{
  public class PostUpdateCommandResult : BaseRequestResult
  {
    public PostUpdateCommandResult()
    {

    }
    public PostUpdateCommandResult(
      ResultError error
      )
    {
      this.Error = error;
    }
  }
}
