using AutoMapper;
using Microsoft.Extensions.Logging;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.BusinessLogic
{
  public abstract class BaseQueryHandler
  {
    public BaseQueryHandler(
      IMapper mapper,
      MasterContext masterContext,
      ILogger<BaseQueryHandler> logger
      )
    {
      this.Mapper = mapper;
      this.MasterContext = masterContext;
      this.Logger = logger;
    }

    public IMapper Mapper { get; }
    public MasterContext MasterContext { get; }
    public ILogger<BaseQueryHandler> Logger { get; }
  }
}
