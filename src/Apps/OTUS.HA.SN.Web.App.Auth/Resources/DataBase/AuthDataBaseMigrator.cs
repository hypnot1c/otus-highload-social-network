using DbUp;
using OTUS.HS.SN.Data.Auth.Context;

namespace OTUS.HA.SN.Web.App.Auth.Resources
{
  internal class AuthDataBaseMigrator
  {
    public AuthDataBaseMigrator(
      ILogger<AuthDataBaseMigrator> logger,
      IConfiguration configuration
      )
    {
      connectionString = configuration.GetConnectionString("AuthMigrationContext");
      this._logger = logger;
    }

    private readonly string connectionString;
    private readonly ILogger<AuthDataBaseMigrator> _logger;

    public async ValueTask MigrateDatabase()
    {
      var tries = 5;
      var currentTry = 0;
      var tryInteraval = 2000;

      do
      {
        if (currentTry != 1)
        {
          await Task.Delay(tryInteraval);
        }

        currentTry++;

        try
        {
          EnsureDatabase.For.PostgresqlDatabase(connectionString);
        }
        catch (Exception ex)
        {
          this._logger.LogError(ex, "Error check for DB");
        }

        tryInteraval += currentTry * 1000;
      }
      while (currentTry <= tries);


      var upgrader =
        DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(typeof(AuthContext).Assembly)
        .WithTransaction()
        .WithExecutionTimeout(TimeSpan.FromSeconds(180))
        .LogToConsole()
        .Build()
        ;

      var result = upgrader.PerformUpgrade();

      if (!result.Successful)
      {
        throw new Exception("Failed to upgrade database", result.Error);
      }



    }
  }
}
