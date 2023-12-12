using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OTUS.HS.SN.Data.Auth.Context;
using OTUS.HS.SN.Data.Auth.Model;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.BusinessLogic.Auth
{
  /// <summary>
  /// 
  /// </summary>
  public class UserCreateCommandHandler : BaseCommandHandler, IRequestHandler<UserCreateCommand, UserCreateCommandResult>
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    public UserCreateCommandHandler(
      IMapper mapper,
      AuthContext authContext,
      MasterContext masterContext,
      ILogger<UserCreateCommandHandler> logger
      ) : base(mapper, masterContext, logger)
    {
      AuthContext = authContext;
    }

    public AuthContext AuthContext { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<UserCreateCommandResult> Handle(UserCreateCommand command, CancellationToken cancellationToken)
    {
      var userDBO = this.Mapper.Map<UserModel>(command);

      userDBO.PasswordHash = command.Password.GetPasswordHash();

      UserCreateCommandResult result = null;
      try
      {
        this.AuthContext.Users.Add(userDBO);
        await this.AuthContext.SaveChangesAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        result = new UserCreateCommandResult(new UnexpectedResultError(ex));
        return result;
      }
      result = new UserCreateCommandResult();
      result.Status = StatusEnum.Ok;
      return result;
    }
  }
}
