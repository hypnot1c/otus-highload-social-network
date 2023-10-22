using System.Collections.Generic;

namespace OTUS.HA.SN.Web.Api.Model.Output
{
  /// <summary>
  /// 
  /// </summary>
  public class PostFeedGetOutputModel
  {
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<PostGetByIdOutputModel> Items { get; set; }
  }
}
