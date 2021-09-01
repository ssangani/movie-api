using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ledger.Cli.Calculator;
using Ledger.Cli.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TinyCsvParser;

namespace Ledger.Cli.Service
{
  public class LedgerService : IHostedService
  {
    private const bool SkipHeader = false;
    private const char Delimiter = ',';
    private const int MinPrecision = 0;
    private const int MaxPrecision = 6;

    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly LedgerCommand _command;
    private readonly IEquityEventAggregator _aggregator;

    private int? _exitCode;

    public LedgerService(
      ILogger<LedgerService> logger,
      IHostApplicationLifetime appLifetime,
      IOptions<LedgerCommand> command,
      IEquityEventAggregator aggregator)
    {
      Validate(command.Value);
      _logger = logger;
      _appLifetime = appLifetime;
      _command = command.Value;
      _aggregator = aggregator;
    }

    /// <summary>
    /// Validate the program parameters.
    /// If invalid, this will stop the applciation
    /// </summary>
    /// <param name="cmd"></param>
    private void Validate(LedgerCommand cmd)
    {
      if (cmd.Precision < MinPrecision || cmd.Precision > MaxPrecision)
        throw new ArgumentException($"Precision parameter must be in range [{MinPrecision}, {MaxPrecision}]");
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

      _ = _appLifetime.ApplicationStarted.Register(() => Run());

      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      _logger.LogDebug($"Exiting with return code: {_exitCode}");

      // In case of forced cancellation set exit code to -1
      Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
      return Task.CompletedTask;
    }

    /// <summary>
    /// The main brain of the application.
    /// This is where you can start abstracting out classes/functions by responsibility.
    /// </summary>
    private void Run()
    {
      try
      {
        // Read the equity positions data from file provided via CLI args
        var equityEvents = GetEvents();

        // Aggregate all equity positions vested by input grant date
        var equityPositions = _aggregator.GetAllEmployeePositions(
          equityEvents,
          _command.TargetDate,
          _command.Precision);

        // Print the results
        var res = new StringBuilder("Output:\n");
        foreach (var equityPosition in equityPositions)
        {
          res.AppendLine(equityPosition.ToString());
        }

        _logger.LogInformation(res.ToString());

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


    /// <summary>
    /// NOTE: Hope you don't OOM the app with a large file.
    ///
    /// This will read the file and load it into an in-memory collection.
    /// This can be decoupled into a separate service which can keep ingesting
    /// stream of equity events and append them to a datastore.
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EquityEvent> GetEvents()
    {
      _logger.LogDebug($"Reading from {_command.ImportPath}");

      // This library is very performant and makes reading file as a list very simple
      var csvOptions = new CsvParserOptions(SkipHeader, Delimiter);
      var mapping = new CsvEquityEventMapping();
      var parser = new CsvParser<EquityEvent>(csvOptions, mapping);

      return parser
        .ReadFromFile(_command.ImportPath, Encoding.ASCII)
        .Where(evt => evt.IsValid)
        .Select(evt => evt.Result);
    }
  }
}