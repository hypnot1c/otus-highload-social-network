using Correlate.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Web.App.Auth.Client
{
  public static class ServiceCollectionExtenstions
  {
    public static IServiceCollection AddWebAppAuthClient(this IServiceCollection services, string url)
    {
      services
        .AddRefitClient<IWebAppAuthClient>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(url))
        .CorrelateRequests("X-Correlation-Id")
        ;

      return services;
    }
  }
}
