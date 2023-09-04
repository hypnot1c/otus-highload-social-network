using System;
using System.Security.Cryptography;
using System.Text;
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

      userDBO.PasswordHash = GetPasswordHash(request.Password);


      this.MasterContext.Users.Add(userDBO);
      await this.MasterContext.SaveChangesAsync(cancellationToken);

      var result = this.Mapper.Map<UserRegistationCommandResult>(userDBO);
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
