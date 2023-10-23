using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.BusinessLogic
{
  public class DialogGetQueryHandler : BaseQueryHandler, IRequestHandler<DialogGetQuery, DialogGetQueryResult>
  {
    public DialogGetQueryHandler(
      Slave1Context slave1Context,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<DialogGetQueryHandler> logger
      ) : base(
        mapper,
        masterContext,
        logger
        )
    {
      this._slave1Context = slave1Context;
    }

    private Slave1Context _slave1Context;

    public async Task<DialogGetQueryResult> Handle(DialogGetQuery request, CancellationToken cancellationToken)
    {
      var result = new DialogGetQueryResult();
      try
      {
        result.Items = await this.Mapper.ProjectTo<DialogMessageGetQueryResult>(
          this._slave1Context.UserDialogs
            .Where(d => d.FromUser.PublicId == request.FromUserId)
            .Where(d => d.ToUser.PublicId == request.ToUserId)
          )
          .ToListAsync(cancellationToken)
          ;

        result.Status = StatusEnum.Ok;
        return result;
      }
      catch (Exception ex)
      {
        result = new DialogGetQueryResult(new UnexpectedResultError(ex));
        return result;
      }
    }
  }
}
