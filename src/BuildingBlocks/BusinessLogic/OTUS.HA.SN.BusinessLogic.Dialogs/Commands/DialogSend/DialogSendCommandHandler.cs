using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OTUS.HA.SN.Data.Dialog.Context;
using OTUS.HA.SN.Data.Dialog.Model;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.BusinessLogic
{
  public class DialogSendCommandHandler : BaseCommandHandler, IRequestHandler<DialogSendCommand, DialogSendCommandResult>
  {
    public DialogSendCommandHandler(
      DialogContext dialogContext,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<DialogSendCommandHandler> logger
      ) : base(mapper, masterContext, logger)
    {
      DialogContext = dialogContext;
    }

    protected DialogContext DialogContext { get; }

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

      var dialogModel = this.Mapper.Map<UserDialogModel>(request);
      dialogModel.FromUserId = fromUserId.Value;
      dialogModel.ToUserId = toUserId.Value;

      this.DialogContext.UserDialogs.Add(dialogModel);

      try
      {
        await this.DialogContext.SaveChangesAsync(cancellationToken);
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