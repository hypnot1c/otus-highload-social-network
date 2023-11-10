using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OTUS.HA.SN.Data.Dialog.TarantoolModel;
using OTUS.HS.SN.Data.Master.Context;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;

namespace OTUS.HA.SN.BusinessLogic
{
  public class DialogGetQueryHandler : BaseQueryHandler, IRequestHandler<DialogGetQuery, DialogGetQueryResult>
  {
    public DialogGetQueryHandler(
      Box tarantoolBox,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<DialogGetQueryHandler> logger
      ) : base(
        mapper,
        masterContext,
        logger
        )
    {
      TarantoolBox = tarantoolBox;
    }

    protected Box TarantoolBox { get; }

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

        var schema = this.TarantoolBox.GetSchema();

        var space = schema["user_dialog"];

        var secondaryIndex = space["secondary"];

        var res = await secondaryIndex.Select<TarantoolTuple<int, int>, UserDialogModel>(TarantoolTuple.Create(fromUserId.Value, toUserId.Value), new SelectOptions { Iterator = Iterator.All });

        result.Items = this.Mapper.Map<DialogMessageGetQueryResult[]>(res.Data);

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
