using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OTUS.HA.SN.Web.App.Auth.V1.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  [ApiController]
  [Route("v1/[controller]")]
  [Produces("application/json")]
  public class ApiBaseController : Controller
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public ApiBaseController(
      IMediator mediator,
      ILogger<ApiBaseController> logger
      )
    {
      this.Mediator = mediator;
      this.Logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    public IMediator Mediator { get; }
    /// <summary>
    /// 
    /// </summary>
    public ILogger<ApiBaseController> Logger { get; }

    /// <summary>
    /// 
    /// </summary>
    protected Guid UserId
    {
      get
      {
        var claimValue = this.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        if (claimValue == null)
          return Guid.Empty;

        return Guid.Parse(claimValue);
      }
    }
  }
}
