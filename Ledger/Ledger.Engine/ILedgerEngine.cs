﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ledger.Engine.DataAccess;
using Ledger.Engine.Model;

namespace Ledger.Engine
{
  public interface ILedgerEngine
  {
    public Task AppendEquityEventAsync(
      EquityEvent equityEvent,
      CancellationToken ctx = default);

    public Task<IEnumerable<EquityEvent>> GetAsync(CancellationToken ctx = default);
  }

  public class LedgerEngine : ILedgerEngine
  {
    public readonly IEquityDataStore _dataStore;

    public LedgerEngine(
      IEquityDataStore dataStore)
    {
      _dataStore = dataStore;
    }

    public async Task AppendEquityEventAsync(
      EquityEvent equityEvent,
      CancellationToken ctx = default)
    {
      await _dataStore.AppendEquityEventAsync(equityEvent, ctx);
    }

    public Task<IEnumerable<EquityEvent>> GetAsync(CancellationToken ctx = default)
    {
      return _dataStore.GetAsync(ctx);
    }
  }
}
