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
  public class FriendDeleteCommandHandler : BaseCommandHandler, IRequestHandler<FriendDeleteCommand, FriendDeleteCommandResult>
  {
    public FriendDeleteCommandHandler(
      IMapper mapper,
      MasterContext masterContext,
      ILogger<FriendDeleteCommandHandler> logger
      ) : base(mapper, masterContext, logger)
    {
    }

    public async Task<FriendDeleteCommandResult> Handle(FriendDeleteCommand request, CancellationToken cancellationToken)
    {
      var friendIds = new Guid[] { request.FriendOneId, request.FriendTwoId };

      var friendEntry = await this.MasterContext.Friends
        .Where(f => f.FriendOne.PublicId == request.FriendOneId)
        .Where(f => f.FriendTwo.PublicId == request.FriendTwoId)
        .SingleOrDefaultAsync(cancellationToken)
        ;

      FriendDeleteCommandResult result;
      if (friendEntry is null)
      {
        result = new FriendDeleteCommandResult(new NotFoundResultError());
        result.Status = StatusEnum.Error;
        return result;
      }

      this.MasterContext.Friends.Remove(friendEntry);

      try
      {
        await this.MasterContext.SaveChangesAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        result = new FriendDeleteCommandResult(new UnexpectedResultError(ex));
        return result;
      }

      result = new FriendDeleteCommandResult();
      result.Status = StatusEnum.Ok;

      return result;
    }
  }
}
