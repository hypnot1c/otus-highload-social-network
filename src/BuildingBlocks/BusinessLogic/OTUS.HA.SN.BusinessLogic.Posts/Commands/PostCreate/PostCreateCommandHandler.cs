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
  public class PostCreateCommandHandler : BaseCommandHandler, IRequestHandler<PostCreateCommand, PostCreateCommandResult>
  {
    public PostCreateCommandHandler(
      IMediator mediator,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<PostCreateCommandHandler> logger
      ) : base(mapper, masterContext, logger)
    {
      Mediator = mediator;
    }

    private IMediator Mediator { get; }

    public async Task<PostCreateCommandResult> Handle(PostCreateCommand request, CancellationToken cancellationToken)
    {
      int? userId = await this.MasterContext.Users
        .Where(u => u.PublicId == request.AuthorId)
        .Select(u => u.Id)
        .SingleOrDefaultAsync(cancellationToken)
        ;

      PostCreateCommandResult result;
      if (userId is null)
      {
        result = new PostCreateCommandResult(new NotFoundResultError());
        return result;
      }

      var postModel = this.Mapper.Map<PostModel>(request);
      postModel.AuthorId = userId.Value;

      this.MasterContext.Posts.Add(postModel);

      try
      {
        await this.MasterContext.SaveChangesAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        result = new PostCreateCommandResult(new UnexpectedResultError(ex));
        return result;
      }

      var notif = this.Mapper.Map<PostCreatedNotification>(postModel);
      notif.AuthorPublicId = request.AuthorId;

      await this.Mediator.Publish(notif, cancellationToken);

      result = this.Mapper.Map<PostCreateCommandResult>(postModel);
      result.Status = StatusEnum.Ok;

      return result;
    }
  }
}
