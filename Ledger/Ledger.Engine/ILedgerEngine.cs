using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ledger.Engine.Calculator;
using Ledger.Engine.DataAccess;
using Ledger.Engine.Model;

namespace Ledger.Engine
{
  public interface ILedgerEngine
  {
    public Task AppendEquityEventAsync(
      EquityEvent equityEvent,
      CancellationToken ctx = default);

    public Task<IEnumerable<EquityPosition>> GetAllEmployeePositionsAsync(
      DateTime targetDate,
      int precision,
      CancellationToken ctx = default);
  }

  public class LedgerEngine : ILedgerEngine
  {
    private readonly IEquityDataStore _dataStore;
    private readonly IEquityEventAggregator _aggregator;

    public LedgerEngine(
      IEquityDataStore dataStore,
      IEquityEventAggregator aggregator)
    {
      _dataStore = dataStore;
      _aggregator = aggregator;
    }

    public async Task AppendEquityEventAsync(
      EquityEvent equityEvent,
      CancellationToken ctx = default)
    {
      await _dataStore.AppendEquityEventAsync(equityEvent, ctx);
    }

    public async Task<IEnumerable<EquityPosition>> GetAllEmployeePositionsAsync(
      DateTime targetDate,
      int precision,
      CancellationToken ctx = default)
    {
      var equityEvents = await _dataStore.GetAsync(ctx);
      return _aggregator.GetAllEmployeePositions(equityEvents, targetDate, precision);
    }
  }
}
