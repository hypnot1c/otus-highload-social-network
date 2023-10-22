namespace OTUS.HA.SN.BusinessLogic
{
  public class FriendAddCommandResult : BaseRequestResult
  {
    public FriendAddCommandResult()
    {

    }
    public FriendAddCommandResult(
      ResultError error
      )
    {
      this.Error = error;
    }
  }
}
