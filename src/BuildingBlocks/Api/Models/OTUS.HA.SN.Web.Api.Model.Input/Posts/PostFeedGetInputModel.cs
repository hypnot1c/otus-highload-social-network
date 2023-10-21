namespace OTUS.HA.SN.Web.Api.Model.Input
{
  /// <summary>
  /// 
  /// </summary>
  public class PostFeedGetInputModel
  {
    /// <summary>
    /// 
    /// </summary>
    public PostFeedGetInputModel()
    {
      this.Offset = 0;
      this.Limit = 10;
    }
    /// <summary>
    /// 
    /// </summary>
    public int Offset { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int Limit { get; set; }
  }
}
