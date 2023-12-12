using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.BusinessLogic.Auth;
using OTUS.HA.SN.Web.App.Auth.Model.Input;

namespace OTUS.HA.SN.Web.App.Auth.V1.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  public class UsersController : ApiBaseController
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public UsersController(
      IMapper mapper,
      IMediator mediator,
      ILogger<UsersController> logger
      ) : base(mediator, logger)
    {
      this._mapper = mapper;
    }

    private readonly IMapper _mapper;

    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    /// <param name="im"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Успешная регистрация</response>
    [HttpPost()]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(UserCreateInputModel im, CancellationToken cancellationToken)
    {
      var command = this._mapper.Map<UserCreateCommand>(im);

      var commandResult = await this.Mediator.Send(command, cancellationToken);

      if (commandResult.Status == StatusEnum.Ok)
      {
        return Ok();
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
}
