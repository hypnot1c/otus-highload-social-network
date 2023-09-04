using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Web.Api.Model.Input;
using OTUS.HA.SN.Web.Api.Model.Output;

namespace OTUS.HA.SN.Web.Api.V1.Controllers
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
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
      var query = new UserGetByIdQuery(Guid.Parse(id));

      var queryResult = await this.Mediator.Send(query, cancellationToken);

      var result = this._mapper.Map<UserGetByIdOutputModel>(queryResult);

      return Ok(result);
    }

    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    /// <param name="im"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Успешная регистрация</response>
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserRegistrationOutputModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(UserRegistrationInputModel im, CancellationToken cancellationToken)
    {
      var command = this._mapper.Map<UserRegistationCommand>(im);

      var commandResult = await this.Mediator.Send(command, cancellationToken);

      var result = this._mapper.Map<UserRegistrationOutputModel>(commandResult);

      return Ok(result);
    }
  }
}
