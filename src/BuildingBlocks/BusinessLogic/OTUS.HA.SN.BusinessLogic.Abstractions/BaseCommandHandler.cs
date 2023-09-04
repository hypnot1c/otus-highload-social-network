using AutoMapper;
using Microsoft.Extensions.Logging;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.BusinessLogic
{
  public abstract class BaseCommandHandler
  {
    public BaseCommandHandler(
      IMapper mapper,
      MasterContext masterContext,
      ILogger<BaseCommandHandler> logger
      )
    {
      this.Mapper = mapper;
      this.MasterContext = masterContext;
      this.Logger = logger;
    }

    public IMapper Mapper { get; }
    public MasterContext MasterContext { get; }
    public ILogger<BaseCommandHandler> Logger { get; }
  }
}
