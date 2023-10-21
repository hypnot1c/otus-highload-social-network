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
  public class PostFeedGetQueryHandler : BaseQueryHandler, IRequestHandler<PostFeedGetQuery, PostFeedGetQueryResult>
  {
    public PostFeedGetQueryHandler(
      Slave1Context slave1Context,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<PostFeedGetQueryHandler> logger
      ) : base(
        mapper,
        masterContext,
        logger
        )
    {
      this._slave1Context = slave1Context;
    }

    private Slave1Context _slave1Context;

    public async Task<PostFeedGetQueryResult> Handle(PostFeedGetQuery request, CancellationToken cancellationToken)
    {
      var result = new PostFeedGetQueryResult();
      try
      {
        result.Items = await this.Mapper.ProjectTo<PostGetByIdQueryResult>(
          this._slave1Context.Friends
            .Where(p => p.FriendOne.PublicId == request.UserId || p.FriendTwo.PublicId == request.UserId)
            .SelectMany(u => u.FriendOne.Posts)
            .Union(
              this._slave1Context.Friends
                .Where(p => p.FriendOne.PublicId == request.UserId || p.FriendTwo.PublicId == request.UserId)
                .SelectMany(u => u.FriendTwo.Posts)
                )
          )
          .ToListAsync(cancellationToken)
          ;

        if (result is not null)
        {
          result.Status = StatusEnum.Ok;
          return result;
        }
      }
      catch (Exception ex)
      {
        result = new PostFeedGetQueryResult(new UnexpectedResultError(ex));
        return result;
      }

      return result;
    }
  }
}
