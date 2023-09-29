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
    /// Регистрация нового пользователя
    /// </summary>
    /// <param name="im"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Успешная регистрация</response>
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserRegistrationOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(UserRegistrationInputModel im, CancellationToken cancellationToken)
    {
      var command = this._mapper.Map<UserRegistationCommand>(im);

      var commandResult = await this.Mediator.Send(command, cancellationToken);

      if (commandResult.Status == StatusEnum.Ok)
      {
        var result = this._mapper.Map<UserRegistrationOutputModel>(commandResult);
        return Ok(result);
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResultError), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
      var query = new UserGetByIdQuery(Guid.Empty);
      try
      {
        query = new UserGetByIdQuery(Guid.Parse(id));
      }
      catch (Exception ex)
      {
        this.Logger.LogError(ex, "Invalid id format");
        return BadRequest("Invalid id format");
      }

      var queryResult = await this.Mediator.Send(query, cancellationToken);

      if (queryResult.Status == StatusEnum.Ok)
      {
        var result = this._mapper.Map<UserGetByIdOutputModel>(queryResult);

        return Ok(result);
      }

      if (queryResult.Error is NotFoundResultError notFound)
      {
        return NotFound(notFound);
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="im"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Search([FromQuery] UserSearchInputModel im, CancellationToken cancellationToken)
    {
      var query = new UserSearchQuery(im.Firstname, im.Lastname);

      var queryResult = await this.Mediator.Send(query, cancellationToken);

      var result = this._mapper.Map<UserSearchOutputModel>(queryResult);

      return Ok(result.Items);
    }
  }
}
