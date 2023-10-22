using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OTUS.HS.SN.Data.Master.Context;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HA.SN.BusinessLogic
{
  public class FriendAddCommandHandler : BaseCommandHandler, IRequestHandler<FriendAddCommand, FriendAddCommandResult>
  {
    public FriendAddCommandHandler(
      IMapper mapper,
      MasterContext masterContext,
      ILogger<FriendAddCommandHandler> logger
      ) : base(mapper, masterContext, logger)
    {
    }

    public async Task<FriendAddCommandResult> Handle(FriendAddCommand request, CancellationToken cancellationToken)
    {
      var friendIds = new Guid[] { request.FriendOneId, request.FriendTwoId };

      var friends = await this.MasterContext.Users
        .Where(u => friendIds.Contains(u.PublicId))
        .Select(u => u.Id)
        .ToListAsync(cancellationToken)
        ;

      FriendAddCommandResult result;
      if (friends.Count < 2)
      {
        result = new FriendAddCommandResult(new NotFoundResultError());
        return result;
      }

      var friendModel = await this.MasterContext.Friends
        .FindAsync(new object[] { friends[0], friends[1] }, cancellationToken: cancellationToken)
        ;

      if (friendModel is not null)
      {
        result = new FriendAddCommandResult();
        result.Status = StatusEnum.Ok;

        return result;
      }

      friendModel = new FriendsModel { FriendOneId = friends[0], FriendTwoId = friends[1] };
      this.MasterContext.Friends.Add(friendModel);

      try
      {
        await this.MasterContext.SaveChangesAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        result = new FriendAddCommandResult(new UnexpectedResultError(ex));
        return result;
      }

      result = new FriendAddCommandResult();
      result.Status = StatusEnum.Ok;

      return result;
    }
  }
}
