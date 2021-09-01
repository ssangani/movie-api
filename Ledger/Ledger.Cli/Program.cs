using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Ledger.Cli.Calculator;
using Ledger.Cli.Model;
using Ledger.Cli.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Ledger.Cli
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      await CreateHosBuilder(args)
        .Build()
        .RunAsync();
    }

    private static IHostBuilder CreateHosBuilder(string[] args) {
      var builtConfig = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddJsonFile("appsettings.json")
        .AddCommandLine(args)
        .Build();

      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builtConfig)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

      return Host
        .CreateDefaultBuilder(args)
        .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
        .UseSerilog()
        .ConfigureAppConfiguration(builder =>
        {
          builder.AddConfiguration(builtConfig);
        })
        .ConfigureServices((hostContext, services) =>
        {
          services
            .AddTransient<IEquityEventAggregator, EquityEventAggregator>()
            .Configure<LedgerCommand>(builtConfig)
            .AddHostedService<LedgerService>();
        });
    }
  }
}
