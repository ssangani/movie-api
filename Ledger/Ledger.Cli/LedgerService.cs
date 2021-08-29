using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ledger.Cli
{
  public class LedgerService : IHostedService
  {
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly LedgerCommand _command;

    private int? _exitCode;

    public LedgerService(
      ILogger<LedgerService> logger,
      IHostApplicationLifetime appLifetime,
      IOptions<LedgerCommand> command)
    {
      _logger = logger;
      _appLifetime = appLifetime;
      _command = command.Value;
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
        _logger.LogInformation($"Path:{_command.ImportPath}");
        await Task.Yield();

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

      Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
      return Task.CompletedTask;
    }
  }
}