using AutoMapper;
using MediatR;

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
  }
}
