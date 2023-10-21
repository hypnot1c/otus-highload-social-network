namespace OTUS.HA.SN.BusinessLogic
{
  public class PostDeleteCommandResult : BaseRequestResult
  {
    public PostDeleteCommandResult()
    {

    }
    public PostDeleteCommandResult(
      ResultError error
      )
    {
      this.Error = error;
    }
  }
}
