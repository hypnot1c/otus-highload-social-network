using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace OTUS.HA.SN.Web.AsyncApi.Versions
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public abstract class BaseHub<T> : Hub<T> where T : class
  {
    public BaseHub(ILogger<BaseHub<T>> logger)
    {
      this.Logger = logger;
    }

    public ILogger<BaseHub<T>> Logger { get; }

    protected int UserId => GetIntValue(JwtRegisteredClaimNames.Sub);

    private int GetIntValue(string key)
    {
      var claim = this.Context.User.Claims.FirstOrDefault(c => c.Type == key);
      if (claim == null)
      {
        this.Logger.LogError("Not found claim by key {0}", key);
        throw new ArgumentException("Not found claim by key", nameof(key));
      }
      if (int.TryParse(claim.Value, out int result))
      {
        return result;
      }
      return 0;
    }
  }
}
