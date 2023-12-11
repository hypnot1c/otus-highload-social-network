using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OTUS.HS.SN.Data.Master.Context;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UserRegistationCommandHandler : BaseCommandHandler, IRequestHandler<UserRegistationCommand, UserRegistationCommandResult>
  {
    public UserRegistationCommandHandler(
      IMapper mapper,
      MasterContext masterContext,
      ILogger<UserRegistationCommandHandler> logger
      ) : base(mapper, masterContext, logger)
    {
    }

    public async Task<UserRegistationCommandResult> Handle(UserRegistationCommand request, CancellationToken cancellationToken)
    {
      var userDBO = this.Mapper.Map<UserModel>(request);

      //userDBO.PasswordHash = request.Password.GetPasswordHash();

      this.MasterContext.Users.Add(userDBO);

      UserRegistationCommandResult result;
      try
      {
        await this.MasterContext.SaveChangesAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        result = new UserRegistationCommandResult(new UnexpectedResultError(ex));
        return result;
      }

      result = this.Mapper.Map<UserRegistationCommandResult>(userDBO);
      result.Status = StatusEnum.Ok;

      return result;
    }
  }
}
