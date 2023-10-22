namespace OTUS.HA.SN.BusinessLogic
{
  public class ForbiddenResultError : ResultError
  {
    public ForbiddenResultError() : base()
    {
      this.Code = 2;
      this.Message = "Access denied";
    }
  }
}
