using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Web.Api.Model.Output;

namespace OTUS.HA.SN.Web.Api.Counters.V1.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  public class CountersController : ApiBaseController
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public CountersController(
      IMapper mapper,
      IMediator mediator,
      ILogger<CountersController> logger
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
      var query = new UserCounterGetByIdQuery(Guid.Empty);
      try
      {
        query = new UserCounterGetByIdQuery(Guid.Parse(id));
      }
      catch (Exception ex)
      {
        this.Logger.LogError(ex, "Invalid id format");
        return BadRequest("Invalid id format");
      }

      var queryResult = await this.Mediator.Send(query, cancellationToken);

      if (queryResult.Status == StatusEnum.Ok)
      {
        var result = this._mapper.Map<UserCounterGetByIdOutputModel>(queryResult);

        return Ok(result);
      }

      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
}
