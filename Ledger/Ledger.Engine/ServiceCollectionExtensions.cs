using Ledger.Engine.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.Engine
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddLedgerServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddTransient<ILedgerEngine, LedgerEngine>();
      services.AddSingleton<IEquityDataStore, LocalEquityDataStore>();

      return services;
    }
  }
}
