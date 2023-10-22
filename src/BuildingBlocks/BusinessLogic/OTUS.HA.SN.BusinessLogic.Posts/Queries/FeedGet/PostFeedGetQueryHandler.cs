using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OTUS.HS.SN.Data.DataService;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.BusinessLogic
{
  public class PostFeedGetQueryHandler : BaseQueryHandler, IRequestHandler<PostFeedGetQuery, PostFeedGetQueryResult>
  {
    public PostFeedGetQueryHandler(
      IDataService dataService,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<PostFeedGetQueryHandler> logger
      ) : base(
        mapper,
        masterContext,
        logger
        )
    {
      this.dataService = dataService;
    }

    private readonly IDataService dataService;

    public async Task<PostFeedGetQueryResult> Handle(PostFeedGetQuery request, CancellationToken cancellationToken)
    {
      var result = new PostFeedGetQueryResult();
      try
      {
        result.Items = this.Mapper.Map<IEnumerable<PostGetByIdQueryResult>>(
          await this.dataService.Post_FeedGetForUser(request.UserId, request.Offset, request.Limit, cancellationToken)
          )
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
