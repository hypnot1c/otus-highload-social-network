namespace OTUS.HA.SN.Web.Api.Model.Output
{
  public class LoginOutputModel
  {
    public LoginOutputModel(string token)
    {
      Token = token;
    }
    public string Token { get; set; }
  }
}
