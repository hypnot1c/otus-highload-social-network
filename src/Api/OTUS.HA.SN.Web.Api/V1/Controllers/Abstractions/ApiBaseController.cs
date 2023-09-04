using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OTUS.HA.SN.Web.Api.V1.Controllers
{
  [ApiController]
  [Route("v1/[controller]")]
  [Produces("application/json")]
  public class ApiBaseController : Controller
  {
    public ApiBaseController(
      IMediator mediator,
      ILogger<ApiBaseController> logger
      )
    {
      this.Mediator = mediator;
      this.Logger = logger;
    }

    public IMediator Mediator { get; }
    public ILogger<ApiBaseController> Logger { get; }
  }
}
