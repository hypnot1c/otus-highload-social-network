using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UserCounterGetByIdQueryHandler : BaseQueryHandler, IRequestHandler<UserCounterGetByIdQuery, UserCounterGetByIdQueryResult>
  {
    public UserCounterGetByIdQueryHandler(
      IDistributedCache distributedCache,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<UserCounterGetByIdQueryHandler> logger
      ) : base(
        mapper,
        masterContext,
        logger
        )
    {
      this._distributedCache = distributedCache;
    }

    private IDistributedCache _distributedCache;

    public async Task<UserCounterGetByIdQueryResult> Handle(UserCounterGetByIdQuery request, CancellationToken cancellationToken)
    {
      UserCounterGetByIdQueryResult result;
      try
      {
        var userUnreadMessagesCount = await this._distributedCache.GetOrCreate<int>(
          $"user-{request.Id}",
          (opts, cancellationToken) =>
          {
            return Task.FromResult(0);
          },
          cancellationToken
        );

        result = new UserCounterGetByIdQueryResult();
        result.UnreadMessages = userUnreadMessagesCount;
        result.Status = StatusEnum.Ok;
        return result;
      }
      catch (Exception ex)
      {
        result = new UserCounterGetByIdQueryResult(new UnexpectedResultError(ex));
        return result;
      }
    }
  }
}
