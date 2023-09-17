using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OTUS.HA.SN.Web.Api.V1.Controllers
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
  }
}
