namespace OTUS.HA.SN.Auth.Jwt
{
  public interface IJwtTokenService
  {
    string GetToken(UserPrincipalModel user);
  }
}
