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
  public class PostsController : ApiBaseController
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public PostsController(
      IMapper mapper,
      IMediator mediator,
      ILogger<PostsController> logger
      ) : base(mediator, logger)
    {
      this._mapper = mapper;
    }

    private readonly IMapper _mapper;

    /// <summary>
    /// Создание поста
    /// </summary>
    /// <param name="im"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Успешное создание</response>
    [HttpPost("create")]
    [ProducesResponseType(typeof(PostCreateOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResultError), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePost(PostCreateInputModel im, CancellationToken cancellationToken)
    {
      var command = this._mapper.Map<PostCreateCommand>(im);
      command.AuthorId = this.UserId;

      var commandResult = await this.Mediator.Send(command, cancellationToken);

      if (commandResult.Status == StatusEnum.Ok)
      {
        var result = this._mapper.Map<PostCreateOutputModel>(commandResult);
        return Ok(result);
      }

      if (commandResult.Error is NotFoundResultError notFound)
      {
        return NotFound(notFound);
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Получение поста по id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Пост найден и доступен</response>
    [HttpGet("get/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResultError), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPost(string id, CancellationToken cancellationToken)
    {
      var query = new PostGetByIdQuery(Guid.Empty);
      try
      {
        query = new PostGetByIdQuery(Guid.Parse(id));
      }
      catch (Exception ex)
      {
        this.Logger.LogError(ex, "Invalid id format");
        return BadRequest("Invalid id format");
      }

      var queryResult = await this.Mediator.Send(query, cancellationToken);

      if (queryResult.Status == StatusEnum.Ok)
      {
        var result = this._mapper.Map<PostGetByIdOutputModel>(queryResult);

        return Ok(result);
      }

      if (queryResult.Error is NotFoundResultError notFound)
      {
        return NotFound(notFound);
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Обновление поста
    /// </summary>
    /// <param name="im"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Успешное обновление</response>
    [HttpPut("update")]
    [ProducesResponseType(typeof(PostUpdateOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ForbiddenResultError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(NotFoundResultError), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdatePost(PostUpdateInputModel im, CancellationToken cancellationToken)
    {
      var command = this._mapper.Map<PostUpdateCommand>(im);
      command.UpdaterId = this.UserId;

      var commandResult = await this.Mediator.Send(command, cancellationToken);

      if (commandResult.Status == StatusEnum.Ok)
      {
        var result = new PostUpdateOutputModel();
        result.Description = "Post has been updated successfully";
        return Ok(result);
      }

      if (commandResult.Error is NotFoundResultError notFound)
      {
        return NotFound(notFound);
      }

      if (commandResult.Error is ForbiddenResultError _)
      {
        return Forbid();
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Удаление поста
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Успешное обновление</response>
    [HttpPut("delete/{id}")]
    [ProducesResponseType(typeof(PostDeleteOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ForbiddenResultError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(NotFoundResultError), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletePost(string id, CancellationToken cancellationToken)
    {
      var isValidPostId = Guid.TryParse(id, out var postId);
      if (!isValidPostId)
      {
        return BadRequest("Invalid id format");
      }

      var command = new PostDeleteCommand(postId, this.UserId);

      var commandResult = await this.Mediator.Send(command, cancellationToken);

      if (commandResult.Status == StatusEnum.Ok)
      {
        var result = new PostDeleteOutputModel();
        result.Description = "Post has been deleted successfully";
        return Ok(result);
      }

      if (commandResult.Error is NotFoundResultError notFound)
      {
        return NotFound(notFound);
      }

      if (commandResult.Error is ForbiddenResultError _)
      {
        return Forbid();
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Лента постов друзей
    /// </summary>
    /// <param name="im"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Успешное обновление</response>
    [HttpGet("feed")]
    [ProducesResponseType(typeof(PostDeleteOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFeed([FromQuery] PostFeedGetInputModel im, CancellationToken cancellationToken)
    {
      var command = this._mapper.Map<PostFeedGetQuery>(im);
      command.UserId = this.UserId;

      var queryResult = await this.Mediator.Send(command, cancellationToken);

      if (queryResult.Status == StatusEnum.Ok)
      {
        var result = this._mapper.Map<PostFeedGetOutputModel>(queryResult);
        return Ok(result);
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
}
