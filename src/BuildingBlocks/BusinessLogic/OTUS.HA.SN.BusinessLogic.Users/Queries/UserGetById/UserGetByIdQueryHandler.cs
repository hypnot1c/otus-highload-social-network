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
  public class UserGetByIdQueryHandler : BaseQueryHandler, IRequestHandler<UserGetByIdQuery, UserGetByIdQueryResult>
  {
    public UserGetByIdQueryHandler(
      IMapper mapper,
      MasterContext masterContext,
      ILogger<UserGetByIdQueryHandler> logger
      ) : base(
        mapper,
        masterContext,
        logger
        )
    {
    }

    public async Task<UserGetByIdQueryResult> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
      var user = await this.Mapper.ProjectTo<UserGetByIdQueryResult>(
        this.MasterContext.Users
          .Where(u => u.PublicId == request.Id)
        )
        .SingleOrDefaultAsync(cancellationToken)
        ;

      return user;
    }
  }
}
