namespace OTUS.HA.SN.BusinessLogic.Auth
{
  public class UserCreateCommandResult : BaseRequestResult
  {
    public UserCreateCommandResult()
    {

    }
    public UserCreateCommandResult(
      ResultError error
      )
    {
      this.Error = error;
    }
  }
}
