using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ledger.Engine.Model;

namespace Ledger.Engine.DataAccess
{
  public class LocalEquityDataStore : IEquityDataStore
  {
    private readonly IList<EquityEvent> _events;

    public LocalEquityDataStore()
    {
      _events = new List<EquityEvent>();
    }

    public async Task AppendEquityEventAsync(
      EquityEvent equityEvent,
      CancellationToken ctx = default)
    {
      _events.Add(equityEvent);
      await Task.Yield();
    }

    public async Task<IEnumerable<EquityEvent>> GetAsync(CancellationToken ctx = default)
    {
      return await Task.FromResult(_events);
    }
  }
}
