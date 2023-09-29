using System.Security.Cryptography;
using OTUS.HS.SN.DB.Data.Fakers;
using PowerArgs;

namespace OTUS.HS.SN.DB.Data
{
  [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
  public class ProgramActions
  {
    [ArgActionMethod, ArgDescription("Generates users")]
    public async Task User(UserGenerationArgs args)
    {
      var password = "password".GetPasswordHash();

      var userFaker = new UserFaker(password);

      var folder = Path.Combine(Environment.CurrentDirectory, "user_data.copy");
      if (args.OutputFile is not null)
      {
        folder = args.OutputFile;
      }

      using var writer = new StreamWriter(folder);

      for (var i = 0; i < args.Count; i++)
      {
        var testUser = userFaker.Generate();
        var line = $"{testUser.PublicId}\t{testUser.Firstname}\t{testUser.Secondname}\t{testUser.BirthDate.ToString("yyyy-MM-dd")}\t{testUser.Biography}\t{testUser.City}\t{testUser.PasswordHash}";
        await writer.WriteLineAsync(line);
      }
    }
  }
}
