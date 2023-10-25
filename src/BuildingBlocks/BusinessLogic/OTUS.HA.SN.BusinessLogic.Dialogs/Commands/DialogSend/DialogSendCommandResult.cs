namespace OTUS.HA.SN.BusinessLogic
{
  public class DialogSendCommandResult : BaseRequestResult
  {
    public DialogSendCommandResult()
    {

    }
    public DialogSendCommandResult(
      ResultError error
      )
    {
      this.Error = error;
    }
  }
}
