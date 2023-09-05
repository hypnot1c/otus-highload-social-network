using System;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UnexpectedResultError : ResultError
  {
    public UnexpectedResultError(Exception ex)
    {
      this.Code = 0;
      this.Message = "Unexpected error";
      this.Exception = ex;
    }
  }
}
