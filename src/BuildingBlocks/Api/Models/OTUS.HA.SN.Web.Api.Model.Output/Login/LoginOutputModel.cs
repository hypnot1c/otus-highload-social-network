namespace OTUS.HA.SN.Web.Api.Model.Output
{
  /// <summary>
  /// 
  /// </summary>
  public class LoginOutputModel
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    public LoginOutputModel(string token)
    {
      Token = token;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Token { get; set; }
  }
}
