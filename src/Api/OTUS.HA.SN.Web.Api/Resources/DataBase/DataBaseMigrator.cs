using System.Reflection;
using DbUp;

namespace OTUS.HA.SN.Web.Api.Resources.DataBase
{
  internal class DataBaseMigrator
  {
    public DataBaseMigrator(
      ILogger<DataBaseMigrator> logger,
      IConfiguration configuration
      )
    {
      connectionString = configuration.GetConnectionString("MigrationContext");
      this._logger = logger;
    }

    private readonly string connectionString;
    private readonly ILogger<DataBaseMigrator> _logger;

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
      }
      while (currentTry <= tries);


      var upgrader =
        DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
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
