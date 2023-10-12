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
  public class UserSearchQueryHandler : BaseQueryHandler, IRequestHandler<UserSearchQuery, UserSearchQueryResult>
  {
    public UserSearchQueryHandler(
      Slave1Context slave1Context,
      IMapper mapper,
      MasterContext masterContext,
      ILogger<UserSearchQueryHandler> logger
      ) : base(
        mapper,
        masterContext,
        logger
        )
    {
      this._slave1Context = slave1Context;
    }

    private Slave1Context _slave1Context;

    public async Task<UserSearchQueryResult> Handle(UserSearchQuery request, CancellationToken cancellationToken)
    {
      var result = new UserSearchQueryResult();
      try
      {
        var query = this._slave1Context.Users.AsQueryable();

        if (!String.IsNullOrWhiteSpace(request.Firstname))
          query = query.Where(u => u.Firstname.ToLower().StartsWith(request.Firstname));

        if (!String.IsNullOrWhiteSpace(request.Lastname))
          query = query.Where(u => u.Secondname.ToLower().StartsWith(request.Lastname));

        result.Items = await this.Mapper.ProjectTo<UserGetByIdQueryResult>(query)
          .ToListAsync(cancellationToken)
          ;

        result.Status = StatusEnum.Ok;
        return result;
      }
      catch (Exception ex)
      {
        result = new UserSearchQueryResult(new UnexpectedResultError(ex));
        return result;
      }
    }
  }
}
