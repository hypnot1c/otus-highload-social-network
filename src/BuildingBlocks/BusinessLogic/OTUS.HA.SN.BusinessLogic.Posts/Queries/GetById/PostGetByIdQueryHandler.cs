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
  public class PostGetByIdQueryHandler : BaseQueryHandler, IRequestHandler<PostGetByIdQuery, PostGetByIdQueryResult>
  {
    public PostGetByIdQueryHandler(
      Slave1Context slave1Context,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<PostGetByIdQueryHandler> logger
      ) : base(
        mapper,
        masterContext,
        logger
        )
    {
      this._slave1Context = slave1Context;
    }

    private Slave1Context _slave1Context;

    public async Task<PostGetByIdQueryResult> Handle(PostGetByIdQuery request, CancellationToken cancellationToken)
    {
      PostGetByIdQueryResult result;
      try
      {
        result = await this.Mapper.ProjectTo<PostGetByIdQueryResult>(
          this._slave1Context.Posts
            .Where(u => u.PublicId == request.Id)
          )
          .SingleOrDefaultAsync(cancellationToken)
          ;

        if (result is not null)
        {
          result.Status = StatusEnum.Ok;
          return result;
        }
      }
      catch (Exception ex)
      {
        result = new PostGetByIdQueryResult(new UnexpectedResultError(ex));
        return result;
      }

      result = new PostGetByIdQueryResult(new NotFoundResultError());

      return result;
    }
  }
}
