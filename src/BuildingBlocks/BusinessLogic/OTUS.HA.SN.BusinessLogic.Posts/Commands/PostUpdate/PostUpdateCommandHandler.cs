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
  public class PostUpdateCommandHandler : BaseCommandHandler, IRequestHandler<PostUpdateCommand, PostUpdateCommandResult>
  {
    public PostUpdateCommandHandler(
      IMapper mapper,
      MasterContext masterContext,
      ILogger<PostUpdateCommandHandler> logger
      ) : base(mapper, masterContext, logger)
    {
    }

    public async Task<PostUpdateCommandResult> Handle(PostUpdateCommand request, CancellationToken cancellationToken)
    {
      var post = await this.MasterContext.Posts
        .Where(u => u.PublicId == request.Id)
        .Include(u => u.Author)
        .SingleOrDefaultAsync(cancellationToken)
        ;

      PostUpdateCommandResult result;
      if (post is null)
      {
        result = new PostUpdateCommandResult(new NotFoundResultError());
        return result;
      }

      if (post.Author.PublicId != request.UpdaterId)
      {
        result = new PostUpdateCommandResult(new ForbiddenResultError());
        return result;
      }

      this.Mapper.Map(request, post);

      try
      {
        await this.MasterContext.SaveChangesAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        result = new PostUpdateCommandResult(new UnexpectedResultError(ex));
        return result;
      }

      result = new PostUpdateCommandResult();
      result.Status = StatusEnum.Ok;

      return result;
    }
  }
}
