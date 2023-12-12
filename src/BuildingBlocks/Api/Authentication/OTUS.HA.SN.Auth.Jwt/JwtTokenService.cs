using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace OTUS.HA.SN.Auth.Jwt
{
  public class JwtTokenService : IJwtTokenService
  {
    public JwtTokenService(
      IOptions<JwtConfigurationOptions> JwtOptions
      )
    {
      this.JwtOptions = JwtOptions.Value;
    }

    public JwtConfigurationOptions JwtOptions { get; }

    public string GetToken(UserPrincipalModel user)
    {
      var issuer = JwtOptions.Issuer;
      var audience = JwtOptions.Audience;
      var key = Encoding.ASCII.GetBytes(JwtOptions.Key);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
        {
          new Claim("Id", user.Id),
          new Claim(JwtRegisteredClaimNames.Sub, user.Id),
          new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
        }),
        Expires = DateTime.UtcNow.AddMinutes(5),
        Issuer = issuer,
        Audience = audience,
        SigningCredentials = new SigningCredentials
          (new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha512Signature)
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var jwtToken = tokenHandler.WriteToken(token);
      var stringToken = tokenHandler.WriteToken(token);

      return stringToken;
    }
  }
}
