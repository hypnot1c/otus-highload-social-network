using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Web.Api.Model.Input;
using OTUS.HA.SN.Web.Api.Model.Output;

namespace OTUS.HA.SN.Web.Api.V1.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  public class DialogsController : ApiBaseController
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public DialogsController(
      IMapper mapper,
      IMediator mediator,
      ILogger<DialogsController> logger
      ) : base(mediator, logger)
    {
      this._mapper = mapper;
    }

    private readonly IMapper _mapper;

    /// <summary>
    /// Отправка сообщения
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="im"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Успешное создание</response>
    [HttpPost("{userId}/send")]
    [ProducesResponseType(typeof(DialogSendOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResultError), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SendMessage(string userId, DialogSendInputModel im, CancellationToken cancellationToken)
    {
      var toUserId = Guid.Empty;
      try
      {
        toUserId = Guid.Parse(userId);
      }
      catch (Exception ex)
      {
        this.Logger.LogError(ex, "Invalid id format");
        return BadRequest("Invalid id format");
      }

      var command = this._mapper.Map<DialogSendCommand>(im);
      command.FromUserId = this.UserId;
      command.ToUserId = toUserId;

      var commandResult = await this.Mediator.Send(command, cancellationToken);

      if (commandResult.Status == StatusEnum.Ok)
      {
        var result = new DialogSendOutputModel();
        result.Description = "Message has been sent successfully";
        return Ok(result);
      }

      if (commandResult.Error is NotFoundResultError notFound)
      {
        return NotFound(notFound);
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{userId}/list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Search(string userId, CancellationToken cancellationToken)
    {
      var toUserId = Guid.Empty;
      try
      {
        toUserId = Guid.Parse(userId);
      }
      catch (Exception ex)
      {
        this.Logger.LogError(ex, "Invalid id format");
        return BadRequest("Invalid id format");
      }

      var query = new DialogGetQuery(this.UserId, toUserId);

      var queryResult = await this.Mediator.Send(query, cancellationToken);

      var result = this._mapper.Map<DialogGetOutputModel>(queryResult);

      return Ok(result.Items);
    }
  }
}
