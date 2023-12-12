using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Web.App.Auth.Model.Input;
using OTUS.HA.SN.Web.App.Auth.Model.Output;
using Refit;
using Web.App.Auth.Client;

namespace OTUS.HA.SN.Web.Api.V1.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  [Obsolete]
  public class LoginController : ApiBaseController
  {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authClient"></param>
    /// <param name="mapper"></param>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public LoginController(
      IWebAppAuthClient authClient,
      IMapper mapper,
      IMediator mediator,
      ILogger<LoginController> logger
      )
      : base(mediator, logger)
    {
      this._authClient = authClient;
      Mapper = mapper;
    }

    private readonly IWebAppAuthClient _authClient;

    /// <summary>
    /// 
    /// </summary>
    public IMapper Mapper { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="im"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResultError), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LoginOutputModel>> Login(LoginInputModel im, CancellationToken cancellationToken)
    {
      try
      {
        var res = await this._authClient.Login(im, cancellationToken);
        return Ok(res);
      }
      catch (ApiException ex)
      {
        switch (ex.StatusCode)
        {
          case System.Net.HttpStatusCode.NotFound:
            return NotFound();
          default:
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }
  }
}
