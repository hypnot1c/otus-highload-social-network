using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OTUS.HS.SN.Data.Master.Context;

namespace OTUS.HA.SN.BusinessLogic
{
  public class LoginQueryHandler : BaseQueryHandler, IRequestHandler<LoginQuery, LoginQueryResult>
  {
    public LoginQueryHandler(
      IMapper mapper,
      MasterContext masterContext,
      ILogger<LoginQueryHandler> logger
      ) : base(
        mapper,
        masterContext,
        logger
        )
    {
    }

    public async Task<LoginQueryResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
      var hash = this.GetPasswordHash(request.Password);

      var result = await this.Mapper.ProjectTo<LoginQueryResult>(
        this.MasterContext.Users
        .Where(u => u.PublicId == request.Id)
        .Where(u => u.PasswordHash == hash)
        )
        .SingleOrDefaultAsync(cancellationToken)
        ;

      return result;
    }

    private string GetPasswordHash(string password)
    {
      var bytes = Encoding.Unicode.GetBytes(password);
      var hash = SHA256.Create().ComputeHash(bytes);
      string hashString = string.Empty;
      foreach (var x in hash)
      {
        hashString += String.Format("{0:x2}", x);
      }
      return hashString;
    }
  }
}
