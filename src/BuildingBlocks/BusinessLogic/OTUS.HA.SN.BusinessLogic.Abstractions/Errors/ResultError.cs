using System;

namespace OTUS.HA.SN.BusinessLogic
{
  public abstract class ResultError
  {
    public ResultError()
    {

    }
    public ResultError(int code, string message)
    {
      this.Code = code;
      this.Message = message;
    }

    public ResultError(int code, string message, Exception exception)
      : this(code, message)
    {
      this.Exception = exception;
    }

    public int Code { get; set; }
    public string Message { get; set; }
    public Exception Exception { get; set; }
  }
}
