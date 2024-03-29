using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OTUS.HS.SN.Data.Auth.Context;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.BusinessLogic
{
  public class LoginQueryHandler : BaseQueryHandler, IRequestHandler<LoginQuery, LoginQueryResult>
  {
    public LoginQueryHandler(
      IMapper mapper,
      AuthSlave1Context authSlave1Context,
      MasterContext masterContext,
      ILogger<LoginQueryHandler> logger
      ) : base(
        mapper,
        masterContext,
        logger
        )
    {
      AuthSlave1Context = authSlave1Context;
    }

    public AuthSlave1Context AuthSlave1Context { get; }

    public async Task<LoginQueryResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
      var hash = request.Password.GetPasswordHash();

      LoginQueryResult result;
      try
      {
        result = await this.Mapper.ProjectTo<LoginQueryResult>(
          this.AuthSlave1Context.Users
          .Where(u => u.PublicId == request.Id)
          .Where(u => u.PasswordHash == hash)
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
        result = new LoginQueryResult(new UnexpectedResultError(ex));
        return result;
      }

      result = new LoginQueryResult(new NotFoundResultError());

      return result;
    }
  }
}
