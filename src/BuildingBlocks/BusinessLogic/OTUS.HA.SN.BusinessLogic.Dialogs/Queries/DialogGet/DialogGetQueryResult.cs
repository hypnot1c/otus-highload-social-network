using System.Collections.Generic;

namespace OTUS.HA.SN.BusinessLogic
{
  public class DialogGetQueryResult : BaseRequestResult
  {
    public DialogGetQueryResult()
    {

    }
    public DialogGetQueryResult(
      ResultError error
      )
    {
      this.Error = error;
    }

    public IEnumerable<DialogMessageGetQueryResult> Items { get; set; }
  }
}
