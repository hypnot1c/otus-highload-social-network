using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OTUS.HA.SN.Data.Dialog.Context;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.BusinessLogic
{
  public class DialogGetQueryHandler : BaseQueryHandler, IRequestHandler<DialogGetQuery, DialogGetQueryResult>
  {
    public DialogGetQueryHandler(
      DialogContext dialogContext,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<DialogGetQueryHandler> logger
      ) : base(
        mapper,
        masterContext,
        logger
        )
    {
      this.DialogContext = dialogContext;
    }

    protected DialogContext DialogContext { get; }

    public async Task<DialogGetQueryResult> Handle(DialogGetQuery request, CancellationToken cancellationToken)
    {
      var result = new DialogGetQueryResult();
      try
      {
        int? fromUserId = await this.MasterContext.Users
          .Where(u => u.PublicId == request.FromUserId)
          .Select(u => u.Id)
          .SingleOrDefaultAsync(cancellationToken)
          ;

        int? toUserId = await this.MasterContext.Users
          .Where(u => u.PublicId == request.ToUserId)
          .Select(u => u.Id)
          .SingleOrDefaultAsync(cancellationToken)
          ;

        result.Items = await this.Mapper.ProjectTo<DialogMessageGetQueryResult>(
          this.DialogContext.UserDialogs
            .Where(d => d.FromUserId == fromUserId)
            .Where(d => d.ToUserId == toUserId)
          )
          .ToListAsync(cancellationToken)
          ;

        foreach (var i in result.Items)
        {
          i.From = request.FromUserId;
          i.To = request.ToUserId;
        }

        result.Status = StatusEnum.Ok;
        return result;
      }
      catch (Exception ex)
      {
        this.Logger.LogError(ex, "Error getting dialog list");
        result = new DialogGetQueryResult(new UnexpectedResultError(ex));
        return result;
      }
    }
  }
}
