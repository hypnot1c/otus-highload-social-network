using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OTUS.HA.SN.Auth.Jwt;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Web.Api.Model.Input;
using OTUS.HA.SN.Web.Api.Model.Output;

namespace OTUS.HA.SN.Web.Api.V1.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  [AllowAnonymous]
  public class LoginController : ApiBaseController
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="jwtTokenService"></param>
    /// <param name="mapper"></param>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public LoginController(
      IJwtTokenService jwtTokenService,
      IMapper mapper,
      IMediator mediator,
      ILogger<LoginController> logger
      )
      : base(mediator, logger)
    {
      this.JwtTokenService = jwtTokenService;
      Mapper = mapper;
    }

    /// <summary>
    /// 
    /// </summary>
    public IJwtTokenService JwtTokenService { get; }
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
    public async Task<IActionResult> Login(LoginInputModel im, CancellationToken cancellationToken)
    {
      var loginQuery = this.Mapper.Map<LoginQuery>(im);

      var loginQueryResult = await this.Mediator.Send(loginQuery, cancellationToken);

      if (loginQueryResult.Status == StatusEnum.Ok)
      {
        var userPrincipal = this.Mapper.Map<UserPrincipalModel>(loginQueryResult);

        var token = this.JwtTokenService.GetToken(userPrincipal);
        return Ok(new LoginOutputModel(token));
      }

      if (loginQueryResult.Error is NotFoundResultError notFound)
      {
        return NotFound(notFound);
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
}
