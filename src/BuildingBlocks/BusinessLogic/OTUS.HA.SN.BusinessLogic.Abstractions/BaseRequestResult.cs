namespace OTUS.HA.SN.BusinessLogic
{
  public abstract class BaseRequestResult
  {
    public StatusEnum Status { get; set; }
    public ResultError Error { get; set; }
  }
}
