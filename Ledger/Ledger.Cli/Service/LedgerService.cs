using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ledger.Engine;
using Ledger.Engine.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TinyCsvParser;

namespace Ledger.Cli.Service
{
  public class LedgerService : IHostedService
  {
    private const bool SkipHeader = true;
    private const char Delimiter = ',';

    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly LedgerCommand _command;
    private readonly ILedgerEngine _engine;

    private int? _exitCode;

    public LedgerService(
      ILogger<LedgerService> logger,
      IHostApplicationLifetime appLifetime,
      IOptions<LedgerCommand> command,
      ILedgerEngine engine)
    {
      _logger = logger;
      _appLifetime = appLifetime;
      _command = command.Value;
      _engine = engine;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

      _ = _appLifetime.ApplicationStarted.Register(async () => await RunAsync(cancellationToken));

      return Task.CompletedTask;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
      try
      {
        // Seed data from file provided via CLI args
        await SeedAsync(cancellationToken);

        // Print
        var events = await _engine.GetAsync(cancellationToken);
        foreach (var evt in events)
        {
          _logger.LogInformation(evt.ToString());
        }

        // Exit with success
        _exitCode = 0;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Unhandled exception!");
        _exitCode = 1;
      }
      finally
      {
        // Stop the application once the work is done
        _appLifetime.StopApplication();
      }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      _logger.LogDebug($"Exiting with return code: {_exitCode}");

      // In case of forced cancellation set exit code to -1
      Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
      return Task.CompletedTask;
    }

    private async Task SeedAsync(CancellationToken cancellationToken)
    {
      _logger.LogInformation($"Seeding Data from {_command.ImportPath}");

      var csvOptions = new CsvParserOptions(SkipHeader, Delimiter);
      var mapping = new CsvEquityEventMapping();
      var parser = new CsvParser<EquityEvent>(csvOptions, mapping);

      var seedTasks = parser
        .ReadFromFile(_command.ImportPath, Encoding.ASCII)
        .Where(evt => evt.IsValid)
        .ToList()
        .Select(evt => _engine.AppendEquityEventAsync(evt.Result, cancellationToken));

      await Task.WhenAll(seedTasks);
    }
  }
}