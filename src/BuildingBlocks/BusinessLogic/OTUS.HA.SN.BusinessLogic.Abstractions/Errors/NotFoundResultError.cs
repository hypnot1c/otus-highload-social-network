namespace OTUS.HA.SN.BusinessLogic
{
  public class NotFoundResultError : ResultError
  {
    public NotFoundResultError() : base()
    {
      this.Code = 1;
      this.Message = "Resource not found";
    }
  }
}
