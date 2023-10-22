namespace OTUS.HA.SN.BusinessLogic
{
  public class FriendDeleteCommandResult : BaseRequestResult
  {
    public FriendDeleteCommandResult()
    {

    }
    public FriendDeleteCommandResult(
      ResultError error
      )
    {
      this.Error = error;
    }
  }
}
