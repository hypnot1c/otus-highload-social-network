namespace OTUS.HA.SN.Auth.Jwt
{
  public class JwtConfigurationOptions
  {
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
  }
}
