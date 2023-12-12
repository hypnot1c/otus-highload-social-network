using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OTUS.HA.SN.Web.App.Auth.Model.Input;
using OTUS.HS.SN.Data.Master.Context;
using OTUS.HS.SN.Data.Master.Model;
using Web.App.Auth.Client;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UserRegistationCommandHandler : BaseCommandHandler, IRequestHandler<UserRegistationCommand, UserRegistationCommandResult>
  {

    public UserRegistationCommandHandler(
      IWebAppAuthClient webAppAuthClient,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<UserRegistationCommandHandler> logger
      ) : base(mapper, masterContext, logger)
    {
      this._webAppAuthClient = webAppAuthClient;
    }

    private readonly IWebAppAuthClient _webAppAuthClient;

    public async Task<UserRegistationCommandResult> Handle(UserRegistationCommand request, CancellationToken cancellationToken)
    {
      var userDBO = this.Mapper.Map<UserModel>(request);

      this.MasterContext.Users.Add(userDBO);

      UserRegistationCommandResult result;
      using var tran = await this.MasterContext.Database.BeginTransactionAsync(cancellationToken);
      try
      {
        await this.MasterContext.SaveChangesAsync(cancellationToken);
        var im = this.Mapper.Map<UserCreateInputModel>(userDBO);
        im.Password = request.Password;
        await this._webAppAuthClient.UserCreate(im, cancellationToken);

        await tran.CommitAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        await tran.RollbackAsync(cancellationToken);
        result = new UserRegistationCommandResult(new UnexpectedResultError(ex));
        return result;
      }

      result = this.Mapper.Map<UserRegistationCommandResult>(userDBO);
      result.Status = StatusEnum.Ok;

      return result;
    }
  }
}
