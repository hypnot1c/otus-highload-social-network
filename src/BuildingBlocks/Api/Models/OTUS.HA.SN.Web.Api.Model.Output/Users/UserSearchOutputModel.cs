using System.Collections.Generic;

namespace OTUS.HA.SN.Web.Api.Model.Output
{
  /// <summary>
  /// 
  /// </summary>
  public class UserSearchOutputModel
  {
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<UserGetByIdOutputModel> Items { get; set; }
  }
}
