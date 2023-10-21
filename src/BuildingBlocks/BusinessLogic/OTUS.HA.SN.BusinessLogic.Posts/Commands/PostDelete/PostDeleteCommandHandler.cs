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
  public class PostDeleteCommandHandler : BaseCommandHandler, IRequestHandler<PostDeleteCommand, PostDeleteCommandResult>
  {
    public PostDeleteCommandHandler(
      IMapper mapper,
      MasterContext masterContext,
      ILogger<PostDeleteCommandHandler> logger
      ) : base(mapper, masterContext, logger)
    {
    }

    public async Task<PostDeleteCommandResult> Handle(PostDeleteCommand request, CancellationToken cancellationToken)
    {
      var post = await this.MasterContext.Posts
        .Where(u => u.PublicId == request.Id)
        .Include(u => u.Author)
        .SingleOrDefaultAsync(cancellationToken)
        ;

      PostDeleteCommandResult result;
      if (post is null)
      {
        result = new PostDeleteCommandResult(new NotFoundResultError());
        return result;
      }

      if (post.Author.PublicId != request.DeleterId)
      {
        result = new PostDeleteCommandResult(new ForbiddenResultError());
        return result;
      }

      this.MasterContext.Posts.Remove(post);

      try
      {
        await this.MasterContext.SaveChangesAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        result = new PostDeleteCommandResult(new UnexpectedResultError(ex));
        return result;
      }

      result = new PostDeleteCommandResult();
      result.Status = StatusEnum.Ok;

      return result;
    }
  }
}
