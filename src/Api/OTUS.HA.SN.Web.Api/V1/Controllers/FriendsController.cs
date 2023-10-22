using MediatR;
using Microsoft.AspNetCore.Mvc;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Web.Api.Model.Output;

namespace OTUS.HA.SN.Web.Api.V1.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  public class FriendsController : ApiBaseController
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public FriendsController(
      IMediator mediator,
      ILogger<FriendsController> logger
      ) : base(mediator, logger)
    {
    }

    /// <summary>
    /// Добавление друга
    /// </summary>
    /// <param name="friendId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Успешная регистрация</response>
    [HttpPut("set/{friendId}")]
    [ProducesResponseType(typeof(FriendAddOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AddFriend(string friendId, CancellationToken cancellationToken)
    {
      var friendTwoId = Guid.Empty;
      try
      {
        friendTwoId = Guid.Parse(friendId);
      }
      catch (Exception ex)
      {
        this.Logger.LogError(ex, "Invalid id format");
        return BadRequest("Invalid id format");
      }

      var command = new FriendAddCommand();
      command.FriendOneId = this.UserId;
      command.FriendTwoId = friendTwoId;

      var commandResult = await this.Mediator.Send(command, cancellationToken);

      if (commandResult.Status == StatusEnum.Ok)
      {
        var result = new FriendAddOutputModel();
        result.Description = "Friend has been added";
        return Ok(result);
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Добавление друга
    /// </summary>
    /// <param name="friendId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Успешная регистрация</response>
    [HttpPut("delete/{friendId}")]
    [ProducesResponseType(typeof(FriendDeleteOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResultError), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteFriend(string friendId, CancellationToken cancellationToken)
    {
      var friendTwoId = Guid.Empty;
      try
      {
        friendTwoId = Guid.Parse(friendId);
      }
      catch (Exception ex)
      {
        this.Logger.LogError(ex, "Invalid id format");
        return BadRequest("Invalid id format");
      }

      var command = new FriendDeleteCommand();
      command.FriendOneId = this.UserId;
      command.FriendTwoId = friendTwoId;

      var commandResult = await this.Mediator.Send(command, cancellationToken);

      if (commandResult.Status == StatusEnum.Ok)
      {
        var result = new FriendDeleteOutputModel();
        result.Description = "Friend has been deleted";
        return Ok(result);
      }

      if (commandResult.Error is NotFoundResultError notFound)
      {
        return NotFound(notFound);
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
}
