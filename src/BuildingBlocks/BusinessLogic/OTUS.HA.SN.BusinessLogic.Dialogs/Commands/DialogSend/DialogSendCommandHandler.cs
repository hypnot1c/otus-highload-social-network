using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OTUS.HS.SN.Data.Master.Context;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;

namespace OTUS.HA.SN.BusinessLogic
{
  public class DialogSendCommandHandler : BaseCommandHandler, IRequestHandler<DialogSendCommand, DialogSendCommandResult>
  {
    public DialogSendCommandHandler(
      Box tarantoolBox,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<DialogSendCommandHandler> logger
      ) : base(mapper, masterContext, logger)
    {
      TarantoolBox = tarantoolBox;
    }

    protected Box TarantoolBox { get; }

    public async Task<DialogSendCommandResult> Handle(DialogSendCommand request, CancellationToken cancellationToken)
    {
      int? toUserId = await this.MasterContext.Users
        .Where(u => u.PublicId == request.ToUserId)
        .Select(u => u.Id)
        .SingleOrDefaultAsync(cancellationToken)
        ;

      DialogSendCommandResult result;
      if (toUserId is null)
      {
        result = new DialogSendCommandResult(new NotFoundResultError());
        return result;
      }

      int? fromUserId = await this.MasterContext.Users
        .Where(u => u.PublicId == request.FromUserId)
        .Select(u => u.Id)
        .SingleOrDefaultAsync(cancellationToken)
        ;

      try
      {
        await this.TarantoolBox.Call<TarantoolTuple<string, int, int, string, string>>("user_dialog_insert",
            new TarantoolTuple<string, int, int, string, string>(
              Guid.NewGuid().ToString(),
              fromUserId.Value,
              toUserId.Value,
              request.Text,
              DateTime.UtcNow.ToString()
            )
          );
      }
      catch (Exception ex)
      {
        result = new DialogSendCommandResult(new UnexpectedResultError(ex));
        return result;
      }

      result = new DialogSendCommandResult();
      result.Status = StatusEnum.Ok;

      return result;
    }
  }
}
